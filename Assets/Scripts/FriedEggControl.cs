using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEggControl : MonoBehaviour
{
    public string PanTag = "Pan";

    public Material EggWhite1;
    public Material EggWhite2;
    public Material EggWhite3;
    public Material EggWhite4;

    public Material Yolk1;
    public Material Yolk2;
    public Material Yolk3;
    public Material Yolk4;
    public float time = 0;
    public int eggstate = 1;

    private GameObject WhiteEgg;
    private GameObject Yolk;

    private GameObject PanCollision;
    private float HeatEgg = 0;
    private bool isInPan = false;
    // Start is called before the first frame update
    void Start()
    {
        WhiteEgg = transform.GetChild(0).gameObject;
        Yolk = transform.GetChild(1).gameObject;
    }


        // Update is called once per frame
        void Update()
    {
        //HeatEgg = PanControlObject.GetComponent<PanControl>().HeatPan;
        if(isInPan && time<20)
        {
            //try
            //{
                if(PanCollision.GetComponent<PanControl>().enabled)
                {
                    HeatEgg = PanCollision.GetComponent<PanControl>().HeatPan;
                }
                else
                {
                    HeatEgg = 0;
                }
            //}
            //catch
            //{
            //    HeatEgg = 0;
            //}
            //print(HeatEgg);
            time += HeatEgg * Time.deltaTime;
        }
        if ((time >= 0) && (time < 5))
        {
            WhiteEgg.GetComponent<Renderer>().material = EggWhite1;
            Yolk.GetComponent<Renderer>().material = Yolk1;
            eggstate = 1;
        }
        if ((time >= 5)&&(time<10))
        {
            WhiteEgg.GetComponent<Renderer>().material = EggWhite2;
            Yolk.GetComponent<Renderer>().material = Yolk2;
            eggstate = 2;
        }
        else if ((time >= 10) && (time < 15))
        {
            WhiteEgg.GetComponent<Renderer>().material = EggWhite3;
            Yolk.GetComponent<Renderer>().material = Yolk3;
            eggstate = 3;
        }
        else if ((time >= 15) && (time < 20))
        {
            WhiteEgg.GetComponent<Renderer>().material = EggWhite4;
            Yolk.GetComponent<Renderer>().material = Yolk4;
            eggstate = 4;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PanTag))
        {
            PanCollision = collision.gameObject;
            isInPan = true;
            //print(isInPan);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(PanTag))
        {
            PanCollision = null;
            isInPan = false;
            //print(isInPan);
        }

    }
}
