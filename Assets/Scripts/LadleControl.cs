using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LadleControl : MonoBehaviour
{
    public GameObject objectToSpawn;
    public LayerMask mask;
    public float TimeDelayAfterGetRice = 5;
    public RiceControl RiceControl;
    public GameObject RiceDetailCanvas;
    private float timeForCanvas = 2f;
    private GameObject spawnedObject;
    private bool riceIsInLadle = false;
    private float rotationValue = 0;
    private float timeRemaining = 0;
    private float wait = 2;
    private bool canGetRice = true;
    // Update is called once per frame
    void Update()
    {
        if(riceIsInLadle)
        { 
            timeRemaining -= 1 * Time.deltaTime;
        }
        if(!canGetRice)
        {
            wait -= 1 * Time.deltaTime;
            if (wait<0)
            {
                canGetRice = true;
            }
        }

        if( timeForCanvas > 0 )
            timeForCanvas -= Time.deltaTime;
        else
            RiceDetailCanvas.SetActive(false);

        if (spawnedObject != null)
        {
            rotationValue = gameObject.transform.rotation.eulerAngles.x;
            if ((rotationValue > 450 || rotationValue < 90) && (riceIsInLadle == true) && (timeRemaining < 0))
            {
                spawnedObject.transform.parent = null;
                spawnedObject.GetComponent<Rigidbody>().isKinematic = false;
                spawnedObject.GetComponent<Rigidbody>().useGravity = true;
                timeRemaining = TimeDelayAfterGetRice;
                riceIsInLadle = false;         
                spawnedObject.layer = 8;
                wait = 1;
                canGetRice = false;
                enablegrab(true);
            }
        }
        else
        {
            timeRemaining = TimeDelayAfterGetRice;
            riceIsInLadle = false;
            wait = 1;
            canGetRice = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((riceIsInLadle == false) && ((mask.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer))
        {
            RiceControl = collision.gameObject.GetComponent<RiceControl>();
            spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            spawnedObject.transform.parent = transform;
            spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
            spawnedObject.GetComponent<Rigidbody>().useGravity = false;
            timeRemaining = TimeDelayAfterGetRice;
            riceIsInLadle = true;
            RiceControl.NumberOfRice--;
            showRiceDetail();
            enablegrab(false);
            //RiceControl = null;
        }
        else if ((riceIsInLadle == false) && (canGetRice) && (collision.gameObject.CompareTag("Rice")))
        {
            spawnedObject = collision.gameObject;
            spawnedObject.gameObject.layer = 13;
            spawnedObject.gameObject.transform.position = transform.position;
            spawnedObject.gameObject.transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 0f);
            spawnedObject.gameObject.transform.parent = transform;
            spawnedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            spawnedObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
            enablegrab(false);
            timeRemaining = TimeDelayAfterGetRice;
            riceIsInLadle = true;
            //RiceControl = null;
        }
        if ((collision.gameObject.layer == 9) && (riceIsInLadle == true)) // ladle fall to ground
        {
            spawnedObject.transform.parent = null;
            spawnedObject.GetComponent<Rigidbody>().isKinematic = false;
            spawnedObject.GetComponent<Rigidbody>().useGravity = true;
            enablegrab(true);
            riceIsInLadle = false;
        }
    }

    void enablegrab(bool isenable)
    {
        spawnedObject.gameObject.GetComponent<XROffsetGrabInteractable>().enabled = isenable;
        //spawnedObject.gameObject.GetComponent<XROffsetGrabInteractable>().trackPosition = isenable;
        //spawnedObject.gameObject.GetComponent<XROffsetGrabInteractable>().trackRotation = isenable;
        //spawnedObject.gameObject.GetComponent<BoxCollider>().enabled = isenable;
    }

    void showRiceDetail()
    {
        if( RiceControl.NumberOfRice == 0 )
            RiceDetailCanvas.SetActive(false);
        else
            RiceDetailCanvas.SetActive(true);

        timeForCanvas = 2f;
        RiceDetailCanvas.GetComponent<PanProgressControl>().NextStateString = RiceControl.NumberOfRice.ToString() + " ครั้ง";
    }

}
