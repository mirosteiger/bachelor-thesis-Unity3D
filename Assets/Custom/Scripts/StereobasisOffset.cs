using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereobasisOffset : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            transform.Translate(new Vector3(-0.65f*0.5f, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(new Vector3(0.65f * 0.5f, 0, 0));
        }
    }
}
