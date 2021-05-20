using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelControl : MonoBehaviour
{
    public GameObject GroupButton;
    int numberoflevel;
    int reachedLevel;

    public Sprite unlockLevel;
    public Sprite currentlevel;
    public Sprite nonlevel;

    public GameObject StartMenuControl;

    public int reachedPage = 0;

    GameObject[] pages;
    Button[] buttons;

    void Start()
    {
        numberoflevel = GameManager.Instance.saveData._LevelInfos.levelInfo.Count - 1;
        reachedLevel = GameManager.Instance.saveData._LevelInfos.reachedLevel;
        //if (reachedLevel > numberoflevel)
        //    reachedLevel = numberoflevel;
        //numberoflevel = 13;
        //reachedLevel = 8;
        CreateButton();
        SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateButton()
    {
        Debug.Log("Total Level = " + numberoflevel);
        GameObject pagecreate;
        for (int i = 0; i <= ModLevelNum(); i++)
        {
            pagecreate = (GameObject)Instantiate(GroupButton, transform);
        }
    }

    void SetButton()
    {
        int runnum = 1; //run along all loop to assign level
        pages = new GameObject[transform.childCount]; //get size of pages[]
        for (int i = 0;i< pages.Length;i++) // loop all page
        {
            pages[i] = transform.GetChild(i).gameObject; //assign page into pages[]
            buttons = new Button[pages[i].transform.childCount]; //get size of buttons[]   which is 6 always
            for (int j = 0; j < buttons.Length; j++) //loop all button in page
            {
                buttons[j] = pages[i].transform.GetChild(j).GetComponent<Button>();
                if (runnum <= reachedLevel)
                {
                    if(runnum != numberoflevel + 1)
                    {
                        buttons[j].interactable = true;
                        buttons[j].image.sprite = unlockLevel;
                        buttons[j].GetComponentInChildren<Text>().text = (runnum).ToString();
                        int level = runnum;
                        buttons[j].onClick.AddListener(() => LoadLevel(level));
                    }
                }
                if (runnum == reachedLevel)
                {
                    Debug.Log("Reached Level = " + runnum);
                    buttons[j].image.sprite = currentlevel;
                    reachedPage = i;
                }
                if (runnum > numberoflevel)
                {
                    buttons[j].image.sprite = nonlevel;
                }
                runnum++;
            }
        }
    }

    int ModLevelNum()
    {
        if (numberoflevel <= 6)
            return 0;
        else
            return numberoflevel / 6;
    }

    void LoadLevel(int level)
    {
        Debug.Log("Select level " + level);
        StartMenuControl.GetComponent<StartMenuControl>().OnClickStartGame(level);
    }

}
