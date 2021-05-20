using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanControl : MonoBehaviour
{
    public GameObject GasStoveControlObject;
    public float HeatPan = 0;

    public AudioSource source;
    public AudioClip HotPanSound;
    public Canvas canvas;
    public Canvas TumYumKung;
    public GameSystem gameSystem;
    //private int fireLevel=0;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        canvas = gameObject.GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //source.PlayOneShot(HotPanSound);
        //print(HeatPan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name.Contains("Egg_State"))
        //{
        //    collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Left Hand" || other.gameObject.name == "Right Hand")
        {
            canvas.enabled = true;
            if(TumYumKung)
                TumYumKung.enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "GasStoveSnapZone")
        {
            //HeatPan = GasStoveControlObject.GetComponent<GasStoveControl>().IsOnFire;
            HeatPan = other.gameObject.GetComponentInParent<GasStoveControl>().IsOnFire;
        }
        if (other.gameObject.name == "Left Hand" || other.gameObject.name == "Right Hand")
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "GasStoveSnapZone")
        {
            HeatPan = 0;
        }
        if (other.gameObject.name == "Left Hand" || other.gameObject.name == "Right Hand")
        {
            canvas.enabled = false;
            if (TumYumKung)
                TumYumKung.enabled = false;
        }

    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    //if (collision.gameObject.name == "FireObject")
    //    //{
    //    //    Debug.Log("Off fire");

    //    //}
    //}
}
