using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCollision : MonoBehaviour
{
    public bool isCol;
    void Start()
    {
        isCol = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "checkFront" || other.gameObject.name == "checkBack")
        {
            isCol = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCol = false;
    }
}
