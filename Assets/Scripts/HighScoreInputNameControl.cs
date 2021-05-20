using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreInputNameControl : MonoBehaviour
{
    private char[] letters = { 'A', 'B', 'C', 'D', 'E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','0','1','2','3','4','5','6','7','8','9' };
    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;
    public Text letter1;
    public Text letter2;
    public Text letter3;
    public GameSystem gameSystem;
    public GameObject BGM;
    void Start()
    {
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        Debug.Log("Total letter = " + letters.Length);
        letter1.text = letters[GameManager.Instance.letter1].ToString();
        letter2.text = letters[GameManager.Instance.letter2].ToString();
        letter3.text = letters[GameManager.Instance.letter3].ToString();
        index1 = GameManager.Instance.letter1;
        index2 = GameManager.Instance.letter2;
        index3 = GameManager.Instance.letter3;
        BGM.gameObject.GetComponent<AudioSource>().Play();
    }

    void Update()
    {

    }

    public void UpButton1()
    {
        index1 = (index1 == letters.Length-1) ? 0 : index1 += 1;
        letter1.text = letters[index1].ToString();
    }

    public void DownButton1()
    {
        index1 = (index1 == 0) ? letters.Length-1 : index1 -= 1;
        letter1.text = letters[index1].ToString();
    }

    public void UpButton2()
    {
        index2 = (index2 == letters.Length - 1) ? 0 : index2 += 1;
        letter2.text = letters[index2].ToString();
    }

    public void DownButton2()
    {
        index2 = (index2 == 0) ? letters.Length - 1 : index2 -= 1;
        letter2.text = letters[index2].ToString();
    }

    public void UpButton3()
    {
        index3 = (index3 == letters.Length - 1) ? 0 : index3 += 1;
        letter3.text = letters[index3].ToString();
    }

    public void DownButton3()
    {
        index3 = (index3 == 0) ? letters.Length - 1 : index3 -= 1;
        letter3.text = letters[index3].ToString();
    }

    public void ConcatLetter()
    {
        string finalName = letters[index1].ToString() + letters[index2].ToString() + letters[index3].ToString();
        Debug.Log("combined name = " + finalName);
        GameManager.Instance.letter1 = index1;
        GameManager.Instance.letter2 = index2;
        GameManager.Instance.letter3 = index3;
        gameSystem.UpdateHighScore(finalName);
    }
}
