using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnIngredients : XRGrabInteractable
{
    public GameObject objectToSpawn;
    private GameObject spawnedObject;
    private XRBaseInteractable grip;

    void Start()
    {

    }

    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            spawnedObject = Instantiate(objectToSpawn, interactor.transform.position, interactor.transform.rotation);
            spawnedObject.transform.parent = interactor.transform;
            spawnedObject.GetComponent<Rigidbody>().isKinematic = true;
            //spawnedObject.GetComponent<MeshCollider>().enabled = true;
        }
        base.OnSelectEnter(interactor);
    }

    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            try
            {
                spawnedObject.transform.parent = null;
                spawnedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            catch
            {

            }

        }
        base.OnSelectExit(interactor);
    }
}