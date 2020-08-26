using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toFollow : MonoBehaviour {

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    //produces a range for your next value to be set
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
	// Use this for initialization
	void Start () {
		//set the camera offset Vector3 to be the current difference in position at start time
        _cameraOffset = transform.position - PlayerTransform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//have new position of where the camera ought to be (the position of the player + original offset)

        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        //slowly interpolate that new position depending on the smooth factor value
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
	}
}
