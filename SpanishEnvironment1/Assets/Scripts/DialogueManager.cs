using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogues;
    private bool isRunning = false;
    public checkpointManager checkpointManage;
    private void Start()
    {
        checkpointManage = GameObject.Find("UI").GetComponent<checkpointManager>();
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
        dialogues[dialogueIndex].SetActive(false);
        checkpointManage.stageOpen = true;
        checkpointManage.currStage += 1;
        isRunning = false;
        yield return null;
    }

    public void startDialogue(int dialogueIndex)
    {
        if (!isRunning)
        {
            StartCoroutine(playActiveDialogue(dialogueIndex));
        }
    }

    void Update()
    {
        if (Input.GetKey("5"))
        {
            if (!isRunning) StartCoroutine(playActiveDialogue(0));
        }

    }

}
