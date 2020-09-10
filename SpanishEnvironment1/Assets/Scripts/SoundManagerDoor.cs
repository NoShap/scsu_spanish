using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerDoor : MonoBehaviour
{

	public AudioSource audio;
	public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource> ();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update() {
    	if (Input.GetKey("f")) {
    		anim.Play("door open", 0, 0f);
    		audio.PlayOneShot(audio.clip, 1f);
    		Debug.Log("Played");

    	}
    }

}
