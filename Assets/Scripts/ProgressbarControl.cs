using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]

public class ProgressbarControl : MonoBehaviour
{
    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;
    public Color color1;
    public Color color2;
    public Color color3;

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillamount = currentOffset / maximumOffset;
        mask.fillAmount = fillamount;
        if(fillamount < 0.25)
        {
            mask.color = color3;
        } 
        else if(fillamount < 0.5)
        {
            mask.color = color2;
        }
        else
        {
            mask.color = color1;
        }
    }
}
