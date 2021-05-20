using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private void Awake()
    {
        BetterStreamingAssets.Initialize();
    }

    [SerializeField] public LevelInfos _LevelInfos = new LevelInfos();
    public void SaveIntoJson()
    {
        try
        {
            string levelInfos = JsonUtility.ToJson(_LevelInfos);
            string path = Path.Combine(Application.persistentDataPath, "levelInfos.json");
            File.WriteAllText(path, levelInfos);
        }
        catch
        {
            //error log
        }
    }

    public void LoadJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "levelInfos.json");

        if (File.Exists(path))
        {
            string levelInfos = File.ReadAllText(path);
            _LevelInfos = JsonUtility.FromJson<LevelInfos>(levelInfos);
            if (_LevelInfos.version != "0.3")
            {
                levelInfos = BetterStreamingAssets.ReadAllText("default.json");
                _LevelInfos = JsonUtility.FromJson<LevelInfos>(levelInfos);
            }
        }
        else
        {
            string levelInfos = BetterStreamingAssets.ReadAllText("default.json");
            _LevelInfos = JsonUtility.FromJson<LevelInfos>(levelInfos);
        }
    }

    private void Start()
    {
        LoadJson();
    }
}

[System.Serializable]
public class LevelInfos
{
    public string version;
    public bool passTutorial;
    public int reachedLevel;
    public List<LevelInfo> levelInfo = new List<LevelInfo>();
}

[System.Serializable]
public class LevelInfo
{
    public int name;
    public bool unlock;
    public List<HighScore> highScore = new List<HighScore>();
}

[System.Serializable]
public class HighScore
{
    public string name;
    public float score;
}