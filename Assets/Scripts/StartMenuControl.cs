using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControl : MonoBehaviour
{
    public LevelLoader levelLoader;
    public SaveData saveData;

    private void Awake()
    {
        saveData = GameObject.Find("LevelInfo").GetComponent<SaveData>();
    }
    public void OnClickStartGame(int level)
    {
        Debug.Log("Start Game");
        Time.timeScale = 1;
        GameManager.Instance.currentLevel = level;
        StartCoroutine(LoadAsynchronously("Oculus Test"));
    }
    public void NextLevel()
    {
        Debug.Log("Start Next Level");
        Time.timeScale = 1;
        if (saveData._LevelInfos.levelInfo.Count - 1 >= GameManager.Instance.currentLevel + 1)
        {
            GameManager.Instance.currentLevel = GameManager.Instance.currentLevel + 1;
            StartCoroutine(LoadAsynchronously("Oculus Test"));
        }
        else
            onClickBackToMainMenu();
    }

    public void restartLevel()
    {
        Debug.Log("Restart Game");
        Time.timeScale = 1;
        GameManager.Instance.currentLevel = GameManager.Instance.currentLevel;
        StartCoroutine(LoadAsynchronously("Oculus Test"));
    }

    public void onClickBackToMainMenu()
    {
        Debug.Log("Main Menu");
        GameManager.Instance.currentLevel = 0;
        StartCoroutine(LoadAsynchronously("MainMenu"));
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        if (levelLoader)
        {
            levelLoader.TriggerTransitionNextScene();
            yield return new WaitForSeconds(levelLoader.duration_transition);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

    }
}
