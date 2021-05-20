using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignVRCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //this.GetComponent<Canvas>().worldCamera = GameObject.Find("VR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Canvas>().worldCamera == null)
            this.GetComponent<Canvas>().worldCamera = GameObject.Find("VR Camera").GetComponent<Camera>();
    }
}
