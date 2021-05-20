using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueLadleControl : MonoBehaviour
{
    public GameObject TomYumKungObject;
    public EnableParticle TomYumKungParticleControl;
    TasteContorl tasteControl;
    // Start is called before the first frame update
    void Start()
    {
        
            tasteControl = this.gameObject.GetComponent<TasteContorl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TomYumKungParticleControl.timeLeftCurrent <= 0)
        {
            TomYumKungParticleControl.timeLeftCurrent = TomYumKungParticleControl.timeLeft;
            TomYumKungObject.SetActive(false);

            tasteControl.sugarcheck = false;
            tasteControl.milkcheck = false;
            tasteControl.numplacheck = false;
        }
    }
}
