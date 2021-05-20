using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    public GameObject pan;
    public GameObject pot;
    public GameObject spatular;
    public GameObject ladle;
    public GameObject ricecooker;
    
    public void SpawnPan()
    {
        Instantiate(pan, transform.position, Quaternion.identity);
    }
    public void SpawnPot()
    {
        Instantiate(pot, transform.position, Quaternion.identity);
    }
    public void SpawnSpatular()
    {
        Instantiate(spatular, transform.position, Quaternion.identity);
    }
    public void SpawnLadle()
    {
        Instantiate(ladle, transform.position, Quaternion.identity);
    }
    public void SpawnRicecooker()
    {
        Instantiate(ricecooker, transform.position, Quaternion.identity);
    }
}
