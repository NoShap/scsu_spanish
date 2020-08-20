using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTimer : MonoBehaviour
{
    public float numSeconds;
    public string triggername;
    private Animator anim;


    IEnumerator ChangeAnim()
    {
        if (!string.IsNullOrEmpty(triggername))
        {
            yield return new WaitForSeconds(numSeconds);
            anim.SetTrigger(triggername);
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
         
        anim = GetComponent<Animator>();
        StartCoroutine(ChangeAnim());
        //  anim.ResetTrigger(triggername);
        // //  foreach(AnimatorControllerParameter p in anim.parameters)
        // //      {
        // //         if (p.type == AnimatorControllerParameterType.Trigger)
        // //         anim.ResetTrigger(p.name);
        // //      }
    }

  
    // Update is called once per frame
    void Update()
    {


    }
}
