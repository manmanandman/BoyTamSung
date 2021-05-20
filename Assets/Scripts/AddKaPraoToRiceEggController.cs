using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddKaPraoToRiceEggController : MonoBehaviour
{
    public GameObject KaPrao;

    private GameObject spawnedObject;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("KaPraoPan") && this.enabled == true)
        {
            spawnedObject = Instantiate(KaPrao, gameObject.transform.position, new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + 90, gameObject.transform.rotation.z, gameObject.transform.rotation.w));
            if (this.gameObject.name == "FriedEggWithRice_State1(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEgg_State1(Clone)";
            }
            else if (gameObject.name == "FriedEggWithRice_State2(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEgg_State2(Clone)";
            }
            else if (gameObject.name == "FriedEggWithRice_State3(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEgg_State3(Clone)";
            }
            else if (gameObject.name == "FriedEggWithRice_State4(Clone)")
            {
                spawnedObject.gameObject.name = "KaPraoEgg_State4(Clone)";
            }
            gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            spawnedObject.transform.GetChild(0).gameObject.SetActive(true);
            spawnedObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = gameObject.transform.GetChild(3).GetComponent<MeshRenderer>().material;
            spawnedObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().material;
            spawnedObject.transform.GetChild(1).gameObject.SetActive(true);
            this.enabled = false;
        }
    }
}
