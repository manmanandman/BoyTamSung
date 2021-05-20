using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripTutorial : Tutorial
{
    public HandPresence Right;
    public HandPresence Left;
    public int pressTime;

    private bool startTutorial = false;
    private int leftGripPressedTimeStart;
    private int rightGripPressedTimeStart;
    public override void CheckIfHappening()
    {
        if((pressTime - (Right.gripPressedTime - rightGripPressedTimeStart)) <= 0)
            Explanation = "กด Grip มือซ้าย " + (pressTime - (Left.gripPressedTime - leftGripPressedTimeStart)).ToString() + " ครั้ง";

        else if ((pressTime - (Left.gripPressedTime - leftGripPressedTimeStart)) <= 0)
            Explanation = "กด Grip มือขวา " + (pressTime - (Right.gripPressedTime - rightGripPressedTimeStart)).ToString()+ " ครั้ง";
        
        else
            Explanation = "กด Grip มือขวา " + (pressTime - (Right.gripPressedTime - rightGripPressedTimeStart)).ToString()
            + " ครั้ง\nกด Grip มือซ้าย " + (pressTime - (Left.gripPressedTime - leftGripPressedTimeStart)).ToString() + " ครั้ง";

        if (!startTutorial)
        {
            startTutorial = true;
            leftGripPressedTimeStart = Left.gripPressedTime;
            rightGripPressedTimeStart = Right.gripPressedTime;
        }
        if ( (Right.gripPressedTime - rightGripPressedTimeStart >= pressTime) && (Left.gripPressedTime - leftGripPressedTimeStart >= pressTime))
            TutorialManager.Instance.CompletedTutorial();
    }
}
