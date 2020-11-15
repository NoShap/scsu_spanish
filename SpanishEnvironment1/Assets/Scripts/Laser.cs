using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lr;
    public Color c1 = Color.yellow;
    Gradient gradient = new Gradient();
    Gradient gradient2 = new Gradient();
    // Start is called before the first frame update
    void Start()
    {

        gradient2.SetKeys(
           new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.green, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }

        );
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },

            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
        );
        lr.colorGradient = gradient;
        //lr.SetColors()
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.GetComponent<LanguageObserverTarget>() != null)
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
                lr.colorGradient = gradient2;
                print(hit.collider.name);
            }
            else
            {
                lr.colorGradient = gradient;
                lr.SetPosition(1, new Vector3(0, 0, 5000));
            }
        }
        else
        {
            lr.colorGradient = gradient;
            lr.SetPosition(1, new Vector3(0, 0, 5000));
        }


    }
}
