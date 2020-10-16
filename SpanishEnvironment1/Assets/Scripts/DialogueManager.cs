using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogues;
    private int currActiveDialogue = 0;
    private bool isRunning = false;
    private void Start()
    {
        foreach (GameObject d in dialogues)
        {
            d.SetActive(false);
        }
    }
    IEnumerator playActiveDialogue()
    {
        isRunning = true;
        print("staging: " + currActiveDialogue);
        dialogues[currActiveDialogue].SetActive(true);
        print("set active:" + currActiveDialogue);
        while (!dialogues[currActiveDialogue].GetComponent<Dialogue>().dialogueFinished)
        {
            yield return null;
        }
        print("Dialogue Completed, moving to next dialogue.");
        isRunning = false;
    }

    void Update()
    {
        if (!isRunning) StartCoroutine(playActiveDialogue());
    }

}
