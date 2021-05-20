using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameControl : MonoBehaviour
{
    public List<Material> photo = new List<Material>();
    int randomIndex;

    public Animator Openanimator;
    private bool enableAnimator = true;
    public AudioSource source;
    public AudioClip SwordBGM;
    public AudioClip LightSaberBGM;
    public AudioClip GunBGM;
    public GameObject Sword;
    public GameObject Gun;
    public GameObject LightSaber;

    private void Start()
    {
        randomIndex = Random.Range(0, photo.Count);
        var mats = this.GetComponent<MeshRenderer>().materials;
        mats[1] = photo[randomIndex];
        this.GetComponent<MeshRenderer>().materials = mats;
    }

    private void OnCollisionEnter(Collision collision)
    {
        randomIndex = Random.Range(0, 3);
        if (enableAnimator && randomIndex == 0)
        {
            Openanimator.enabled = true;
            enableAnimator = false;
            source.PlayOneShot(SwordBGM);
            Sword.SetActive(true);
        }
        else if (enableAnimator && randomIndex == 1)
        {
            Openanimator.enabled = true;
            enableAnimator = false;
            source.PlayOneShot(LightSaberBGM);
            LightSaber.SetActive(true);
        }
        else if (enableAnimator && randomIndex == 2)
        {
            Openanimator.enabled = true;
            enableAnimator = false;
            source.PlayOneShot(LightSaberBGM);
            Gun.SetActive(true);
        }
    }
}


