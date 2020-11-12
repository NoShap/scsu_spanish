using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// takes array of Materials and cycles gameObject that has the script through x specified materials per second
public class cycleMaterials : MonoBehaviour
{
    public Material[] frames;
    private float framesPerSecond = 1.0f;
    private float index;

    void Update() 
    {
        index = Time.time * framesPerSecond;
        index = index % frames.Length;
        gameObject.GetComponent<Renderer>().material = frames[(int)index];    
    }
}
