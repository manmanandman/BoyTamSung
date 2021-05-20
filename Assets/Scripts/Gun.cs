using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : SpecialObject
{
    public float speed = 40f;
    public GameObject bullet;
    public Transform barrel;
    public AudioClip shotSound;

    public void Fire()
    {
        GameObject spawedBullet = Instantiate(bullet,barrel.position,barrel.rotation);
        spawedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        source.PlayOneShot(shotSound);
        Destroy(spawedBullet, 3);
    }

}
