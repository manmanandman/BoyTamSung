using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : MonoBehaviour
{
    public GameSystem gameSystem;
    public Text textTarget;
    public Text Countdown;
    public Text TimeRemain;
    private System.TimeSpan time;
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        Countdown.text = "ด่านที่ " + gameSystem.currentLevel;
        time = System.TimeSpan.FromSeconds(gameSystem.timeRemain);
        if (gameSystem.currentLevel == gameSystem.lastLevel)
        {
            textTarget.text = "ไม่จำกัด";
            TimeRemain.text = "ไม่จำกัด";
        }
        else
        {
            textTarget.text = gameSystem.passScore.ToString("#,#");
            TimeRemain.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");
        }
        GameManager.Instance.enablePause = true;
    }
}
