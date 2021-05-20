using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomYumKungControl : MonoBehaviour
{
    // Start is called before the first frame update
    public int total = 5;
    public int current;
    public List<GameObject> ItemInTomYumKung = new List<GameObject>();
    public PanProgressControl ProgressControl;
    public Canvas ProgressCanvas;

    public bool sugarcheck = false;
    public bool milkcheck = false;
    public bool numplacheck = false;
    public bool makamcheck = false;
    void Start()
    {

    }

    // Run every time that object "Set Active"
    private void OnEnable() 
    {
        current = total; // set current to max (total)
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("TrueLadle")) // check trigger enter by name of "TrueLadle"
        {
            if(other.GetComponent<TrueLadleControl>().TomYumKungObject.activeSelf == false && current>0) // check can use ladle
            {
                other.GetComponent<TrueLadleControl>().TomYumKungObject.SetActive(true); // Set active water

                // set taste food
                TasteContorl tasteControl = other.gameObject.GetComponent<TasteContorl>();
                tasteControl.sugarcheck = sugarcheck;
                tasteControl.milkcheck = milkcheck ;
                tasteControl.numplacheck = numplacheck;
                tasteControl.makamcheck = makamcheck;
                other.GetComponent<TrueLadleControl>().TomYumKungParticleControl.enabled = false; // Disable particle to cannot pour while in pot
                other.GetComponent<TrueLadleControl>().TomYumKungParticleControl.timeLeftCurrent = other.GetComponent<TrueLadleControl>().TomYumKungParticleControl.timeLeft; //reset time
                current--;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("TrueLadle")) // check trigger enter by name of "TrueLadle"
        {
            other.GetComponent<TrueLadleControl>().TomYumKungParticleControl.enabled = true; // Enable particle control system
            Debug.Log("Enable particle = "+ other.GetComponent<TrueLadleControl>().TomYumKungParticleControl.enabled);
            if (current != 0)
                ProgressControl.NextStateString = "ตักได้อีก " + current + " ครั้ง";
            StartCoroutine(showUI());
            if (current == 0) // out of food
            {
                this.gameObject.SetActive(false);
                    sugarcheck = false;
                    milkcheck = false;
                    numplacheck = false;
                    makamcheck = false;
            }
        }
    }


    IEnumerator showUI()
    {
        ProgressCanvas.enabled = true;
        yield return new WaitForSecondsRealtime(2f);
        ProgressCanvas.enabled = false;
    }

}
