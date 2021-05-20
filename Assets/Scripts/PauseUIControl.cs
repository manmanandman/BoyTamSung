using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIControl : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    // Start is called before the first frame update
    void Update()
    {
        if(GameManager.Instance.currentLevel == 0)
        {
            button1.GetComponent<Button>().interactable = false;
            button2.GetComponent<Button>().interactable = false;
        }
        else
        {
            button1.GetComponent<Button>().interactable = true;
            button2.GetComponent<Button>().interactable = true;
        }
        
    }
}
