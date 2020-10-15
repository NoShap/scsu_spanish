using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    private bool firstObjectHit = false;
    private bool objectSet = false;
    private GameObject obj;  //stores the most recent past object to be hit
    private Renderer objRenderer;
    public Material highlightMaterial;
    private Material oldMtl;
    private bool hitSuccess;
    private bool displayGUI;
    private RaycastHit hit;
    public Font customFont;
    private bool notPlaying = true; //checks if an audio file is currently playing
    public AudioManager WordAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        displayGUI = false;

    }

    // Update is called once per frame
    void Update()
    {
        hitSuccess = false;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // When target object hit
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<LanguageObserverTarget>() != null)
        {
            obj = hit.collider.gameObject;
            if (obj != null && !objectSet)
            {
                objectSet = true;
                firstObjectHit = true;
                hitSuccess = true;
                objRenderer = obj.GetComponent<Renderer>();
                oldMtl = objRenderer.material;
                objRenderer.material = highlightMaterial;
            }

            //allow player to hear audio when 'x' key is pressed
            //allow this any number of times as long as they're still looking at the object
            if (Input.GetKeyDown("x") && notPlaying)
            {
                StartCoroutine(PlayAudio(obj));
            }

        }

        //When not hitting
        else
        {
            if (objectSet)
            {
                if (firstObjectHit)
                {
                    objRenderer.material = oldMtl;
                }
                //destroy canvasses
                displayGUI = false;
                Destroy(GameObject.Find("Canvas"));
                objectSet = false;
            }
        }
    }

    IEnumerator PlayAudio(GameObject obj)
    {
        notPlaying = false;
        string name = obj.name.ToLower();
        WordAudioManager.Play(name);
        AudioClip aud = WordAudioManager.GetClip(name);
        yield return new WaitForSeconds(aud.length + 1f);
        notPlaying = true;

    }

    void OnGUI()
    {
        if (hitSuccess)
        {
            if (!displayGUI) //if the GUI isn't already displaying
            {
                //GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 200), hit.collider.gameObject.name + "Press X to hear Audio");
                //create new Canvas
                GameObject newCanvas = new GameObject("Canvas");
                //set location of canvas
                newCanvas.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + (float)0.5, obj.transform.position.z);
                Canvas c = newCanvas.AddComponent<Canvas>();
                newCanvas.transform.SetParent(newCanvas.transform, false);
                c.renderMode = RenderMode.WorldSpace;

                //set size of canvas
                RectTransform rtc = newCanvas.GetComponent<RectTransform>();
                rtc.sizeDelta = new Vector2(9, 5);
                rtc.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);


                newCanvas.AddComponent<CanvasScaler>();
                newCanvas.GetComponent<CanvasScaler>().referencePixelsPerUnit = 2000;
                newCanvas.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 200;

                newCanvas.AddComponent<GraphicRaycaster>();

                //add  text to UI
                Text t = newCanvas.AddComponent<Text>();
                t.text = hit.collider.gameObject.name + "\n Press X to hear Audio";
                t.font = customFont;
                t.material = customFont.material;
                t.fontSize = 1;
                t.alignment = TextAnchor.MiddleCenter;
                t.color = Color.blue;

                // //create Panel
                // GameObject panel = new GameObject("Panel");
                // panel.AddComponent<CanvasRenderer>();

                // //Add style to panel
                // Image i = panel.AddComponent<Image>();
                // i.color = Color.white;

                // //set Size of panel
                // RectTransform rt = panel.GetComponent<RectTransform>();
                // rt.sizeDelta = new Vector2(9,5);
                // //rt.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
                // panel.transform.position = new Vector3(panel.transform.position.x - (float)0.1, panel.transform.position.y, panel.transform.position.z - (float)0.1);

                // panel.transform.SetParent(newCanvas.transform, false);


                //allow GUI to follow camera
                newCanvas.AddComponent<followCamera>();
                newCanvas.GetComponent<followCamera>().m_Camera = Camera.main;

                //indicate we're currently displaying a GUI
                displayGUI = true;
            }
        }
    }
}

