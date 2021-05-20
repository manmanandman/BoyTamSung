using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceControl : MonoBehaviour
{
    public int NumberOfRice = 5;
    public GameObject ProgressCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(NumberOfRice == 0)
        {
            ProgressCanvas.GetComponent<Canvas>().enabled = true;
            gameObject.SetActive(false);
            ProgressCanvas.GetComponent<PanProgressControl>().FoodString = "ข้าวหมด";
            ProgressCanvas.GetComponent<PanProgressControl>().NextStateString = "ปิดฝาหม้อเพื่อหุงข้าว";
            ProgressCanvas.GetComponent<PanProgressControl>().current = 0;
        }
    }
}
