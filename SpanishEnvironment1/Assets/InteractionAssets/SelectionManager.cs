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
    public Camera customCamera;
    public int numObserved = 0;

    // Start is called before the first frame update
    void Start()
    {
        displayGUI = false;

    }

    // Update is called once per frame
    void Update()
    {
        hitSuccess = false;
        var ray = customCamera.ScreenPointToRay(Input.mousePosition);

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
                print("Material stored:, " + oldMtl.name);
                objRenderer.material = highlightMaterial;
                numObserved += 1;
            }
        }

        //When not hitting
        else
        {
            if (objectSet)
            {
                if (firstObjectHit)
                {
                    print("Material fetched:" + oldMtl.name);
                    objRenderer.material = oldMtl;
                }
                //destroy canvasses
                displayGUI = false;
                Destroy(GameObject.Find("Canvas"));
                objectSet = false;
            }
        }
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
                newCanvas.GetComponent<followCamera>().m_Camera = customCamera;

                //indicate we're currently displaying a GUI
                displayGUI = true;
            }
        }
    }
}

