﻿using System.Collections;
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
        dialogue2, // guard talking to you
        voiceOver2, //explaining checkpoints and tasks
        task1, // get food for Sanchez Mazas 
        voiceOver3, //explaining languageObserver
        //dialogue with food delivery guard
        dialogue3, //talk with Food Stand Man 
        task2, // pick up food and receive directive to return to SM
        event1, //watching prisoners be escorted out
        dialogue4, //tell Sanchez Mazas to get up and move outside
        event2, // Sanchez Mazas runs away and player yells stop
        voiceOver4, //consequences
        fadeOut,
    }

    private GameObject UI;
    public stage currStage = stage.fadeIn;
    public GameObject checkpointPrefab;
    public GameObject Guard;
    private GameObject currCheckpoint;
    public GameObject door;
    private MazasController mazasController;
    public AudioManager audio_manager;
    private GameObject dialogueObject;
    private DialogueManager dialogueManager;
    public bool stageOpen = true;


    // Start is called before the first frame update
    void Start()
    {
        dialogueObject = GameObject.Find("DialogueManager");
        dialogueManager = dialogueObject.GetComponent<DialogueManager>();
        currStage = stage.voiceOver1; //should eventually be set to fadeIn
        mazasController = GameObject.Find("SanchezMazas").GetComponent<MazasController>();
        UI = GameObject.Find("UI");
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
            stageOpen = false;
            dialogueManager.startDialogue(0);
        }
        if (currStage == stage.dialogue2 && stageOpen)
        {
            stageOpen = false;
            var guard_mover = Guard.GetComponent<DestinationMove>();
            guard_mover.moveToDestination(guard_mover.target);
            dialogueManager.startDialogue(1);
        }
        // Stage 2
        if (currStage == stage.voiceOver2 && stageOpen) //beginning food delivery task
        {
            stageOpen = false;
            //display text
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
            print("Talk with food stand ");
            dialogueManager.startDialogue(2);
        }
        //having acquired the tray, go deliver it to sanchez mazas
        if (currStage == stage.task2 && stageOpen)
        {
            // stageOpen = false;
            UI.GetComponent<Text>().text = "\n Deliver the tray to Sanchez Mazas' prison cell.";
            //instantiate a checkpoint by Sanchez Mazas' Cell
            Quaternion rot = Quaternion.Euler(-1f, -28f, 0f);
            currCheckpoint = Instantiate(checkpointPrefab, new Vector3(134f, 5f, 110f), rot);
            currCheckpoint.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true)
            {
                Destroy(currCheckpoint);
                UI.GetComponent<Text>().text = "";
                currStage += 1;
                // stageOpen = true;
            }
        }
        if (currStage == stage.dialogue4)
        {
            stageOpen = false;
            if (currCheckpoint.GetComponent<CheckpointTrigger>().hasReached == true)
            {
                //insert dialogue
                StartCoroutine("GiveFood");
                UI.GetComponent<Text>().text = "\n Task Complete. You may now Explore the Map.";
                currStage += 1;
            }
        }

        //if (currStage == stage.event1)
        {
            //event1 watching prisoners be escorted out
        }

        if (currStage == stage.event2)
        {
          mazasController.startRunning()
          dialogueManager.startDialogue(3);
        }

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

