using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTutorial : Tutorial
{
    public GameSystem gameSystem;
    public QueueCheck queueCheck;
    public int count = 0;
    public override void CheckIfHappening()
    {
        Explanation = "เสิร์ฟอาหารให้ลูกค้า " + (5-count).ToString() + " คน";
        if (count == 5)
        {
            TutorialManager.Instance.CompletedTutorial();
        }
        queueCheck.SpawnMoreNpc();
        if (gameSystem.levelScore != 0)
        {
            if (gameSystem.levelScore >= 5)
            {
                count++;
            }
            gameSystem.levelScore = 0;
        }
    }
}
