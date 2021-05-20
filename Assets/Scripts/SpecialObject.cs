using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObject : MonoBehaviour
{
    public AudioSource source;
    public AudioClip SoundWhenSelect;
    public Animator animator;
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        if(SoundWhenSelect)
            source.clip = SoundWhenSelect;
        animator = GameObject.Find("room 1").gameObject.GetComponent<Animator>();
    }

    public void setNullParent()
    {
        //this.transform.parent = null;
        this.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.layer = 0;
        animator.SetBool("Close",true);
    }
}
