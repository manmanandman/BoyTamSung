using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerTutorial : Tutorial
{
    public GameSystem gameSystem;
    private bool complete = false;
    public GameObject button;
    public bool isFinish = false;
    private bool isPlaySong = false;
    public AudioSource source;
    public GameObject BGM;
    public override void CheckIfHappening()
    {
        if(isFinish)
        {
            gameSystem.pass = true;
            gameSystem.end = true;
            if(!isPlaySong)
            {
                source.Play();
                BGM.GetComponent<AudioSource>().Pause();
                isPlaySong = true;
            }
        }
        if (complete)
        {
            button.SetActive(false);
            TutorialManager.Instance.CompletedTutorial();
        }
        else
            button.SetActive(true);
    }

    public void SetComplete()
    {
        complete = true;
    }
}
