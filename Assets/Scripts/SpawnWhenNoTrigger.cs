using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnWhenNoTrigger : MonoBehaviour
{
    public GameObject ObjectToRespawn;
    public float timeToCountdown = 5f;
    public float DistanceThreshold = 5f;
    public bool DestroyOldObject = false;
    public bool dish;

    private float timeRemaining;
    private GameObject spawnedObject;
    private bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeToCountdown;
        if (dish)
        {
            spawnedObject = Instantiate(ObjectToRespawn, this.transform);
        }
        else
        {
            spawnedObject = Instantiate(ObjectToRespawn, this.transform.position + Vector3.down * 0.1f, this.transform.rotation);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTrigger)
        {
            timeRemaining -= 1 * Time.deltaTime;
            if (timeRemaining < 0)
            {
                spawnedObject = Instantiate(ObjectToRespawn, this.transform.position + Vector3.down * 0.1f, this.transform.rotation);
                spawnedObject.GetComponent<Rigidbody>().useGravity = true;
                timeRemaining = timeToCountdown;
            }
        }
        else
        {
            timeRemaining = timeToCountdown;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dish)
        {
            if (collision.gameObject.name.Contains("Dish(Clone)") && collision.gameObject.name.Length < 14)
            {
                isTrigger = true;
            }
            else
            {
                isTrigger = false;
            }
        }
        else
        {
            if (collision.gameObject.name.Contains("Bowl(Clone)") || collision.gameObject.name.Contains("BowlofTomYumKung"))
            {
                isTrigger = true;
            }
            else
            {
                isTrigger = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (dish)
        {
            if (collision.gameObject.name.Contains("Dish(Clone)") && collision.gameObject.name.Length < 14)
            {
                isTrigger = true;
            }
        }
        else
        {
            if (collision.gameObject.name.Contains("Bowl(Clone)") || collision.gameObject.name.Contains("BowlofTomYumKung"))
            {
                isTrigger = true;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (dish)
        {
            if (collision.gameObject.name.Contains("Dish(Clone)") && collision.gameObject.name.Length < 14)
            {
                isTrigger = false;
            }
        }
        else
        {
            if (collision.gameObject.name.Contains("Bowl(Clone)") || collision.gameObject.name.Contains("BowlofTomYumKung"))
            {
                isTrigger = false;
            }
        }
    }

}

