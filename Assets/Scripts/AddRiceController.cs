using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddRiceController : MonoBehaviour
{
    public GameObject KaPrao;
    private GameObject spawnedObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Rice") && this.enabled)
        {
            if (gameObject.name.Contains("DishKaPrao"))
            {
                spawnedObject = Instantiate(KaPrao, gameObject.transform.position, gameObject.transform.rotation);
                spawnedObject.gameObject.name = "KaPrao";
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
                this.enabled = false;
            }
            else if (gameObject.name.Contains("KaPraoEggNoRice"))
            {
                spawnedObject = Instantiate(KaPrao, gameObject.transform.position, gameObject.transform.rotation);
                if (this.gameObject.name.Contains("State1(Clone)"))
                {
                    spawnedObject.gameObject.name = "KaPraoEgg_State1(Clone)";
                }
                else if (this.gameObject.name.Contains("State2(Clone)"))
                {
                    spawnedObject.gameObject.name = "KaPraoEgg_State2(Clone)";
                }
                else if (this.gameObject.name.Contains("State3(Clone)"))
                {
                    spawnedObject.gameObject.name = "KaPraoEgg_State3(Clone)";
                }
                else if (this.gameObject.name.Contains("State4(Clone)"))
                {
                    spawnedObject.gameObject.name = "KaPraoEgg_State4(Clone)";
                }

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
                spawnedObject.transform.GetChild(0).gameObject.SetActive(true);
                spawnedObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
                spawnedObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material;
                spawnedObject.transform.GetChild(1).gameObject.SetActive(true);
                Destroy(gameObject);
                this.enabled = false;
            }
        }
    }
}
