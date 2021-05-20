using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class IngredientProgressControl : MonoBehaviour
{

    public Text FoodName;
    public string FoodString;
    public Text NextState;
    public string NextStateString;
    public float minimum;
    public float maximum;
    public float current;
    public Image mask;
    public Image fill;
    public Image ProgressbarImage;
    public Color color;
    public Color colorBehind;
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        FoodName.text = FoodString;
        NextState.text = NextStateString;

        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillamount = currentOffset / maximumOffset;
        mask.fillAmount = fillamount;

        fill.color = color;
        ProgressbarImage.color = colorBehind;
    }
}
