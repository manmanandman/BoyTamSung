using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHitCheck : MonoBehaviour
{
    public bool hit;
    public NpcController npcCon;

    private void Start()
    {
        hit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (npcCon.npcState == "WAITING")
        {
            hit = true;
        }
    }
}
