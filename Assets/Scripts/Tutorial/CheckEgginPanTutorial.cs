using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEgginPanTutorial : Tutorial
{
    public CheckEgginPan egginpan;

    public override void CheckIfHappening()
    {
        if (egginpan.EggInPan)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
