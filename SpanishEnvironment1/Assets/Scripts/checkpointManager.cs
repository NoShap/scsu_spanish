using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkpointManager : MonoBehaviour
{
    //need a new function to treat events like dialogues and voiceovers
    public enum stage
    {
        fadeIn, // screen goes from black to full brightness
        voiceOver1, //historical intro
        dialogue1, // talk with Sanchez Mazas 
        dialogue2, // guard talking to you
        voiceOver2, //explaining checkpoints and tasks
        task1, // get food for Sanchez Mazas 
        voiceOver3, //explaining languageObserver
        dialogue3, //talk with Food Stand Man 
        task2, // pick up food and receive directive to return to SM
        dialogue4, // Tell Sanchez Mazas to eat food
        event1, //watching prisoners be escorted out
        dialogue5, //Guard Tells you to get the prisoner
        dialogue6, // tell SM to get up and go outside (and move him outside)
        event2, // SM goes outside
        event3, // Sanchez Mazas runs away and player yells stop
        dialogue7, // Tell Sanchez Mazas to stop
        voiceOver4, //consequences
        fadeOut,
    }

    private GameObject UI;
    public stage currStage = stage.event2;
    public GameObject checkpointPrefab;
    public GameObject Guard;
    private Animator guardAnim;
    private GameObject currCheckpoint;
    public GameObject door;
    private MazasController mazasController;
    public AudioManager audio_manager;
    private GameObject dialogueObject;
    private DialogueManager dialogueManager;
    public bool stageOpen = true;
    private GameObject prisoners;


    // Start is called before the first frame update
    void Start()
    {
        prisoners = GameObject.Find("Prisoners");
        dialogueObject = GameObject.Find("DialogueManager");
        dialogueManager = dialogueObject.GetComponent<DialogueManager>();
        // currStage = stage.voiceOver1; //should eventually be set to fadeIn
        mazasController = GameObject.Find("SanchezMazas").GetComponent<MazasController>();
        UI = GameObject.Find("UI");
        guardAnim = Guard.GetComponent<Animator>();
    }



    // State Machine controlling Player Progress throughout Tasks
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
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n Sanchez Mazas looks like he needs to speak to you. Press 7 to listen, and 7 to continue a conversation.";
            dialogueManager.startDialogue(0);
        }
        if (currStage == stage.dialogue2 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n A guard needs to talk to you. Press 7 to listen.";
            var guard_mover = Guard.GetComponent<DestinationMove>();
            guard_mover.moveToDestination(guard_mover.target);
            dialogueManager.startDialogue(1);
            guardAnim.SetBool("Talking", true);
        }
        // Stage 2
        if (currStage == stage.voiceOver2 && stageOpen) //beginning food delivery task
        {
            guardAnim.SetBool("Talking", false);
            stageOpen = false;
            // Display text
            UI.GetComponent<Text>().text = "\n Proceed to the Food Pickup Area outside the prison.";
            //instantiate a checkpoint prefab at position and rotation rot. then transform it to the appropriate size.
            Quaternion rot = Quaternion.Euler(0, -34.2f, 0);
            currCheckpoint = Instantiate(checkpointPrefab, new Vector3(119.81f, 5.1622f, 148.78f), rot);
            currCheckpoint.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            StartCoroutine(waitForAudioClip("VoiceOver2"));
        }
        if (currStage == stage.task1 && stageOpen)
        {
            //if player has reached the checkpoint, destroy this instance of the checkpoint prefab and display new instructions
            if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true)
            {
                Destroy(currCheckpoint);
                UI.GetComponent<Text>().text = "";
                currStage += 1;
            }
        }

        //VO: good work for completing the task. currently explains the languageobserver tool again
        if (currStage == stage.voiceOver3 && stageOpen)
        {
            stageOpen = false;
            StartCoroutine(waitForAudioClip("VoiceOver3"));
        }
        if (currStage == stage.dialogue3 && stageOpen)
        {
            stageOpen = false;
            print("Talk with f ood stand ");
            UI.GetComponent<Text>().text = "\n Press 7 to speak with the guard on kitchen duty.";
            dialogueManager.startDialogue(2);
        }
        // having acquired the tray, go deliver it to sanchez mazas
        if (currStage == stage.task2 && stageOpen)
        {
            //stageOpen = false;
            UI.GetComponent<Text>().text = "\n Deliver the tray to Sanchez Mazas' prison cell.";
            //instantiate a checkpoint by Sanchez Mazas' Cell
            Quaternion rot = Quaternion.Euler(-1f, -28f, 0f);
            currCheckpoint = Instantiate(checkpointPrefab, new Vector3(134f, 5f, 110f), rot);
            currCheckpoint.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            currStage += 1;

        }

        if (currStage == stage.dialogue4 && stageOpen)
        {
            if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true)
            {
                stageOpen = false;
                StartCoroutine("GiveFood");
                dialogueManager.startDialogue(3);
                UI.GetComponent<Text>().text = "\n Task Complete. Press 7 to speak with Sanchez Mazas.";
            }
        }
        // event1 watching prisoners be escorted out
        if (currStage == stage.event1 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n The prisoners are being moved outside.";
            var guard_mover = Guard.GetComponent<DestinationMove>();
            guard_mover.moveToDestination(guard_mover.target);
            prisoners.GetComponent<MoveOut>().moveOutChildren(prisoners.transform);
            currStage += 1;
            stageOpen = true;
        }
        //Guard Tells you to get the prisoner
        if (currStage == stage.dialogue5 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n A guard has a command for you. Press 7 to speak with him.";
            dialogueManager.startDialogue(4);
        }
        // tell Sanchez Mazas to get up
        if (currStage == stage.dialogue6 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n Press 7 to relay your command to Sanchez Mazas.";
            dialogueManager.startDialogue(5);
            UI.GetComponent<Text>().text = "\n Press 7 to let Sanchez Mazas out of the cell.";
            mazasController.getUp();
        }
        // Sanchez Mazas goes outside
        if (currStage == stage.event2 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n Follow Sanchez Mazas outside.";
            mazasController.nextDestination();
            Debug.Log("waiting");
            waitForTime(10f);
        }

        // Sanchez Mazas runs away
        if (currStage == stage.event3 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n Chase after Sanchez Mazas!";
            mazasController.startRunning();
            UI.GetComponent<Text>().text = "\n Press 7 to tell Sanchez Mazas to stop running.";
            dialogueManager.startDialogue(6);
        }
        //tell Sanchez Mazas to stop
        if (currStage == stage.dialogue7 && stageOpen)
        {
            stageOpen = false;
            UI.GetComponent<Text>().text = "\n Press 7 to tell Sanchez Mazas to stop running.";
            dialogueManager.startDialogue(6);
        }
        if (currStage == stage.voiceOver4 && stageOpen)
        {
            StartCoroutine(waitForAudioClip("VoiceOver4"));
        }
        if (currStage == stage.fadeOut)
        {
            mazasController.startRunning();
            dialogueManager.startDialogue(3);
            //fadeOut scene into conclusion scene
        }

        print(currStage);
    }

    IEnumerator waitForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("done waiting");
        currStage += 1;
        stageOpen = true;
    }

    IEnumerator waitForAudioClip(string name)
    {
        audio_manager.Play(name);
        AudioClip aud = audio_manager.GetClip(name);
        yield return new WaitForSeconds(aud.length + 1f);
        currStage += 1;
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

