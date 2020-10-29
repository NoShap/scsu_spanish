using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    // The target marker.

    public void turnToCamera(Camera target, GameObject turner)
    {
        //use this
        // float currX = turner.transform.rotation.eulerAngles.x;
        // float currZ = turner.transform.rotation.eulerAngles.z;
        // float targetY = target.transform.eulerAngles.y;

        //and
        // turner.transform.rotation = Quaternion.Euler(currX, targetY + 180, currZ);
        //or
        //turner.transform.Rotate(currX, targetY + 180, currZ);
    }
}