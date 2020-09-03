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
                Canvas c = newCanvas.AddComponent<Canvas>();
                c.renderMode = RenderMode.WorldSpace;
                newCanvas.AddComponent<CanvasScaler>();
                newCanvas.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 200;
                newCanvas.AddComponent<GraphicRaycaster>();

                //set size of canvas
                RectTransform rtc = newCanvas.GetComponent<RectTransform>();
                rtc.sizeDelta = new Vector2(10,10);

                //create Panel
                GameObject panel = new GameObject("Panel");
                panel.AddComponent<CanvasRenderer>();
                
                // //Add style to panel
                // Image i = panel.AddComponent<Image>();
                // i.color = Color.white;

                // //set Size of panel
                // RectTransform rt = panel.GetComponent<RectTransform>();
                // rt.sizeDelta = new Vector2(1,1);

                //add  text to UI
                Text t = newCanvas.AddComponent<Text>();
                t.text = hit.collider.gameObject.name + "\n Press X to hear Audio";
                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                t.font = ArialFont;
                t.material = ArialFont.material;
                t.fontSize = 1;
                t.alignment = TextAnchor.MiddleCenter;
                t.color = Color.black;
                panel.transform.SetParent(newCanvas.transform, false);
                


                //allow GUI to follow camera
                newCanvas.AddComponent<followCamera>();
                newCanvas.GetComponent<followCamera>().m_Camera = Camera.main;

                //indicate we're currently displaying a GUI
                displayGUI = true;
             }
         }
    }
}

