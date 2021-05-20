using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWater : MonoBehaviour
{
    public int waterHitCount = 0;
    public int milkHitCount = 0;
    public int SugarHitCount = 0;
    public int NumplaHitCount = 0;
    public int OilHitCount = 0;
    public int PrikPaoHitCount = 0;
    public int MakamHitCount = 0;

    public GameObject waterObject;
    private Vector3 waterPosition = new Vector3();
    public int WaterStatus = 0;

    public List<Material> WaterColor = new List<Material>();

    DestoryOnFloor destoryOnFloor;
    // Start is called before the first frame update
    void Start()
    {
        destoryOnFloor = GetComponent<DestoryOnFloor>();
        if(waterObject)
        {
            waterPosition = waterObject.transform.position;
            ChangeWaterColor(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(waterObject)
        {
            if(destoryOnFloor.isOnFloor && waterObject!=null)
            {
                ResetWater();
            }

        }

    }

    public void ResetWater()
    {
        if (waterObject)
        {
            waterHitCount = 0;
            //milkHitCount = 0;
            // SugarHitCount = 0;
            //NumplaHitCount = 0;
            // OilHitCount = 0;
            PrikPaoHitCount = 0;
            //MakamHitCount = 0;
            waterObject.transform.localPosition = new Vector3(0,0,0.0003f);
            waterObject.SetActive(false);
            ChangeWaterColor(0);
        }
    }

    public void ChangeWaterColor(int index)
    {
        waterObject.GetComponent<MeshRenderer>().material = WaterColor[index];
        WaterStatus = index;
    }


    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.name == "SinkWater")
        {
            waterHitCount++;
            if (waterHitCount % 10 == 0 && waterObject!=null)
            {
                waterObject.SetActive(true);
                //Debug.Log("hit by " + other.gameObject.name +"_"+ waterHitCount);
                if (waterObject.transform.localPosition.z <= 0.004)
                {
                    waterObject.transform.Translate(Vector3.up * Time.deltaTime);
                }
            }
        }

        if (other.gameObject.name == "Milk")
        {
            milkHitCount++;
        }
        if (other.gameObject.name == "Sugar")
        {
            SugarHitCount++;
        }
        if (other.gameObject.name == "Numpla")
        {
            NumplaHitCount++;
        }
        if (other.gameObject.name == "Oil")
        {
            OilHitCount++;
        }
        if (other.gameObject.name == "PrikPao")
        {
            PrikPaoHitCount++;
        }
        if (other.gameObject.name == "Makam")
        {
            MakamHitCount++;
        }

    }

}
