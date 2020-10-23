using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private CheckpointTrigger cp;
    // Start is called before the first frame update
    void Start()
    {
        //cp = GetComponent<ChangeScene>();
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("HD_Forest");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
