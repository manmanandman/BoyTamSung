using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevel;
    public SaveData saveData;

    //public bool enableMagicSpawnerCanvas = false;
    //tutorial
    public GameObject StartMenu;
    public GameObject Tutorial;

    //pause game
    public bool enablePause = false;
    public bool isPause = false;
    public Canvas pauseCanvas;
    public GameObject Zawarudo;
    private GameObject BGMnormal;
    public AudioSource source;
    public AudioClip pauseSound;
    public AudioClip resumeSound;

    //highscore temp
    [HideInInspector]
    public int letter1, letter2, letter3 = 0;

    private float time = 3f;
    private bool pass = true;

    //instance
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();

            if (instance == null)
                Debug.Log("There is no Game Manager");

            return instance;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
        saveData.LoadJson();
        if (saveData._LevelInfos.passTutorial)
        {
            Tutorial.SetActive(false);
        }
        else
        {
            StartMenu.SetActive(false);
        }
    }
    void Start()
    {
        //Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //    if (task.Result == Firebase.DependencyStatus.Available)
        //    {
        //        Debug.Log("Firebase Already!!!");
        //    }
        //    else
        //    {
        //        Debug.LogError($"Firebase Dependency Not Avialiable With {task.Result}");
        //    }
        //}
        //);
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0f && pass)
        {
            pass = false;
            //try
            //{
            //    DatabaseReference db = FirebaseDatabase.DefaultInstance.RootReference;
            //    Test data = new Test(2, 50.5f);
            //    string json = JsonUtility.ToJson(data);
            //    db.Child(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff")).SetRawJsonValueAsync(json);
            //    Debug.Log("Success Firebase Data Test Pushing");
            //}
            //catch 
            //{
            //    Debug.LogError("Cant push Test Data to Firebase");
            //}

        }
        else
        {
            time -= 1 * Time.deltaTime;
        }
    }
    public void setEnablePause(bool enable)
    {
        if (!enable)
            enablePause = false;
        else
            enablePause = true;
    }

    public void TriggerPlayPause()
    {
        if (enablePause)
        {
            if (isPause)
            {
                isPause = false;
                ResumeGame();
            }
            else
            {
                isPause = true;
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        source.PlayOneShot(pauseSound);
        BGMnormal = GameObject.Find("BGM_Gameplay");
        BGMnormal.GetComponent<AudioSource>().Pause();
        Zawarudo.SetActive(true);
        pauseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        source.PlayOneShot(resumeSound);
        BGMnormal.GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        Zawarudo.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
    }

    public class Test
    {
        public int level;
        public float score;

        public Test()
        {
        }

        public Test(int level, float score)
        {
            this.level = level;
            this.score = score;
        }
    }
}
