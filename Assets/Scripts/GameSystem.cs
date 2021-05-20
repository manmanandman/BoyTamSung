using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public int currentLevel;

    public float levelScore;
    public float rawScore;
    public float tipScore;
    public float comboScore;
    public int correctServe;
    public int failServe;
    public int hitCount;
    public float timeRemain;
    public float passScore;
    public bool pass;
    public bool end;
    public Canvas resultCanvas;
    public Canvas nameHighScore;
    public Canvas pauseCanvas;
    public bool IsStartCountDown;
    public float timeToStartCountDown = 10;
    public GameObject NPCSpawner;
    public int combo;
    public SaveData saveData;
    public bool isInGameScene;
    public LevelLoader levelLoader;
    public bool random;
    public Text debugLog;
    private ArrayList levelDetail;
    private bool newHigh = false;
    public int deathCount;
    public int lastLevel;
    public AudioClip countdown;
    public HandPresence LeftHandPresence;

    public GameObject comboText;
    private float timeShowCombo = 5f;
    private float timeShowComboDecresing = 0f;
    private bool playCountdown = false;
    public List<AudioClip> comboSFX = new List<AudioClip>();
    void Awake()
    {
        if(debugLog)
            debugLog.text = null;
        saveData = GameObject.Find("LevelInfo").GetComponent<SaveData>();
        IsStartCountDown = false;
        currentLevel = GameManager.Instance.currentLevel;
        StartCoroutine(WaitForTransistionThenStopTime());
        levelDetail = LevelData.levelDetails[currentLevel];
        timeRemain = (float)levelDetail[1];
        levelScore = 0f;
        rawScore = 0f;
        tipScore = 0f;
        comboScore = 0f;
        correctServe = 0;
        failServe = 0;
        hitCount = 0;
        deathCount = 0;
        passScore = (float)levelDetail[0];
        pass = false;
        end = false;
        random = false;
        playCountdown = false;
        lastLevel = saveData._LevelInfos.levelInfo.Count - 1;
        if (currentLevel != 0)
        {
            resultCanvas.gameObject.SetActive(false);
            nameHighScore.gameObject.SetActive(false);

        }
        combo = 0;
        //LeftHandPresence.enableMagicSpawnerCanvas = false;
    }

    void Start()
    {
        if (currentLevel != 0)
        {
            Time.timeScale = 0;
        }
        if(comboText)
            comboText.SetActive(false);
    }

    public void PauseGame()
    {
        GameManager.Instance.TriggerPlayPause();
    }

    public void ResumeGame()
    {
        GameManager.Instance.isPause = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (currentLevel != 0)
        {
            if (Input.GetKeyDown("space"))
            {
                GameManager.Instance.TriggerPlayPause();
            }

            if (!IsStartCountDown)
            {
                GameManager.Instance.enablePause = false;               
                timeToStartCountDown -= 1 * Time.deltaTime;
                if (timeToStartCountDown <= 0)
                {
                    IsStartCountDown = true;
                    if(LeftHandPresence)
                        LeftHandPresence.enableMagicSpawnerCanvas = true;
                }
            }
            else
            {
                NPCSpawner.SetActive(true);
                if (timeRemain > 0)
                {
                    GameManager.Instance.enablePause = true;
                }
                if (timeRemain <= 0 || deathCount >= 5)
                {
                    if (deathCount >= 5)
                    {
                        timeRemain = 0;
                    }
                    GameManager.Instance.enablePause = false;
                    if (levelScore >= passScore)
                    {
                        pass = true;
                        if(saveData._LevelInfos.levelInfo.Count - 1 >= currentLevel + 1)
                            saveData._LevelInfos.levelInfo[currentLevel + 1].unlock = true;
                        if (saveData._LevelInfos.reachedLevel < currentLevel + 1)
                            saveData._LevelInfos.reachedLevel = currentLevel + 1;
                        saveData.SaveIntoJson();
                    }
                    end = true;
                    foreach (HighScore row in saveData._LevelInfos.levelInfo[currentLevel].highScore)
                    {
                        if (levelScore > row.score && pass && !newHigh)
                        {
                            newHigh = true;
                            HighScoreNameControl(true);
                            break;
                        }
                    }
                    if (!newHigh)
                    {
                        ResultCanvasControl(true);
                    }
                }
                else if (currentLevel < lastLevel)
                {
                    if (timeRemain <= 11.3f && !playCountdown)
                    {
                        playCountdown = true;
                        GameManager.Instance.source.PlayOneShot(countdown);
                    }
                    timeRemain -= 1 * Time.deltaTime;
                }
            }
            if (timeShowComboDecresing <= timeShowCombo && timeShowComboDecresing > 0)
            {
                timeShowComboDecresing -= 1 * Time.deltaTime;
            }
            if (timeShowComboDecresing <= 0f)
            {
                comboText.SetActive(false);
            }
        }
        else // TUTORIAL
        {

        }
    }

    public void UpdateScore(float noMenuScore,float raw,float tip,int correct,int fail,int hit)
    {
        levelScore += noMenuScore + raw + tip;
        rawScore += raw;
        tipScore += tip;
        correctServe += correct;
        failServe += fail;
        hitCount += hit;
        if (correct == 1)
        {
            combo += 1;
        }
        else
        {
            combo = 0;
        }
        if (combo >= 5)
        {
            timeShowComboDecresing = timeShowCombo;
            comboText.SetActive(true);
            comboText.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = "COMBO x" + combo + "!!";
            comboText.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "COMBO x" + combo + "!!";
            comboScore += raw * 0.2f;
            levelScore += raw * 0.2f;


            int randomSound = Random.Range(0, comboSFX.Count);
            GameManager.Instance.source.PlayOneShot(comboSFX[randomSound]);
        }  
        if (levelScore < 0)
        {
            levelScore = 0;
        }
        if (rawScore < 0)
        {
            rawScore = 0;
        }
    }

    void ResultCanvasControl(bool active)
    {
        resultCanvas.gameObject.SetActive(active);
    }
    void HighScoreNameControl(bool active)
    {
        nameHighScore.gameObject.SetActive(active);
    }

    public void UpdateHighScore(string name)
    {
        saveData._LevelInfos.levelInfo[currentLevel].highScore = saveData._LevelInfos.levelInfo[currentLevel].highScore.OrderBy(x => x.score).Reverse().ToList();
        saveData._LevelInfos.levelInfo[currentLevel].highScore[2].name = name;
        saveData._LevelInfos.levelInfo[currentLevel].highScore[2].score = levelScore;
        saveData.SaveIntoJson();
        nameHighScore.gameObject.SetActive(false);
    }

    IEnumerator WaitForTransistionThenStopTime()
    {
        if (levelLoader)
        {
            if(!isInGameScene)
                yield return new WaitForSeconds(5f);
            else
                yield return new WaitForSeconds(levelLoader.duration_transition);
        }
        //if (isInGameScene)
        //    Time.timeScale = 0;
    }
}
