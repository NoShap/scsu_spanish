using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    private GameObject obj;  //stores the most recent past object to be hit
    private Renderer objRenderer;
	public Material highlightMaterial;
    private Material oldMtl;
    private bool hitSuccess;
    private bool displayGUI;
    private RaycastHit hit;
    public Font customFont;
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
        if(Physics.Raycast(ray, out hit))
        {
            if(obj != hit.collider.gameObject)//If we're not pointing at the previous target
             {
                   if(obj != null)//If previous target is set, reset its material
                    {
                     objRenderer.material = oldMtl;
                    }
                    obj = hit.collider.gameObject;//Store reference of target to a variable
                    objRenderer = obj.GetComponent<Renderer>();//Get targets Renderer

                    oldMtl = objRenderer.material;//Store targets current material
             }

            //sets newly hit object's material
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();
            if(selectionRenderer != null)
            {
                selectionRenderer.material = highlightMaterial;
                hitSuccess = true;
            }
        }
        else
        {
            //if the raycaster isn't hitting anything, remove all GUIs currently displayed
            //ensures only one GUI is seen at a time
            displayGUI = false;
            Destroy (GameObject.Find ("Canvas"));
        }
    }

    void OnGUI ()
    {
         if(hitSuccess)
         {
             if(!displayGUI) //if the GUI isn't already displaying
             {
                //GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 200), hit.collider.gameObject.name + "Press X to hear Audio");
                //create new Canvas
                GameObject newCanvas = new GameObject("Canvas");
                //set location of canvas
                newCanvas.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + (float)0.5 , obj.transform.position.z);
                Canvas c = newCanvas.AddComponent<Canvas>();
                newCanvas.transform.SetParent(newCanvas.transform, false);
                c.renderMode = RenderMode.WorldSpace;

                //set size of canvas
                RectTransform rtc = newCanvas.GetComponent<RectTransform>();
                rtc.sizeDelta = new Vector2(9,5);
                rtc.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);


                newCanvas.AddComponent<CanvasScaler>();
                newCanvas.GetComponent<CanvasScaler>().referencePixelsPerUnit = 2000;
                newCanvas.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 200;
                
                newCanvas.AddComponent<GraphicRaycaster>();

                //add  text to UI
                Text t = newCanvas.AddComponent<Text>();
                t.text =  hit.collider.gameObject.name + "\n Press X to hear Audio";
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

