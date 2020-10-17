using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkpointManager : MonoBehaviour
{
    public enum stage
    {
        fadeIn, // screen goes from black to full brightness
        voiceOver1, //historical intro
        dialogue1, //talk with Sanchez Mazas
        voiceOver2, //explaining checkpoints and tasks
        task1, // get food for Sanchez Mazas 
        voiceOver3, //explaining languageObserver
        dialogue2, //dialogue with food delivery guard
        task2, // pick up food and receive directive to return to SM
        dialogue3, //talk with Sanchez Mazas 
        event1, //watching prisoners be escorted out
        dialogue4, //tell Sanchez Mazas to get up and move outside
        event2, // Sanchez Mazas runs away
        dialogue5, // confront Sanchez Mazas
        voiceOver4, //consequences
        fadeOut,
    }

    private stage currStage = stage.fadeIn;
    public GameObject checkpointPrefab;
    private GameObject currCheckpoint;
    public GameObject door;
    private AudioManager audio_manager;
    private GameObject dialogueManager;
    private DialogueManager dialogue1;
    bool stageOpen = true;



    // Start is called before the first frame update
    void Start()
    {
        audio_manager = FindObjectOfType<AudioManager>();
        currStage = stage.voiceOver1; //should eventually be set to fadeIn
        // dialogueManager = GameObject.Find("Dialogue1");
        // dialogue1 = dialogueManager.GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    //State Machine controlling Player Progress throughout Tasks
    void Update()
    {

        // Stage 0
        if (currStage == stage.fadeIn)
        {
            // Wait for Fade In and then proceed
            currStage++;
        }
        // Stage 1
        if (currStage == stage.voiceOver1 && stageOpen)
        {
            stageOpen = false;
            //call coroutine to play audio file of voiceover
            StartCoroutine(waitForAudioClip("VoiceOver1"));
        }
        if (currStage == stage.dialogue1 && stageOpen)
        {
            //dialogue1.curDialogue = 1;
            //get something that increments curr stage 

        }
        // Stage 2
        if (currStage == stage.voiceOver2 && stageOpen) //beginning food delivery task
        {
            //display text
            GameObject.Find("UI").GetComponent<Text>().text = "\n Proceed to the Food Pickup Area outside the prison.";
            //instantiate a checkpoint prefab at position and rotation rot. then transform it to the appropriate size.
            Quaternion rot = Quaternion.Euler(0, -34.2f, 0);
            currCheckpoint = Instantiate(checkpointPrefab, new Vector3(119.81f, 5.1622f, 148.78f), rot);
            currCheckpoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            stageOpen = false;
            StartCoroutine(waitForAudioClip("VoiceOver2"));

        }
        // //Start interaction with Cook
        // if (currStage == 3f && stageOpen)
        // {
        //     if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true) //if player has reached the checkpoint, destroy this instance of the checkpoint prefab and display new instructions
        //     {
        //         Destroy(currCheckpoint);
        //         GameObject.Find("UI").GetComponent<Text>().text = "\n Select the apple and the potatoes and add them to the tray. Press x to continue";
        //         stageOpen = false;
        //         StartCoroutine(waitForAudioClip("VoiceOver3"));
        //     }
        // }
        // if (currStage == 4f)
        // {
        //     if (Input.GetKeyDown("x"))
        //     {
        //         GameObject.Find("UI").GetComponent<Text>().text = "\n Deliver the tray to Sanchez Mazas' prison cell.";
        //         //instantiate a checkpoint by Sanchez Mazas' Cell
        //         Quaternion rot = Quaternion.Euler(-1f, -28f, 0f);
        //         currCheckpoint = Instantiate(checkpointPrefab, new Vector3(134f, 5f, 110f), rot);
        //         currCheckpoint.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //         currStage = 5f;
        //     }
        // }
        // if (currStage == 5f)
        // {
        //     if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true)
        //     {

        //         StartCoroutine("GiveFood");
        //         GameObject.Find("UI").GetComponent<Text>().text = "\n Task Complete. You may now Explore the Map.";
        //         currStage = 6f;
        //     }
        // }
        // if (currStage == 6f)
        // {
        //     if (Input.GetKeyDown("5")) //to be changed to colliding w/ checkpoint
        //     {
        //         GameObject.Find("UI").GetComponent<Text>().text = "\n Explore the map.";
        //         currStage = 7f;
        //     }
        // }
        // if (currStage == 7f)
        // {
        //     if (Input.GetKeyDown("6")) //to be changed to colliding w/ checkpoint
        //     {
        //         Destroy(GameObject.Find("FoodTray"));
        //         currStage = 8f;
        //     }
        // }

    }

    IEnumerator waitForAudioClip(string name)
    {
        audio_manager.Play(name);
        AudioClip aud = audio_manager.GetClip(name);
        yield return new WaitForSeconds(aud.length + 1f);
        currStage++;
        stageOpen = true;
    }

    IEnumerator GiveFood()
    {
        door.GetComponent<Animator>().Play("door open", 0, 0f);
        door.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        door.GetComponent<Animator>().Play("door close", 0, 0f);
        door.GetComponent<AudioSource>().Play();
        yield break;

    }
}

