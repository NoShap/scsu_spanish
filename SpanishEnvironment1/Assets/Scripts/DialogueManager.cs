using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogues;
    private bool isRunning = false;
    private void Start()
    {
        foreach (GameObject d in dialogues)
        {
            d.SetActive(false);
        }
    }
    public IEnumerator playActiveDialogue(int dialogueIndex)
    {
        isRunning = true;
        dialogues[dialogueIndex].SetActive(true);
        while (!dialogues[dialogueIndex].GetComponent<Dialogue>().dialogueFinished)
        {
            yield return null;
        }
        print("Dialogue Completed, moving to next dialogue.");
        isRunning = false;
    }

    public void startDialogue(int dialogueIndex)
    {
        StartCoroutine(playActiveDialogue(dialogueIndex));
    }

    void Update()
    {
        if (Input.GetKey("5"))
        {
            if (!isRunning) StartCoroutine(playActiveDialogue(0));
        }

    }

}
