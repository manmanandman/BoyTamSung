using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float timeRemaining = 5;
    void Update()
    {
        timeRemaining -= 1 * Time.deltaTime;
        if (timeRemaining < 0)
        {
            if (gameObject != null)
            {
               Destroy(gameObject);
            }
        }
    }
}
