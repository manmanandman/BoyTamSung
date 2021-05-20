using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTutorial : Tutorial
{
    public GameSystem gameSystem;
    public QueueCheck queueCheck;
    private bool spawned = false;

    public override void CheckIfHappening()
    {
        if (gameSystem.levelScore >= 5)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
        if (!spawned)
        {
            spawned = true;
            queueCheck.SpawnOneNpc();
        }
        if (gameSystem.levelScore <= -5)
        {
            gameSystem.levelScore = 0;
            spawned = false;
        }
    }
}
