using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMe : MonoBehaviour
{
    //initialize a general animator variable (this will communicate with our animator window!)
    private Animator anim;
    public string animation;
    private AudioManager audio;
    
    void Start()
    {
        //store this gameobject's Animator in the variable we just initialized 
        anim = gameObject.GetComponent<Animator>();
        audio = FindObjectOfType<AudioManager>();
    }

   

    // Update is called once per frame
    void Update()
    {
        //Input.GetKet() takes in a string that contains the information 
        if (Input.GetKey("l")) { 

            StartCoroutine(fireRoutine());
        }
        // else
        // {
        //     anim.SetBool(animation, false);
        // }
        
    }

    IEnumerator fireRoutine()
    {
      audio.Play("Fuego");
      yield return new WaitForSeconds(2f);
      audio.Play("Volley");
      yield return new WaitForSeconds(0.1f);
      anim.SetBool(animation, true);
    }
}
