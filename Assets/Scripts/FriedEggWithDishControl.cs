using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FriedEggWithDishControl : MonoBehaviour
{
    public GameObject FriedEggWithRice;
    public GameObject KaPraoEggNoRice;

    private GameObject spawnedObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "RiceInLadle(Clone)")
        {
            try
            {
                collision.gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            }
            catch
            {
                collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            }  
            Destroy(collision.gameObject);
            try
            {
                gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            }
            catch
            {
                gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            }
            Destroy(gameObject);
            if(spawnedObject == null)
            { 
                spawnedObject = Instantiate(FriedEggWithRice, gameObject.transform.position, gameObject.transform.rotation);
            }
            if (gameObject.name == "FriedEggWithDish_State1(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State1(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State2(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State2(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State3(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State3(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State4(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State4(Clone)";
            }
            spawnedObject.transform.GetChild(2).GetComponent<Renderer>().material = transform.GetChild(2).GetComponent<Renderer>().material;
            spawnedObject.transform.GetChild(3).GetComponent<Renderer>().material = transform.GetChild(1).GetComponent<Renderer>().material;
        }
        if (collision.gameObject.name.Contains("KaPraoPan"))
        {
            try
            {
                collision.gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            }
            catch
            {
                collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            }
            Destroy(collision.gameObject);
            try
            {
                gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            }
            catch
            {
                gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            }
            Destroy(gameObject);
            spawnedObject = Instantiate(KaPraoEggNoRice, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + 90, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
            if (gameObject.name == "FriedEggWithDish_State1(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEggNoRice_State1(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State2(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEggNoRice_State2(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State3(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEggNoRice_State3(Clone)";
            }
            else if (gameObject.name == "FriedEggWithDish_State4(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEggNoRice_State4(Clone)";
            }
            spawnedObject.transform.GetChild(0).GetComponent<Renderer>().material = transform.GetChild(1).GetComponent<Renderer>().material;
            spawnedObject.transform.GetChild(1).GetComponent<Renderer>().material = transform.GetChild(2).GetComponent<Renderer>().material;
        }
    }

}
