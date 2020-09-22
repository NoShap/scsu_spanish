using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkpointManager : MonoBehaviour
{
    private double currStage = 0f;
    public GameObject checkpointPrefab;
    private GameObject currCheckpoint;
    

    /*
        Numeric Stages : Player progress in game
        2 : food delivery
    */


    // Start is called before the first frame update
    void Start()
    {
        currStage = 2f;

    }

    // Update is called once per frame
    //i guess this will have to look like a State Machine
    void Update()
    {
         
        if(currStage == 2f) //beginning food delivery task
        {
            if(Input.GetKeyDown("1"))
            {
                //display text
                GameObject.Find("UI").GetComponent<Text>().text = "\n Proceed to the Food Pickup Area outside the prison.";
                //instantiate a checkpoint prefab at position and rotation rot. then transform it to the appropriate size.
                Quaternion rot = Quaternion.Euler(0, -34.2f, 0);
                currCheckpoint = Instantiate(checkpointPrefab, new Vector3(119.81f, 5.1622f, 148.78f), rot);
                currCheckpoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                currStage = 2.1f;
            }
        }
        if(currStage == 2.1f)
        {
            if(currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true) //if player has reached the checkpoint, destroy this instance of the checkpoint prefab and display new instructions
            {
                Destroy(currCheckpoint);
                GameObject.Find("UI").GetComponent<Text>().text = "\n Pick up a Tray. Press x to continue";
                currStage = 2.2f;
            }
        }
        if(currStage == 2.2f)
        {
            if(Input.GetKeyDown("x"))
            {
                GameObject.Find("UI").GetComponent<Text>().text = "\n Deliver the tray to Sanchez Mazas' prison cell.";
                //instantiate a checkpoint by Sanchez Mazas' Cell
                Quaternion rot = Quaternion.Euler(-1f, -28f, 0f);
                currCheckpoint = Instantiate(checkpointPrefab, new Vector3(134f, 5f, 110f), rot);
                currCheckpoint.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                currStage = 2.3f;
            }
        }
        if(currStage == 2.3f)
        {
            if(currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true) 
            {
                GameObject.Find("UI").GetComponent<Text>().text = "\n Task Complete. You may now Explore the Map.";
                currStage = 2.4f;
            }
        }
         if(currStage == 2.4f)
        {
            if(Input.GetKeyDown("5")) //to be changed to colliding w/ checkpoint
            {
                GameObject.Find("UI").GetComponent<Text>().text = "\n Explore the map.";
                currStage = 2.5f;
            }
        }
         if(currStage == 2.5f)
        {
            if(Input.GetKeyDown("6")) //to be changed to colliding w/ checkpoint
            {
                Destroy(GameObject.Find("FoodTray"));
                currStage = 2.6f;
            }
        }

    }

    
}
