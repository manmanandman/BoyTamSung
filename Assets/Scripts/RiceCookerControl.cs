using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RiceCookerControl : MonoBehaviour
{

    public GameObject rice;
    public float Cooktime = 5;
    public GameObject ProgressCanvas;
    public AudioSource source;
    public AudioClip fullricesound;

    private bool isplaysound = false;
    private float timeRemaining;
    private bool isEmpty = false;
    private int tempNumberOfRice;

    void Start()
    {
        timeRemaining = Cooktime;
        tempNumberOfRice = rice.GetComponent<RiceControl>().NumberOfRice;
        ProgressCanvas.GetComponent<PanProgressControl>().current = 0;
        ProgressCanvas.GetComponent<PanProgressControl>().maximum = Cooktime;
    }
    // Update is called once per frame
    void Update()
    {
        ProgressCanvas.GetComponent<Canvas>().enabled = isEmpty;
        if(rice.GetComponent<RiceControl>().NumberOfRice == 0)
        {
            isEmpty = true;
            isplaysound = false;
        }
        if(isEmpty)
        {
            timeRemaining -= 1 * Time.deltaTime;

            ProgressCanvas.GetComponent<PanProgressControl>().FoodString = "กำลังหุงข้าว";
            ProgressCanvas.GetComponent<PanProgressControl>().NextStateString = "ปิดฝาหม้อเพื่อหุงข้าว";
            ProgressCanvas.GetComponent<PanProgressControl>().current = Cooktime - timeRemaining;

            if (timeRemaining < 0)
            {          
                rice.SetActive(true);
                rice.GetComponent<RiceControl>().NumberOfRice = tempNumberOfRice;
                isEmpty = false;
                timeRemaining = Cooktime;
                if (!isplaysound )
                {
                    source.PlayOneShot(fullricesound);
                    isplaysound = true;
                }
            }
        }
    }
}
