using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicHandTutorial : Tutorial
{
    public Canvas magichandLeft;
    public Canvas magichandRight;

    public override void CheckIfHappening()
    {
        if (magichandRight.enabled || magichandLeft.enabled)
        {
              TutorialManager.Instance.CompletedTutorial();
        }
    }
}
