using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPan : Tutorial
{
    private bool complete = false;
    public override void CheckIfHappening()
    {
        if(complete)
            TutorialManager.Instance.CompletedTutorial();
    }

    public void SetComplete()
    {
        complete = true;
    }
}
