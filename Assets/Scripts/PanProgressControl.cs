using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class PanProgressControl : MonoBehaviour
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
    public Color color;
    public Camera CameraToLookAt;

    // Start is called before the first frame update
    void Awake()
    {
        CameraToLookAt = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        transform.LookAt(CameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(CameraToLookAt.transform.forward);
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
    }

}
