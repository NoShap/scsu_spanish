using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Text uI;
    private AudioManager audioManager;

    private int conversationCounter = 0;
    private string[] conversationLines = { "Diga: Hola, ¿como está?", "\"Tengo hambre\"", "Pregúntele: ¿Que quiere que le traiga?", "\"Comería qualquier cosa\"", "Diga: Ok, le traigo una bandeja de comida" };
    private string[] audioLines = { "d0", "d1", "d2", "d3", "d4" };
    public static bool dialogueFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        uI = GameObject.Find("UI").GetComponent<Text>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            StartCoroutine(conversate1());
        }
    }

    IEnumerator conversate1()
    {
        if (conversationCounter < conversationLines.Length)
        {
            uI.text = "\n " + conversationLines[conversationCounter];
            yield return new WaitForSeconds(1f);
            audioManager.Play(audioLines[conversationCounter]);
            conversationCounter++;
        }
        else
        {
            uI.text = "";
            dialogueFinished = true;
        }
    }
}
