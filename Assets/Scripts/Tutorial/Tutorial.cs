using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public int Order;

    public string Header;
    public Sprite Image;
    public AudioClip Sound;

    [TextArea(3,10)]
    public string Explanation;

    void Awake()
    {
        TutorialManager.Instance.Tutorials.Add(this);
    }

    public virtual void CheckIfHappening() { }
}
