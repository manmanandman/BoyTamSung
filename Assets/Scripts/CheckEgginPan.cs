using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEgginPan : MonoBehaviour
{

    public bool EggInPan = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Egg_State"))
        {
            EggInPan = true;
        }
    }
}
