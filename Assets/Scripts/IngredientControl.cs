using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientControl : MonoBehaviour
{
    public float colliderCount = 0;
    public float colliderRange = 50;
    public float colliderRange2 = 100;
    public string ingredientName;
    public string nextStage = "ผัดให้สุก";
    public IngredientProgressControl IngredientProgressControl;
    public bool isDone = false;
    public bool isBurned = false;
    public int HeatControl;
    public bool isEnable = true;
    public bool nameSetFlag = false;
    public bool canPan = true;
    public bool isBoiling = false;
    public bool isOil = false;
    public bool pour = false;
    public Material DoneMat;
    public Material BurnMat;
    void Start()
    {
        if (!canPan)
            StartCoroutine(waitAndCanPan());
    }

    // Update is called once per frame
    void Update()
    {
        if (IngredientProgressControl)
        {
            if(!nameSetFlag)
            {
                if(!isDone && !isBurned)
                IngredientProgressControl.FoodString = ingredientName;
                if (pour)
                {
                    IngredientProgressControl.NextStateString = "เทเพิ่ม";
                }
                else
                {
                    if(!isBoiling)
                        IngredientProgressControl.NextStateString = nextStage;
                    else
                        IngredientProgressControl.NextStateString = "ต้มให้สุก";
                }
                nameSetFlag = true;
            }
            IngredientProgressControl.maximum = colliderRange;
            IngredientProgressControl.current = colliderCount;
            if (colliderCount >= colliderRange2)
            {
                IngredientProgressControl.color = Color.black;
                if (pour)
                {
                    IngredientProgressControl.FoodString = ingredientName + "เยอะไป";
                }
                else
                {
                    if (!isBoiling)
                        IngredientProgressControl.FoodString = ingredientName + "ไหม้";
                    else
                        IngredientProgressControl.FoodString = ingredientName + "เปื่อย";
                }
                IngredientProgressControl.NextStateString = "";
                isEnable = false;
                isDone = false;
                isBurned = true;
                if (BurnMat)
                {
                    var mats = this.GetComponent<MeshRenderer>().materials;
                    for (int i = 0; i < mats.Length; i++)
                    {
                        mats[i] = BurnMat;
                    }
                    this.GetComponent<MeshRenderer>().materials = mats;
                }
                //if(!(this.gameObject.name.Contains("BURNED")))
                //    this.gameObject.name = this.gameObject.name +"_BURNED" ;

            }
            else if ((colliderCount >= colliderRange) )
            {
                
                colliderCount = 0;
                colliderRange = colliderRange2;              
                isDone = true;
            }
            if (isDone)
            {
                if (DoneMat)
                    this.GetComponent<MeshRenderer>().material = DoneMat;
                IngredientProgressControl.color = Color.red;
                if (pour)
                {
                    IngredientProgressControl.FoodString = ingredientName + "พอดี";
                    IngredientProgressControl.NextStateString = "เทพอดีแล้ว";
                }
                else
                {
                    IngredientProgressControl.FoodString = ingredientName + "สุก";
                    if(!isBoiling)
                        IngredientProgressControl.NextStateString = "กำลังจะไหม้";
                    else
                        IngredientProgressControl.NextStateString = "กำลังจะเปื่อย";
                }
                if (ColorUtility.TryParseHtmlString("#00D136", out Color newCol))
                {
                    IngredientProgressControl.colorBehind = newCol;
                }
            }

            isEnable = (HeatControl != 0) ? true : false;
            if (isEnable)
            {
                if (!isBoiling)
                {
                    if (isOil)
                        colliderCount += HeatControl / 2 * Time.deltaTime;
                    else
                        colliderCount += HeatControl * 0 * Time.deltaTime;
                }
                else
                {
                    colliderCount += HeatControl / 2 * Time.deltaTime;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isEnable && isOil)
        {
            if (collision.gameObject.CompareTag("Spatular"))
            {
                colliderCount+=3 ;
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "TriggerZone")
        {
            Debug.Log(this.name + " out from pan");
            HeatControl = 0;
        }
    }

    IEnumerator waitAndCanPan()
    {
        yield return new WaitForSeconds(2f);
        canPan = true;
    }
}
