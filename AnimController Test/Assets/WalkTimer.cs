using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTimer : MonoBehaviour
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
    }

  
    // Update is called once per frame
    void Update()
    {


    }
}
