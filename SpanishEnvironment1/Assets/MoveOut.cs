using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void moveOutChildren(Transform obj)
    {
        if (obj.childCount > 0)
        {
            foreach (Transform child in obj)
            {
                if (child.GetComponent<PrisonerController>() != null && child.gameObject.activeSelf == true)
                {
                    // print(child.name);
                    child.GetComponent<PrisonerController>().moveOut();
                }
                else moveOutChildren(child);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
