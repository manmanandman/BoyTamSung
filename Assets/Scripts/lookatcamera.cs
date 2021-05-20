using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatcamera : MonoBehaviour
{
    public Camera CameraToLookAt;
    void Awake()
    {
        CameraToLookAt = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(CameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(CameraToLookAt.transform.forward);
    }
}
