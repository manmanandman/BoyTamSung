using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CrackControl : MonoBehaviour
{
    public float magnitudeTreshold = 5;
    public float GroundThreshold = 5;
    public GameObject crackObject;
    public GameObject Egg;
    public LayerMask mask;
    public AudioSource source;
    public AudioClip Cracksound;

    private float totalmagnitude = 0;
    private bool isCrack = false;

    void Update()
    {
        if(isCrack)
        {
            Debug.Log("crack");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            //Debug.Log(collision.gameObject.name + "   " + collision.relativeVelocity.magnitude);
            totalmagnitude = totalmagnitude + collision.relativeVelocity.magnitude;
        }
        if (totalmagnitude > magnitudeTreshold && ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer))
        {
                if(!isCrack)
                {
                    //source.PlayOneShot(Cracksound);
                    isCrack = true;
                    Instantiate(Egg, gameObject.transform.position, Quaternion.identity);
                    Instantiate(crackObject, gameObject.transform.position, Quaternion.identity);
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
        if (collision.gameObject.layer == 9 && collision.relativeVelocity.magnitude > GroundThreshold)
        {
            if (!isCrack)
            {
                isCrack = true;
                Instantiate(Egg, gameObject.transform.position, Quaternion.identity);
                Instantiate(crackObject, gameObject.transform.position, Quaternion.identity);
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
