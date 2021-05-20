using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultControl : MonoBehaviour
{
    public GameSystem gameSystem;
    public Sprite panelSuccess;
    public Sprite panelFail;
    public Text textPassOrFail;
    public Text textTotalScore;
    public Text textTargetScore;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;
    public GameObject BgmPass;
    public GameObject BgmFail;
    public GameObject buttonNextLevel;
    public SaveData saveData;
    public GameObject slash;

    private bool isPlayingResult = false;
    // Start is called before the first frame update
    void Start()
    {
        saveData.LoadJson();
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();

        if (gameSystem.currentLevel == gameSystem.lastLevel)
        {
            textTargetScore.gameObject.SetActive(false);
            slash.SetActive(false);
        }
        else
        {
            textTargetScore.gameObject.SetActive(true);
            slash.SetActive(true);
        }

        if ( !isPlayingResult )
        {
            if ( gameSystem.pass )
            {
                BgmPass.gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                BgmFail.gameObject.GetComponent<AudioSource>().Play();
            }
            isPlayingResult = true;
        }
        
        // set text header and background UI
        if ( gameSystem.pass && gameSystem.currentLevel + 1 < saveData._LevelInfos.levelInfo.Count)
        {
            buttonNextLevel.SetActive(true);
        }
        else //ปิดปุ่ม next ด่านที่แพ้ และด่านสุดท้าย
        {
            buttonNextLevel.SetActive(false);
        }
        if (gameSystem.pass)
        {
            textPassOrFail.GetComponent<Text>().text = "ผ่าน";
            this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = panelSuccess;
        }
        else
        {
            textPassOrFail.GetComponent<Text>().text = "ไม่ผ่าน";
            this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = panelFail;
        }

        // set score text
        SetNumberText(textTotalScore, gameSystem.levelScore);
        SetNumberText(textTargetScore, gameSystem.passScore);
    }

    // Update is called once per frame
    void Update()
    {
        score1.text = gameSystem.rawScore.ToString();
        score2.text = gameSystem.tipScore.ToString() + "\n" + gameSystem.comboScore.ToString();
        score3.text = gameSystem.correctServe.ToString();
        score4.text = gameSystem.failServe.ToString();
        score5.text = gameSystem.hitCount.ToString();
    }

    void SetNumberText(Text text,float number)
    {
        if(number==0)
            text.text = number.ToString();
        else
            text.text = number.ToString("#,#.##");
    }
}
