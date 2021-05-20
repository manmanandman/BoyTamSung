using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTutorial : Tutorial
{
    public FryControl OilCheck;
    public AudioSource source;
    public AudioClip Extra3;
    private bool complete = false;
    private bool starPlayAudio = false;
    public override void CheckIfHappening()
    {
        if (OilCheck.isOil)
        {
            if(complete)
            {
                TutorialManager.Instance.CompletedTutorial();
            }
            if(!starPlayAudio)
            {
                starPlayAudio = true;
                StartCoroutine(PlaySound());
            }
        }
    }
    IEnumerator PlaySound()
    {
        source.Stop();
        source.PlayOneShot(Extra3);
        yield return new WaitForSeconds(Extra3.length);
        complete = true;
    }

}
