using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    public GameObject target;
  

    //creating the actions that you want to be in your project
    //private SteamVR_Action_Single squeezeAction;

    public SteamVR_Action_Vector2 touchPadAction;

    public SteamVR_Action_Boolean sideTouch;
    public SteamVR_Action_Boolean triggerPull;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set the values properly to each action
        //  float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.Any);

        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        bool sideTouched = sideTouch.GetState(SteamVR_Input_Sources.Any);

        bool triggerPulled = triggerPull.GetState(SteamVR_Input_Sources.Any);

        /*   
       if (triggerValue > 0)
        {   
           print(triggerValue);
        }*/

        if (touchPadValue != Vector2.zero)
        {
            float addX = touchPadValue.x * 0.1f;
            float addY = touchPadValue.y * 0.1f;
            //have the target object affected
           target.transform.localScale += new Vector3(addX, addY, 0);

           print(touchPadValue);
        }


        target.SetActive(!sideTouched);

        if (sideTouched)
        {
            print("Xavier, you thot");
            
        }

        if (triggerPulled)
        {
            print("Noah, you genius");

        }
    }
}
