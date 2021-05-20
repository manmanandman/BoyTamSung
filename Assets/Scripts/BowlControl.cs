using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlControl : MonoBehaviour
{

    public GameObject TomYumKungInBowl;

    private bool enable = true;

    public bool sugarcheck = false;
    public bool milkcheck = false;
    public bool numplacheck = false;
    public bool makamcheck = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "TomYumKungParticle" && enable)
        {
            //other.GetComponentInParent<EnableParticle>().timeLeftCurrent = 0;
            TasteContorl tasteControl = other.gameObject.GetComponentInParent<TasteContorl>();
            sugarcheck = tasteControl.sugarcheck;
            milkcheck = tasteControl.milkcheck;
            numplacheck = tasteControl.numplacheck;
            makamcheck = tasteControl.makamcheck;

            Debug.Log("Get Food : BowlofTomYumKung");
            TomYumKungInBowl.SetActive(true);
            enable = false;

            this.gameObject.tag = "Food";
            this.gameObject.name = "BowlofTomYumKung";
        }


    }
}
