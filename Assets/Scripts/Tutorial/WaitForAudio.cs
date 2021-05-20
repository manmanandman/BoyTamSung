using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAudio : Tutorial
{
    private bool complete = false;
    private bool starPlayAudio = false;
    public AudioSource source;
    public AudioClip VO0;
    public AudioClip VO1;
    public SaveData saveData;
    public override void CheckIfHappening()
    {
        if (complete)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
        if(!starPlayAudio)
        {
            starPlayAudio = true;
            StartCoroutine(PlaySound(2f));
        }
    }

    IEnumerator PlaySound(float sec)
    {
        yield return new WaitForSeconds(sec);
        if(saveData._LevelInfos.passTutorial == false)
        {
            source.PlayOneShot(VO0);
            yield return new WaitForSeconds(VO0.length);
        }
        source.PlayOneShot(VO1);
        yield return new WaitForSeconds(VO1.length);
        complete = true;
    }
}
