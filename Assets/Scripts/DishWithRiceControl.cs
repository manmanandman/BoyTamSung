using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DishWithRiceControl : MonoBehaviour
{
    public GameObject FriedEggWithRice;
    public GameObject KapraoWithRice;
    //private int dishstate = 0;
    private GameObject spawnedObject;
    bool enable = true;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {

        //======================== Fried Egg With Rice ========================
        if (collision.gameObject.name.Contains("Egg_State") && enable)
        {

            spawnedObject = Instantiate(FriedEggWithRice, gameObject.transform.position, gameObject.transform.rotation);
            if (collision.gameObject.name == "Egg_State1(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State1(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State2(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State2(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State3(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State3(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State4(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithRice_State4(Clone)";
            }

            spawnedObject.transform.GetChild(2).GetComponent<Renderer>().material = collision.transform.GetChild(1).GetComponent<Renderer>().material;
            spawnedObject.transform.GetChild(3).GetComponent<Renderer>().material = collision.transform.GetChild(0).GetComponent<Renderer>().material;
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
            enable = false;
        }

        //======================== KaPrao With Rice ========================
        if (collision.gameObject.name == "KaPraoPan(Clone)" && enable && collision.gameObject.GetComponent<IngredientControl>().isBurned == false) 
        {
            spawnedObject = Instantiate(KapraoWithRice, gameObject.transform.position, KapraoWithRice.transform.rotation);
            spawnedObject.gameObject.name = "KaPrao";
            Debug.Log("Get Food >> " + spawnedObject.gameObject.name);
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
            enable = false;
        }
    }

}
