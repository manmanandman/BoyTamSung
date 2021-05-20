using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnPauseTutorial : Tutorial
{
    public override void CheckIfHappening()
    {
        if(Time.timeScale == 1)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
