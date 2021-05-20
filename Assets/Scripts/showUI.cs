using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showUI : MonoBehaviour
{
    public GameSystem gameSystem;

    bool enableUI;

    public GameObject FriedEgg;
    public GameObject FriedEggRice;
    public GameObject KaPrao;
    public GameObject KaPraoFriedEgg;
    public GameObject TomYumKung;
    public GameObject LastLevel;


    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        setUI(gameSystem.currentLevel);
        if(enableUI)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    void setUI(int level)
    {
        //====================================================================
        if (level == 1) // friedEgg
        {
            FriedEgg.gameObject.SetActive(true);
            enableUI = true;
        }
        else if (level == 2) // friedEgg + Rice
        {
            FriedEggRice.gameObject.SetActive(true);
            enableUI = true;
        }
        else if (level == 3) // MIX#1
        {
            FriedEggRice.gameObject.SetActive(true);
            enableUI = false;
        }
        //====================================================================
        else if (level == 4) //KaPrao
        {
            KaPrao.gameObject.SetActive(true);
            enableUI = true;
        }
        else if (level == 5) //KaPrao + FriedEgg
        {
            KaPraoFriedEgg.gameObject.SetActive(true);
            enableUI = true;
        }
        else if (level == 6) // MIX#2
        {
            KaPraoFriedEgg.gameObject.SetActive(true);
            enableUI = false;
        }
        //====================================================================
        else if (level == 7) //TYK
        {
            TomYumKung.gameObject.SetActive(true);
            enableUI = true;
        }
        else if (level == 8) // MIX#2
        {
            TomYumKung.gameObject.SetActive(true);
            enableUI = false;
        }
        //====================================================================
        else if(level == gameSystem.lastLevel)
        {
            LastLevel.gameObject.SetActive(true);
            enableUI = true;
        }
    }
}
