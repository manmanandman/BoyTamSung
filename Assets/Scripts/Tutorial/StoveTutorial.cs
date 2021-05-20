using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTutorial : Tutorial
{
    public GasStoveControl gasStoveControl;
    public GasStoveControl gasStoveControl2;
    public override void CheckIfHappening()
    {
        if (gasStoveControl.IsOnFire == 2 || gasStoveControl2.IsOnFire == 2)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
