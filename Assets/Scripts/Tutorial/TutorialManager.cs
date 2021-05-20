using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public SaveData saveData;
    public GameObject Tutorial;
    public GameObject StartMenu;
    public StartMenuControl menu;

    public List<Tutorial> Tutorials = new List<Tutorial>();
    public Text HeaderText;
    public Text expText;
    public Image Image;
    public AudioSource adsource;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();

            if (instance == null)
                Debug.Log("There is no TutorialManager");

            return instance;
        }
    }

    private Tutorial currentTutorial;

    // Start is called before the first frame update
    void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTutorial)
        {
            expText.text = currentTutorial.Explanation;
            currentTutorial.CheckIfHappening();
        }
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.Order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if(!currentTutorial)
        {
            CompletedAllTutorial();
            return;
        }

        expText.text = currentTutorial.Explanation;
        HeaderText.text = currentTutorial.Header;
        if (currentTutorial.Image)
        {
            Image.sprite = currentTutorial.Image;
            Color c = Image.color;
            c.a = 1;
            Image.color = c;
        }
        else
        {
            Image.sprite = null;
            Color c = Image.color;
            c.a = 0;
            Image.color = c;
        }
        if (currentTutorial.Sound)
        {
            adsource.Stop();
            adsource.PlayOneShot(currentTutorial.Sound);
        }

    }

    public void CompletedAllTutorial()
    {
        expText.text = "You have completed all tutorial!";

        saveData._LevelInfos.passTutorial = true;
        saveData.SaveIntoJson();
        menu.onClickBackToMainMenu();
        //Tutorial.SetActive(false);
        //StartMenu.SetActive(true);

    }

    public Tutorial GetTutorialByOrder(int Order) 
    { 
        for(int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == Order)
                return Tutorials[i];
        }

        return null;
    }

    public void RestartTutorial()
    {
        SetNextTutorial(0);
    }
}
