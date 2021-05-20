using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddFriedEggControl : MonoBehaviour
{
    public string foodName;
    public GameObject KaPraoEggNoRice;

    private GameObject spawnObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Egg_State") && this.enabled == true)
        {
            //Debug.Log("Get Food >> " + this.gameObject.name);
            if (gameObject.name.Equals("KaPrao"))
            {
                if (collision.gameObject.name == "Egg_State1(Clone)")
                {
                    this.gameObject.name = gameObject.name + "Egg_State1(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State2(Clone)")
                {
                    this.gameObject.name = gameObject.name + "Egg_State2(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State3(Clone)")
                {
                    this.gameObject.name = gameObject.name + "Egg_State3(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State4(Clone)")
                {
                    this.gameObject.name = gameObject.name + "Egg_State4(Clone)";
                }
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = collision.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
                gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = collision.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                spawnObject = Instantiate(KaPraoEggNoRice, gameObject.transform.position, gameObject.transform.rotation);
                if (collision.gameObject.name == "Egg_State1(Clone)")
                {
                    spawnObject.gameObject.name = "KaPraoEggNoRice_State1(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State2(Clone)")
                {
                    spawnObject.gameObject.name = "KaPraoEggNoRice_State2(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State3(Clone)")
                {
                    spawnObject.gameObject.name = "KaPraoEggNoRice_State3(Clone)";
                }
                else if (collision.gameObject.name == "Egg_State4(Clone)")
                {
                    spawnObject.gameObject.name = "KaPraoEggNoRice_State4(Clone)";
                }
                try
                {
                    gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
                }
                catch
                {
                    gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
                }

                spawnObject.transform.GetChild(0).GetComponent<Renderer>().material = collision.transform.GetChild(0).GetComponent<Renderer>().material;
                spawnObject.transform.GetChild(1).GetComponent<Renderer>().material = collision.transform.GetChild(1).GetComponent<Renderer>().material;
                Destroy(gameObject);
                
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
            this.enabled = false;
        }
    }
}
