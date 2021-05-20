using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScoreControl : MonoBehaviour
{
    public GameSystem gameSystem;
    public Text TimeRemain;
    public Text textScore;
    public Text textTarget;
    public Text Countdown;
    public GameObject timeProgress;
    public Text TimeHeader;
    public List<Sprite> xMarks;
    public Image X1;
    public Image X2;
    public Image X3;
    public Image X4;
    public Image X5;

    //TimeProgress
    private System.TimeSpan time;
    private float minimumTime = 0;
    private float maximumTime;
    private float currentTime;
    public Image maskTime;
    //ScoreProgress
    private float minimumScore =0;
    private float maximumScore;
    private float currentScore;
    public Image maskScore;

    //BGM
    public GameObject BGM = null;


    private bool isPlayingBGM = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        maximumTime = gameSystem.timeRemain;
        maximumScore = gameSystem.passScore;
        textScore.text = "0";
        textTarget.text = "/" + gameSystem.passScore.ToString("#,#");
        if (gameSystem.currentLevel == gameSystem.lastLevel)
        {
            timeProgress.SetActive(false);
            TimeRemain.gameObject.SetActive(false);
            TimeHeader.gameObject.SetActive(false);
            textTarget.gameObject.SetActive(false);
            X1.gameObject.SetActive(true);
            X1.sprite = xMarks[0];
            X2.gameObject.SetActive(true);
            X2.sprite = xMarks[0];
            X3.gameObject.SetActive(true);
            X3.sprite = xMarks[0];
            X4.gameObject.SetActive(true);
            X4.sprite = xMarks[0];
            X5.gameObject.SetActive(true);
            X5.sprite = xMarks[0];
        }
        else
        {
            timeProgress.SetActive(true);
            TimeRemain.gameObject.SetActive(true);
            TimeHeader.gameObject.SetActive(true);
            textTarget.gameObject.SetActive(true);
            X1.gameObject.SetActive(false);
            X2.gameObject.SetActive(false);
            X3.gameObject.SetActive(false);
            X4.gameObject.SetActive(false);
            X5.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameSystem.IsStartCountDown)
        {
            if (((int)gameSystem.timeToStartCountDown) >= 4)
            {
                Countdown.text = "เตรียมตัว";
            }
            else if(((int)gameSystem.timeToStartCountDown)==0)
            {
                Countdown.text = "เริ่มเกม";
            }
            else
            {
                Countdown.text = ((int)gameSystem.timeToStartCountDown).ToString();
            }
        }
        else
        {
            Countdown.text = "ด่านที่ " + gameSystem.currentLevel;
            TimeRemain.text = ((int)gameSystem.timeRemain).ToString();
            SetNumberText(textScore, gameSystem.levelScore);
            textTarget.text = "/" + gameSystem.passScore.ToString();
            if (BGM != null)
            {
                if (!isPlayingBGM)
                {
                    BGM.gameObject.GetComponent<AudioSource>().Play();
                    isPlayingBGM = true;
                }
                if (((int)gameSystem.timeRemain)<=0)
                {
                    BGM.gameObject.GetComponent<AudioSource>().loop = false;
                    BGM.gameObject.GetComponent<AudioSource>().volume -= 0.003f;

                }
            }
        }
        switch (gameSystem.deathCount)
        {
            case 0:
                break;

            case 1:
                X1.sprite = xMarks[1];
                break;

            case 2:
                X2.sprite = xMarks[1];
                break;

            case 3:
                X3.sprite = xMarks[1];
                break;

            case 4:
                X4.sprite = xMarks[1];
                break;

            case 5:
                X5.sprite = xMarks[1];
                break;

            default:
                break;
        }

        //float to time
        time = System.TimeSpan.FromSeconds(gameSystem.timeRemain);
        TimeRemain.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00");

        //set progress
        GetCurrentFill(gameSystem.levelScore, minimumScore, maximumScore,maskScore);
        GetCurrentFill(gameSystem.timeRemain, minimumTime, maximumTime, maskTime);
    }

    void GetCurrentFill(float current,float minimum,float maximum,Image mask)
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillamount = currentOffset / maximumOffset;
        mask.fillAmount = fillamount;
    }

    void SetNumberText(Text text, float number)
    {
        if (number == 0)
            text.text = number.ToString();
        else
            text.text = number.ToString("#,#.##");
    }
}
