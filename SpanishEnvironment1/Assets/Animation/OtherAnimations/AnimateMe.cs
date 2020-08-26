using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMe : MonoBehaviour
{
    //initialize a general animator variable (this will communicate with our animator window!)
    private Animator anim;
    public string animation;
    
    void Start()
    {
        //store this gameobject's Animator in the variable we just initialized 
        anim = gameObject.GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {
        //Input.GetKet() takes in a string that contains the information 
        if (Input.GetKey("space")) { 
            anim.SetBool(animation, true);
        }
        // else
        // {
        //     anim.SetBool(animation, false);
        // }
        
    }
}
