using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTutorial : Tutorial
{
    public HandPresence Left;
    public override void CheckIfHappening()
    {
        GameManager.Instance.enablePause = true;
        if (Left.menubutton)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
    }
}
