using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : AudioManager
{
    private Text UI;
    private int dialogueCounter = 0;
    public bool dialogueFinished = false;
    private bool isRunning = false;
    public bool playerSpeaking = true;
    public string[] conversationLines = { "Diga: Hola, ¿como está?",
    "\"Tengo hambre\"",
    "Pregúntele: ¿Que quiere que le traiga?",
    "\"Comería qualquier cosa\"",
    "Diga: Ok, le traigo una bandeja de comida"
    };

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<Text>();
    }

    IEnumerator waitForNPC(string name)
    {
        UI.text = "\n " + conversationLines[dialogueCounter];
        playerSpeaking = true;
        yield return waitForAudioClip(name);
    }
    IEnumerator waitForPlayer()
    {
        UI.text = "\n " + conversationLines[dialogueCounter];
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            //representative of the player speaking into the mic
            yield return null;
        }

        UI.text = "";
        playerSpeaking = false;
        yield return waitForAudioClip(sounds[dialogueCounter].name);
    }

    IEnumerator waitForAudioClip(string name)
    {
        Play(name);
        AudioClip aud = GetClip(name);
        yield return new WaitForSeconds(aud.length);
    }

    IEnumerator playNextLine()
    {
        isRunning = true;
        // print(dialogueCounter, dialogueCounter / 2, sounds.Length);
        if (dialogueCounter >= sounds.Length)
        {
            Debug.Log("Dialogue Sequence Finished");
            dialogueFinished = true;
            Debug.Log("Dialogue Sequence Finished");
            yield return null;
        }
        else if (playerSpeaking)
        {
            yield return waitForPlayer();
            dialogueCounter++;
        }
        else
        {
            yield return waitForNPC(sounds[dialogueCounter].name);
            dialogueCounter++;
        }
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("7"))
        {
            if (!isRunning) StartCoroutine(playNextLine());
        }
    }
}
