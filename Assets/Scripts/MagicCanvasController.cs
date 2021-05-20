using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicCanvasController : MonoBehaviour
{

    public Image WhatIsItImage;

    public Sprite Hedfang;
    public Sprite Basil;
    public Sprite Magrood;
    public Sprite Egg;
    public Sprite Shrimp;
    public Sprite Kha;
    public Sprite Takrai;
    public Sprite Milk;
    public Sprite Sugar;
    public Sprite Numpra;
    public Sprite Prikpao;
    public Sprite Oil;
    public Sprite Chilli;
    public Sprite Makam;
    public Sprite Lime;
    public Sprite Pork;
    public Sprite Homdang;
    public Sprite Garlic;

    private void Start()
    {
        WhatIsItImage = this.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Image>();
        //if (WhatIsItImage)
        //    Debug.Log("Found WhatIsItImage");
    }
    public void UpdateUnlocking()
    {
        this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
        if (GameManager.Instance.saveData._LevelInfos.reachedLevel >= 2)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        }

        if (GameManager.Instance.saveData._LevelInfos.reachedLevel >= 4)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
        }

        if (GameManager.Instance.saveData._LevelInfos.reachedLevel >= 5)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(true);
        }

        if (GameManager.Instance.saveData._LevelInfos.reachedLevel >= 7)
        {
            this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void MenusPanelSetActive(bool enable)
    {
        this.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(enable);
        //Debug.Log(this.transform.GetChild(0).transform.GetChild(1).gameObject.name + enable);
    }

    public void WhatIsItPanelSetActive(bool enable)
    {
        this.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(enable);
    }

    public void SetWhatIsItImage(string itemname)
    {

        MenusPanelSetActive(false);
        WhatIsItPanelSetActive(true);
        switch (itemname)
        {
            case "hedfang":
                WhatIsItImage.sprite = Hedfang;
                break;
            case "basil":
                WhatIsItImage.sprite = Basil;
                break;
            case "magrood":
                WhatIsItImage.sprite = Magrood;
                break;
            case "egg":
                WhatIsItImage.sprite = Egg;
                break;
            case "shrimp":
                WhatIsItImage.sprite = Shrimp;
                break;
            case "kha":
                WhatIsItImage.sprite = Kha;
                break;
            case "takrai":
                WhatIsItImage.sprite = Takrai;
                break;
            case "milk":
                WhatIsItImage.sprite = Milk;
                break;
            case "sugar":
                WhatIsItImage.sprite = Sugar;
                break;
            case "numpra":
                WhatIsItImage.sprite = Numpra;
                break;
            case "prikpao":
                WhatIsItImage.sprite = Prikpao;
                break;
            case "oil":
                WhatIsItImage.sprite = Oil;
                break;
            case "chilli":
                WhatIsItImage.sprite = Chilli;
                break;
            case "makam":
                WhatIsItImage.sprite = Makam;
                break;
            case "lime":
                WhatIsItImage.sprite = Lime;
                break;
            case "pork":
                WhatIsItImage.sprite = Pork;
                break;
            case "garlic":
                WhatIsItImage.sprite = Garlic;
                break;
            case "homdang":
                WhatIsItImage.sprite = Homdang;
                break;

            default:
                break;
        }

           

    }

    public void ResetCanvas()
    {
        WhatIsItImage.sprite = null;
        MenusPanelSetActive(true);
        WhatIsItPanelSetActive(false);
    }

}
