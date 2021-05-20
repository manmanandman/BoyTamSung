using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : SpecialObject
{
    private Animator animatorLight;
    public GameObject Light;

    public AudioClip LightOnSound;
    // Start is called before the first frame update
    void Start()
    {
        Light = this.transform.GetChild(1).gameObject;
        animatorLight = Light.GetComponent<Animator>();
    }

    public void UseItem()
    {
        if(animatorLight.enabled)
        {
            bool isOn = animatorLight.GetBool("Light");

            if (!isOn)
            {
                //Light.SetActive(true);
                source.Play();
                animatorLight.SetBool("Light", true);
                source.PlayOneShot(LightOnSound);
            }
            else
            {
                //Light.SetActive(false);
                source.Pause();
                animatorLight.SetBool("Light", false);
            }
        }
        else
        {
            animatorLight.enabled = true;
            source.Play();
            source.PlayOneShot(LightOnSound);
        }

        //if (!Light.activeSelf)
        //{
        //    Light.SetActive(true);
        //    source.Play();
        //}
        //else
        //{
        //    Light.SetActive(false);
        //    source.Pause();
        //}
    }
    public void putDownItem()
    {
        //Light.SetActive(false);
        animatorLight.SetBool("Light", false);
        source.Pause();
    }

}
