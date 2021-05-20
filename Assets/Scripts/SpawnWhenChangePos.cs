using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnWhenChangePos : MonoBehaviour
{
    public GameObject ObjectToRespawn;
    public float timeToCountdown = 5f;
    public float DistanceThreshold = 5f;
    public bool DestroyOldObject = false;
    public bool SetParentNull;

    private float timeRemaining;
    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeToCountdown;
        spawnedObject = Instantiate(ObjectToRespawn, this.transform.position, Quaternion.identity);
        spawnedObject.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null)
        {
            timeRemaining -= 1 * Time.deltaTime;
            if (timeRemaining < 0)
            {
                spawnedObject = Instantiate(ObjectToRespawn, this.transform.position, Quaternion.identity);
                spawnedObject.GetComponent<Rigidbody>().useGravity = true;
                spawnedObject.transform.parent = gameObject.transform;
                timeRemaining = timeToCountdown;
            }
        }
        else if (Vector3.Distance(this.transform.position, spawnedObject.transform.position) > DistanceThreshold)
        {
            timeRemaining -= 1 * Time.deltaTime;
            if (timeRemaining < 0)
            {
                spawnedObject =  Instantiate(ObjectToRespawn, this.transform.position , Quaternion.identity);
                spawnedObject.GetComponent<Rigidbody>().useGravity = true;
                timeRemaining = timeToCountdown;
                if (SetParentNull)
                    spawnedObject.transform.parent = null;
            }
        }

    }
}

