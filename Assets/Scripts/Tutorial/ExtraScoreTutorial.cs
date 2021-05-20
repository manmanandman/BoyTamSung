using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScoreTutorial : Tutorial
{
    private bool complete = false;

    public GameObject SetCompletebutton;
    public GameObject DetailGameButton;
    public GameObject ExtraScoreButtn;
    public GameObject Canvas1;
    public GameObject Canvas2;
    public override void CheckIfHappening()
    {
        if (complete)
        {
            SetCompletebutton.SetActive(false);
            DetailGameButton.SetActive(false);
            ExtraScoreButtn.SetActive(false);
            Canvas1.SetActive(false);
            Canvas2.SetActive(false);
            TutorialManager.Instance.CompletedTutorial();
        }
        else
        {
            SetCompletebutton.SetActive(true);
            DetailGameButton.SetActive(true);
            ExtraScoreButtn.SetActive(true);
        }
    }

    public void SetComplete()
    {
        complete = true;
    }
}
