using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWithRiceTutorial : Tutorial
{
    public RiceControl riceControl;
    public override void CheckIfHappening()
    {
        riceControl.NumberOfRice = 3;
        if (GameObject.Find("DishWithRice(Clone)"))
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
