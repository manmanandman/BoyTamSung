using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DishControl : MonoBehaviour
{
    public GameObject DishWithRice;
    public GameObject DishWithFriedEgg;
    public GameObject DishKaPrao;
    //private int dishstate = 0;
    private GameObject spawnedObject;

    bool enableDish = true;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rice" && enableDish)
        {
            spawnedObject = Instantiate(DishWithRice, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            enableDish = false;        }
        if (collision.gameObject.name.Contains("Egg_State") && enableDish && collision.gameObject.name.Length < 18)
        {
            spawnedObject = Instantiate(DishWithFriedEgg, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            spawnedObject.transform.GetChild(1).GetComponent<Renderer>().material = collision.transform.GetChild(0).GetComponent<Renderer>().material;
            spawnedObject.transform.GetChild(2).GetComponent<Renderer>().material = collision.transform.GetChild(1).GetComponent<Renderer>().material;
            if (collision.gameObject.name == "Egg_State1(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithDish_State1(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State2(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithDish_State2(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State3(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithDish_State3(Clone)";
            }
            else if (collision.gameObject.name == "Egg_State4(Clone)")
            {
                spawnedObject.gameObject.name = "FriedEggWithDish_State4(Clone)";
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
            enableDish = false;
        }
        if (collision.gameObject.name.Contains("KaPraoPan") && enableDish)
        {
            spawnedObject = Instantiate(DishKaPrao, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + 90, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
            gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            enableDish = false;
        }
    }

}
