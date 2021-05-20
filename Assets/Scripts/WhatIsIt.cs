using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsIt : MonoBehaviour
{
    public MagicCanvasController magicCanvasController = null;

    void Start()
    {
        magicCanvasController = this.transform.GetChild(1).gameObject.GetComponent<MagicCanvasController>();
        //if (magicCanvasController)
        //    Debug.Log("Found Magic Canvas");
    }

    private void OnTriggerEnter(Collider item)
    {
        //if(magicCanvasController.gameObject.activeSelf)
        //{
            //Debug.Log(this.gameObject.name + " what is it : " + item.name);
            if (item.name.Contains("bottleofoil"))
            {
                magicCanvasController.SetWhatIsItImage("oil");
            }
            else if (item.name.Contains("Basil"))
            {
                magicCanvasController.SetWhatIsItImage("basil");
            }
            else if (item.name.Contains("Chilli"))
            {
                magicCanvasController.SetWhatIsItImage("chilli");
            }
            else if (item.name.Contains("Egg"))
            {
                magicCanvasController.SetWhatIsItImage("egg");
            }
            else if (item.name.Contains("Garlic"))
            {
                magicCanvasController.SetWhatIsItImage("garlic");
            }
            else if (item.name.Contains("Hedfang"))
            {
                magicCanvasController.SetWhatIsItImage("hedfang");
            }
            else if (item.name.Contains("Homdang"))
            {
                magicCanvasController.SetWhatIsItImage("homdang");
            }
            else if (item.name.Contains("Kha"))
            {
                magicCanvasController.SetWhatIsItImage("kha");
            }
            else if (item.name.Contains("Lime"))
            {
                magicCanvasController.SetWhatIsItImage("lime");
            }
            else if (item.name.Contains("Magrood"))
            {
                magicCanvasController.SetWhatIsItImage("magrood");
            }
            else if (item.name.Contains("Pork"))
            {
                magicCanvasController.SetWhatIsItImage("pork");
            }
            else if (item.name.Contains("Garlic"))
            {
                magicCanvasController.SetWhatIsItImage("garlic");
            }
            else if (item.name.Contains("Shrimp"))
            {
                magicCanvasController.SetWhatIsItImage("shrimp");
            }
            else if (item.name.Contains("Takrai"))
            {
                magicCanvasController.SetWhatIsItImage("takrai");
            }
            else if (item.name.Contains("bottleofnumpla"))
            {
                magicCanvasController.SetWhatIsItImage("numpra");
            }
            else if (item.name.Contains("bottleofsugar"))
                {
                    magicCanvasController.SetWhatIsItImage("sugar");
                }
            else if (item.name.Contains("boxofmilk"))
            {
                magicCanvasController.SetWhatIsItImage("milk");
            }
            else if (item.name.Contains("bottleofnaprikpaoy"))
            {
                magicCanvasController.SetWhatIsItImage("prikpao");
            }
            else if (item.name.Contains("bottleofmakam"))
            {
                magicCanvasController.SetWhatIsItImage("makam");
            }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        magicCanvasController.ResetCanvas();
    }
}
