using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DestoryOnFloor : MonoBehaviour
{
    public LayerMask mask;
    public float timeToCountdown = 45f;
    public bool respawnOnDestroy = false;
    public float groundThreshold = -99f;
    public GameObject spawnlocation = null;

    public bool isOnFloor = false;
    private float timeRemaining;
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeToCountdown;
        initialAttachLocalPos = gameObject.transform.position;
        initialAttachLocalRot = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {    
        if(isOnFloor || gameObject.transform.position.y < groundThreshold)
        {
            timeRemaining -= 1 * Time.deltaTime;
            if(timeRemaining < 0)
            {
                if (gameObject != null)
                {
                    if (respawnOnDestroy)
                    {
                        Debug.Log("RESPAWN ON FLOOR >> " + gameObject.name);
                        if (spawnlocation)
                        {
                            transform.position = spawnlocation.transform.position;
                            transform.rotation = spawnlocation.transform.rotation;
                        }
                        else
                        {
                            transform.position = initialAttachLocalPos;
                            transform.rotation = initialAttachLocalRot;
                        }
                    }
                    else
                    {
                        Debug.Log("DestroyOnFloor >> " + gameObject.name);
                        try
                        {
                            gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
                        }
                        catch
                        {
                            gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
                        }
                        Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            timeRemaining = timeToCountdown;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
            //Debug.Log("in "+collision.gameObject.name);
        if ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            isOnFloor = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("out " + collision.gameObject.name);
        if ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            isOnFloor = false;
        }
    }
}
