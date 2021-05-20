using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class spatulaControl : MonoBehaviour
{
    //public string tag;
    public float TimeDelayAfterGetObject = 5;

    private bool ItemInSpatula = false;
    private float rotationValue = 0;
    private float timeRemaining = 0;
    private float wait = 2;
    private bool CanGetItem = true;
    private float tempMass;
    public GameObject Changeposition;
    private GameObject ItemtoGet = null;


    // Update is called once per frame
    void Update()
    {
        if (ItemInSpatula)
        {
            timeRemaining -= 1 * Time.deltaTime;
        }
        if (!CanGetItem)
        {
            wait -= 1 * Time.deltaTime;
            if (wait < 0)
            {
                CanGetItem = true;
            }
        }
        if (ItemtoGet != null)
        {
            rotationValue = gameObject.transform.rotation.eulerAngles.x;
            if ((rotationValue > 360 || rotationValue < 150) && (ItemInSpatula == true) && (timeRemaining < 0))
            {
                ItemtoGet.transform.parent = null;
                ItemtoGet.GetComponent<Rigidbody>().isKinematic = false;
                ItemtoGet.GetComponent<Rigidbody>().useGravity = true;
                timeRemaining = TimeDelayAfterGetObject;
                ItemInSpatula = false;
                enablegrab(true);
                ItemtoGet.layer = 8;
                wait = 1;
                CanGetItem = false;
                ItemtoGet.gameObject.GetComponent<Rigidbody>().mass = tempMass;
            }
        }
        else
        {
            timeRemaining = TimeDelayAfterGetObject;
            ItemInSpatula = false;
            wait = 1;
            CanGetItem = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((ItemInSpatula == false) && (CanGetItem) && (collision.gameObject.CompareTag("CanSpatular")))
        {
            Debug.Log("Spatula get " + collision.gameObject.name);
            //assign focus gameobject data
            ItemtoGet = collision.gameObject;

            //ItemtoGet.gameObject.transform.position = this.transform.position + Vector3.up*0.03f;
            ItemtoGet.gameObject.transform.position = Changeposition.transform.position;
            ItemtoGet.gameObject.transform.rotation = this.transform.rotation * Quaternion.Euler(90f, 0f, 0f);

            ItemtoGet.gameObject.transform.parent = this.transform;

            ItemtoGet.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ItemtoGet.gameObject.layer = 17;
            tempMass = ItemtoGet.gameObject.GetComponent<Rigidbody>().mass;
            ItemtoGet.gameObject.GetComponent<Rigidbody>().mass = 0;
            ItemtoGet.gameObject.GetComponent<Rigidbody>().useGravity = false;
            enablegrab(false);
            timeRemaining = TimeDelayAfterGetObject;
            ItemInSpatula = true;
        }
        if ((collision.gameObject.layer == 9) && (ItemInSpatula == true)) // spatula fall to ground
        {
            ItemtoGet.transform.parent = null;
            ItemtoGet.GetComponent<Rigidbody>().isKinematic = false;
            ItemtoGet.GetComponent<Rigidbody>().useGravity = true;
            ItemtoGet.gameObject.GetComponent<Rigidbody>().mass = tempMass;
            enablegrab(true);
            ItemInSpatula = false;
            ItemtoGet.gameObject.layer = 8;
        }
    }

    void enablegrab(bool isenable) //still can grab but cannot move / rotate  object
    {
        ItemtoGet.gameObject.GetComponent<XRGrabInteractable>().trackPosition = isenable;
        ItemtoGet.gameObject.GetComponent<XRGrabInteractable>().trackRotation = isenable;
    }
}
