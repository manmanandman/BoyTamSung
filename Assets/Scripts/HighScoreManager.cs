using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public GameSystem gameSystem;
    public SaveData saveData;
    public Text namer;
    public Text score;
    public List<HighScore> highScore;

    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        saveData = GameObject.Find("LevelInfo").GetComponent<SaveData>();
        saveData.LoadJson();
        foreach(LevelInfo level in saveData._LevelInfos.levelInfo)
        {
            if (level.name.Equals(gameSystem.currentLevel))
            {
                highScore = level.highScore;
                break;
            }
        }
        highScore = highScore.OrderBy(x => x.score).Reverse().ToList();
        namer.text = null;
        score.text = null;
        foreach (HighScore row in highScore)
        {
            namer.text = namer.text + row.name + "\n";
            if (row.score == -9999f)
            {
                score.text = score.text + "---\n";
            }
            else
            {
                score.text = score.text + row.score.ToString() + "\n";
            }
        }
        namer.text = namer.text.Trim('\n');
        score.text = score.text.Trim('\n');
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
