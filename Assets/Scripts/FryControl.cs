using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryControl : MonoBehaviour
{
    private List<GameObject> chilli = new List<GameObject>();
    private List<GameObject> garlic = new List<GameObject>();
    private List<GameObject> pork = new List<GameObject>();
    private List<GameObject> basil = new List<GameObject>();
    private List<GameObject> shrimp = new List<GameObject>();
    private List<GameObject> hedfang = new List<GameObject>();
    private List<GameObject> lime = new List<GameObject>();
    private List<GameObject> homdang = new List<GameObject>();
    private List<GameObject> kha = new List<GameObject>();
    private List<GameObject> takrai = new List<GameObject>();
    private List<GameObject> magrood = new List<GameObject>();
    private List<GameObject> food = new List<GameObject>();

    private int chilliNum = 1;
    private int garlicNum = 1;
    private int porkNum = 1;
    private int basilNum = 1;
    private int shrimpNum = 1;
    private int hedfangNum = 1;
    private int limeNum = 1;
    private int homdangNum = 1;
    private int khaNum = 1;
    private int takraiNum = 1;
    private int otherNum = 1;
    private int milkNum = 1;
    private int sugarNum = 1;
    private int numplaNum = 1;
    private int magroodNum = 1;
    private int makamNum = 1;

    public List<GameObject> UI = new List<GameObject>();
    public PanControl panControl;
    public int HeatFromPanControl;
    public GameObject ingredientWithProgress;
    public GameObject Grid;
    public GameObject Panel;
    public RectTransform rt;

    public GameObject SpawnPosition;
    public GameObject PrefabKrapaoMoosub;
    public GameObject PrefabTomYumKung;
    public GameObject TomYumKungObject;
    private GameObject TomYumKungSpawn;
    //private int TomYumKungNum = 1;

    public bool isOil = false;
    public GameObject Oil;
    public GameObject OilCanvas;

    public bool isBoiling = false;
    private GetWater GetWater;
    GameObject spawn;
    public float oilCountdown = 60f;
    private float oilCurrentCountdown;

    public GameObject Milk;
    private GameObject milkSpawn;
    private int milkTemp = 0;
    private bool milkFlag = false;

    public GameObject Sugar;
    private GameObject sugarSpawn;
    private int sugarTemp = 0;
    private bool sugarFlag = false;

    public GameObject Numpla;
    private GameObject numplaSpawn;
    private int numplaTemp = 0;
    private bool numplaFlag = false;

    private bool prikpaoflag = false;

    public GameObject MaKam;
    private GameObject MaKamSpawn;
    private int MaKamtemp = 0;
    private bool MaKamflag = false;

    public GameObject TomYumKungCanvas;
    public Text takraitext;
    public Text khatext;
    public Text magroodtext;
    public Text shrimptext;
    public Text homdangtext;
    public Text prikpaotext;
    public Text hedfangtext;
    public Text limetext;
    public Text sugartext;
    public Text milktext;
    public Text numpratext;
    public Text makamtext;

    public Canvas TYKHelper;

    public AudioSource source;
    public AudioClip DoneFood;
    private int oilTemp = 0;
    void Start()
    {
        rt = Panel.GetComponent<RectTransform>();
        if(this.GetComponentInParent<GetWater>())
        {
            GetWater = this.GetComponentInParent<GetWater>();
        }
        oilCurrentCountdown = oilCountdown;

        source = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        // Get Heat Value
        HeatFromPanControl = (int)panControl.HeatPan;

        if (UI.Count == 0)
            Panel.SetActive(false);
        else
            Panel.SetActive(true);

        SetIngredientEnable();

        if ((chilli.Count > 0) && (garlic.Count >0) && (pork.Count >0) && (basil.Count>0) && !isBoiling)
            KraPaoMooSubCheck();

        if (isBoiling)
        {
            TomYumKungCheck();
            if(TYKHelper)
                TYKHelper.gameObject.SetActive(true);
            //TomYumKungCanvas.SetActive(true);
        }
        else
        {
            if (TYKHelper)
                TYKHelper.gameObject.SetActive(false);
        }
        //else
        //    if(TomYumKungCanvas)
        //    TomYumKungCanvas.SetActive(false);

        //if(isBoiling && PrikPaoSpawn != null)
        //{
        //    GetWater.ChangeWaterColor(1);
        //}

        if (Oil)
        {
            isOil = (GetWater.OilHitCount > 0) ? true : false;

            if(oilTemp != GetWater.OilHitCount)
            {
                oilTemp = GetWater.OilHitCount;
                oilCurrentCountdown = oilCountdown;
            }
            if (isOil)
            {
                Oil.SetActive(true);
                OilCanvas.SetActive(false);
                oilCurrentCountdown -= 1 * Time.deltaTime;
                if (oilCurrentCountdown <= 0)
                {
                    isOil = false;
                    oilCurrentCountdown = oilCountdown;
                    GetWater.OilHitCount = 0;
                    oilTemp = 0;
                }
            }
            else
            {
                Oil.SetActive(false);
                OilCanvas.SetActive(true);
            }
        }

        if (GetWater)
        {
            if (GetWater.waterHitCount >= 10) isBoiling = true;
            //MILK
            if (GetWater.milkHitCount == 1 && !milkFlag)
            {
                milkSpawn = Instantiate(Milk, transform.position, Quaternion.identity) as GameObject;
                milkSpawn.name = "milkingredient_" + milkNum;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = "milkingredient_" + milkNum;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "นม";
                ChangeRectSize(true);
                milkNum++;
                milkSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                milkSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                milkTemp = GetWater.milkHitCount;
                milkFlag = true;
            }
            if(milkTemp !=  GetWater.milkHitCount)
            {
                milkTemp = GetWater.milkHitCount;
                milkSpawn.GetComponent<IngredientControl>().colliderCount += 1;
            }
            //SUGAR
            if (GetWater.SugarHitCount == 1 && !sugarFlag)
            {
                sugarSpawn = Instantiate(Sugar, transform.position, Quaternion.identity) as GameObject;
                sugarSpawn.name = "sugaringredient_" + sugarNum;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = "sugaringredient_" + sugarNum;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "น้้ำตาล";
                ChangeRectSize(true);
                sugarNum++;
                sugarSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                sugarSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                sugarTemp = GetWater.SugarHitCount;
                sugarFlag = true;
            }
            if (sugarTemp != GetWater.SugarHitCount)
            {
                sugarTemp = GetWater.SugarHitCount;
                sugarSpawn.GetComponent<IngredientControl>().colliderCount += 1;
            }
            //NUMPLA
            if (GetWater.NumplaHitCount == 1 && !numplaFlag)
            {
                numplaSpawn = Instantiate(Numpla, transform.position, Quaternion.identity) as GameObject;
                numplaSpawn.name = "numplaingredient_" + numplaNum;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = "numplaingredient_" + numplaNum;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "น้ำปลา";
                ChangeRectSize(true);
                numplaNum++;
                numplaSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                numplaSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                numplaTemp = GetWater.NumplaHitCount;
                numplaFlag = true;
            }
            if (numplaTemp != GetWater.NumplaHitCount)
            {
                numplaTemp = GetWater.NumplaHitCount;
                numplaSpawn.GetComponent<IngredientControl>().colliderCount += 1;
            }
            //PRIKPAO
            if (GetWater.PrikPaoHitCount > 0)
            {
                if(isBoiling)
                    GetWater.ChangeWaterColor(1);
            }
            else
            { 
                if(GetWater.waterObject)
                    GetWater.ChangeWaterColor(0);
            }

            //MAKAM
            if (GetWater.MakamHitCount == 1 && !MaKamflag)
            {
                MaKamSpawn = Instantiate(MaKam, transform.position, Quaternion.identity) as GameObject;
                MaKamSpawn.name = "makamingredient_" + makamNum;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = "makamingredient_" + makamNum;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "มะขามเปียก";
                ChangeRectSize(true);
                makamNum++;
                MaKamSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                MaKamSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                MaKamtemp = GetWater.MakamHitCount;
                MaKamflag = true;
            }
            if (MaKamtemp != GetWater.MakamHitCount)
            {
                MaKamtemp = GetWater.MakamHitCount;
                MaKamSpawn.GetComponent<IngredientControl>().colliderCount += 1;
            }
        }

    }

    public void ChangeRectSize(bool check)
    {
        if (check)
        {
            if(UI.Count != 0)
                rt.offsetMax += new Vector2(0, 68);
        }
        else
            rt.offsetMax -= new Vector2(0, 68);
    }
    private void OnTriggerEnter(Collider ingredient)
    {
        if (ingredient.gameObject.name.Contains("Chilli"))
        {
            ingredient.gameObject.name = "Chilli_" + chilliNum;
            chilli.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Chilli_" + chilliNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "พริก";
            ChangeRectSize(true);
            chilliNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Garlic"))
        {
            ingredient.gameObject.name = "Garlic_" + garlicNum;
            garlic.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Garlic_" + garlicNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "กระเทียม";
            ChangeRectSize(true);
            garlicNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Pork"))
        {
            ingredient.gameObject.name = "Pork_" + porkNum;
            pork.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Pork_" + porkNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "หมูสับ";
            ChangeRectSize(true);
            porkNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Basil"))
        {
            ingredient.gameObject.name = "Basil_" + basilNum;
            basil.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Basil_" + basilNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "กะเพรา";
            ChangeRectSize(true);
            basilNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("KaPraoPan"))
        {
            ingredient.gameObject.name = "KaPraoPan_" + otherNum;
            food.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "KaPraoPan_" + otherNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "ผัดกะเพราหมูสับ";
            ChangeRectSize(true);
            otherNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Shrimp"))
        {
            ingredient.gameObject.name = "Shrimp_" + shrimpNum;
            shrimp.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Shrimp_" + shrimpNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "กุ้ง";
            ChangeRectSize(true);
            shrimpNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Hedfang"))
        {
            ingredient.gameObject.name = "Hedfang_" + hedfangNum;
            hedfang.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Hedfang_" + hedfangNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "เห็ดฟาง";
            ChangeRectSize(true);
            hedfangNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Lime"))
        {
            ingredient.gameObject.name = "Lime_" + limeNum;
            lime.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Lime_" + limeNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "มะนาว";
            ChangeRectSize(true);
            limeNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Homdang"))
        {
            ingredient.gameObject.name = "Homdang_" + homdangNum;
            homdang.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Homdang_" + homdangNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "หอมแดง";
            ChangeRectSize(true);
            homdangNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Kha"))
        {
            ingredient.gameObject.name = "Kha_" + khaNum;
            kha.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Kha_" + khaNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "ข่า";
            ChangeRectSize(true);
            khaNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Takrai"))
        {
            ingredient.gameObject.name = "Takrai_" + takraiNum;
            takrai.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Takrai_" + takraiNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "ตะไคร้";
            ChangeRectSize(true);
            takraiNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if (ingredient.gameObject.name.Contains("Magrood"))
        {
            ingredient.gameObject.name = "Magrood_" + magroodNum;
            magrood.Add(ingredient.gameObject);
            var newUI = Instantiate(ingredientWithProgress, Grid.transform);
            newUI.name = "Magrood_" + magroodNum;
            UI.Add(newUI);
            UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "ใบมะกรูด";
            ChangeRectSize(true);
            magroodNum++;
            ingredient.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
            ingredient.GetComponent<IngredientControl>().nameSetFlag = false;
        }
        else if(ingredient.gameObject.name.Contains("milkingredient"))
        {
            if(!milkFlag)
            {
                milkFlag = true;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = ingredient.gameObject.name;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "นม";
                ChangeRectSize(true);
                milkSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                milkSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                milkTemp = GetWater.milkHitCount;
            }
        }
        else if (ingredient.gameObject.name.Contains("sugaringredient"))
        {
            if (!sugarFlag)
            {
                sugarFlag = true;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = ingredient.gameObject.name;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "น้ำตาล";
                ChangeRectSize(true);
                sugarSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                sugarSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                sugarTemp = GetWater.SugarHitCount;
            }
        }
        else if (ingredient.gameObject.name.Contains("numplaingredient"))
        {
            if (!numplaFlag)
            {
                numplaFlag = true;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = ingredient.gameObject.name;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "น้ำปลา";
                ChangeRectSize(true);
                numplaSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                numplaSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                numplaTemp = GetWater.NumplaHitCount;
            }
        }
        else if (ingredient.gameObject.name.Contains("makamingredient"))
        {
            if (!MaKamflag)
            {
                MaKamflag = true;
                var newUI = Instantiate(ingredientWithProgress, Grid.transform);
                newUI.name = ingredient.gameObject.name;
                UI.Add(newUI);
                UI[UI.Count - 1].transform.GetChild(1).GetComponent<Text>().text = "มะขามเปียก";
                ChangeRectSize(true);
                MaKamSpawn.GetComponent<IngredientControl>().IngredientProgressControl = newUI.GetComponent<IngredientProgressControl>();
                MaKamSpawn.GetComponent<IngredientControl>().nameSetFlag = false;
                MaKamtemp = GetWater.MakamHitCount;
            }
        }
    }

    private void OnTriggerExit(Collider ingredient)
    {
        if (ingredient.gameObject.name.Contains("Chilli"))
        {
            chilli.RemoveAt(chilli.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Garlic"))
        {
            garlic.RemoveAt(garlic.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Pork"))
        {
            pork.RemoveAt(pork.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Basil"))
        {
            basil.RemoveAt(basil.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Shrimp"))
        {
            shrimp.RemoveAt(shrimp.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Hedfang"))
        {
            hedfang.RemoveAt(hedfang.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Lime"))
        {
            lime.RemoveAt(lime.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Homdang"))
        {
            homdang.RemoveAt(homdang.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Kha"))
        {
            kha.RemoveAt(kha.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Takrai"))
        {
            takrai.RemoveAt(takrai.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("Magrood"))
        {
            magrood.RemoveAt(magrood.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("KaPraoPan"))
        {
            food.RemoveAt(food.IndexOf(ingredient.gameObject));
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ingredient.gameObject.name = "KaPraoPan(Clone)";
            //if(ingredient.gameObject.GetComponent<IngredientControl>().isBurned)
            //    ingredient.gameObject.name = "KaPraoBurned";
            ChangeRectSize(false);
        }
        else if (ingredient.gameObject.name.Contains("milkingredient"))
        {
            
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
            milkFlag = false;
            milkTemp = 0;
            GetWater.milkHitCount = 0;
        }
        else if (ingredient.gameObject.name.Contains("sugaringredient"))
        {
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
            sugarFlag = false;
            sugarTemp = 0;
            GetWater.SugarHitCount = 0;
        }
        else if (ingredient.gameObject.name.Contains("numplaingredient"))
        {
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
            numplaFlag = false;
            numplaTemp = 0;
            GetWater.NumplaHitCount = 0;
        }
        else if (ingredient.gameObject.name.Contains("makamingredient"))
        {
            Destroy(UI[FindUIByName(ingredient.name)]);
            UI.RemoveAt(FindUIByName(ingredient.name));
            ChangeRectSize(false);
            MaKamflag = false;
            MaKamtemp = 0;
            GetWater.MakamHitCount = 0;
        }
        //else if (ingredient.gameObject.name.Contains("TomYumKung"))
        //{
        //    Destroy(UI[FindUIByName(ingredient.name)]);
        //    UI.RemoveAt(FindUIByName(ingredient.name));
        //    ChangeRectSize(false);
        //}
    }

    int FindUIByName(string name)
    {
        for(int i=0; i<UI.Count; i++)
        {
            if(UI[i].name == name) 
            {
                return i;
            }
        }
        return -1;
    }

    int FindDoneIngredient(List<GameObject> ingredientList)
    {
        for (int i = 0; i < ingredientList.Count; i++)
        {
            if (ingredientList[i].GetComponent<IngredientControl>().isDone == true)
            {
                return i;
            }
        }
        return -1;
    }

    int FindDoneOneIngredient(GameObject ingredient)
    {
        if (ingredient != null)
        {
            if (ingredient.GetComponent<IngredientControl>().isDone == true)
            {
                return 1;
            }
            else
                return -1;
        }
        else return -1;
    }

    void SetIngredientEnable()
    {
        if(HeatFromPanControl == 0)
        {
            EnableControl(chilli, false,isBoiling,isOil);
            EnableControl(pork, false, isBoiling, isOil);
            EnableControl(garlic, false, isBoiling, isOil);
            EnableControl(basil, false, isBoiling, isOil);
            EnableControl(shrimp, false, isBoiling, isOil);
            EnableControl(hedfang, false, isBoiling, isOil);
            EnableControl(lime, false, isBoiling, isOil);
            EnableControl(food, false, isBoiling, isOil);
            EnableControl(homdang, false, isBoiling, isOil);
            EnableControl(kha, false, isBoiling, isOil);
            EnableControl(takrai, false, isBoiling, isOil);
            EnableControl(magrood, false, isBoiling, isOil);
        }
        else
        {
            EnableControl(chilli, true, isBoiling, isOil);
            EnableControl(pork, true, isBoiling, isOil);
            EnableControl(garlic, true, isBoiling, isOil);
            EnableControl(basil, true, isBoiling, isOil);
            EnableControl(shrimp, true, isBoiling, isOil);
            EnableControl(hedfang, true, isBoiling, isOil);
            EnableControl(lime, true, isBoiling, isOil);
            EnableControl(homdang, true, isBoiling, isOil);
            EnableControl(kha, true, isBoiling, isOil);
            EnableControl(takrai, true, isBoiling, isOil);
            EnableControl(food, true, isBoiling, isOil);
            EnableControl(magrood, true, isBoiling, isOil);
        }
    }

    void EnableControl(List<GameObject> ingredientList, bool Enable, bool isInWater,bool isOil)
    {
        for (int i = 0; i < ingredientList.Count; i++)
        {
            ingredientList[i].GetComponent<IngredientControl>().isEnable = Enable;
            ingredientList[i].GetComponent<IngredientControl>().isBoiling = isInWater;
            ingredientList[i].GetComponent<IngredientControl>().isOil = isOil;
            if(Enable)
                ingredientList[i].GetComponent<IngredientControl>().HeatControl = HeatFromPanControl;
            else
                ingredientList[i].GetComponent<IngredientControl>().HeatControl = 0;
        }
    }

    void DestroyIngredient(List<GameObject> ingredientList, int index)
    {
        Destroy(UI[FindUIByName(ingredientList[index].gameObject.name)]);
        UI.RemoveAt(FindUIByName(ingredientList[index].gameObject.name));
        ingredientList[index].GetComponent<XROffsetGrabInteractable>().colliders.Clear();
        Destroy(ingredientList[index]);
        ingredientList.RemoveAt(index);
        ChangeRectSize(false);
    }


    void KraPaoMooSubCheck()
    {
        var chillicheck = FindDoneIngredient(chilli);
        var garlicheck = FindDoneIngredient(garlic);
        var porkcheck = FindDoneIngredient(pork);
        var basilcheck = FindDoneIngredient(basil);
        if ((chillicheck != -1) && (garlicheck != -1) && (porkcheck != -1) && (basilcheck != -1))
        {
            print("Player made Ka-Prao-Moo-Sub");
            spawn = Instantiate(PrefabKrapaoMoosub, transform.position, Quaternion.identity) as GameObject;
            spawn.transform.parent = null;

            //delete ingredient
            DestroyIngredient(chilli, chillicheck);
            DestroyIngredient(garlic, garlicheck);
            DestroyIngredient(pork, porkcheck);
            DestroyIngredient(basil, basilcheck);
            source.PlayOneShot(DoneFood);
        }
    }

    void TomYumKungCheck()
    {
        var takraicheck = FindDoneIngredient(takrai);
        var khacheck = FindDoneIngredient(kha);
        var shrimpcheck = FindDoneIngredient(shrimp);
        var homdangcheck = FindDoneIngredient(homdang);
        var magroodcheck = FindDoneIngredient(magrood);
        var hedfangcheck = FindDoneIngredient(hedfang);
        var limecheck = FindDoneIngredient(lime);
        //var milkcheck = FindDoneOneIngredient(milkSpawn);
        //var numpracheck = FindDoneOneIngredient(numplaSpawn);
        //var sugarcheck = FindDoneOneIngredient(sugarSpawn);
        //var makamheck = FindDoneOneIngredient(MaKamSpawn);
        checkIsHaveToChangColor(takraitext, takrai.Count);
        checkIsHaveToChangColor(khatext, kha.Count);
        checkIsHaveToChangColor(shrimptext, shrimp.Count);
        checkIsHaveToChangColor(homdangtext, homdang.Count);
        checkIsHaveToChangColor(hedfangtext, hedfang.Count);
        checkIsHaveToChangColor(limetext, lime.Count);
        checkIsHaveToChangColor(magroodtext, magrood.Count);
        checkIsHaveToChangColor(milktext, GetWater.milkHitCount);
        checkIsHaveToChangColor(numpratext, GetWater.NumplaHitCount);
        checkIsHaveToChangColor(sugartext, GetWater.SugarHitCount);
        checkIsHaveToChangColor(makamtext, GetWater.MakamHitCount);
        checkwaterstatustochangecolortext(prikpaotext, GetWater.WaterStatus);

        if ((takraicheck != -1) && (khacheck != -1) && (shrimpcheck != -1) && (homdangcheck != -1) && 
            (hedfangcheck != -1) && (limecheck != -1) && (milkSpawn != null) && (numplaSpawn != null) && (GetWater.WaterStatus==1) && 
            (sugarSpawn != null) && (MaKamSpawn != null) && (magroodcheck != -1))
        {
            print("Player made Tom-Yum-Kung");
            PrefabTomYumKung.SetActive(true);
            TomYumKungControl TYKcontrol = PrefabTomYumKung.GetComponent<TomYumKungControl>();
            if (milkSpawn.GetComponent<IngredientControl>().isDone)
                TYKcontrol.milkcheck = true;
            if (sugarSpawn.GetComponent<IngredientControl>().isDone)
                TYKcontrol.sugarcheck = true;
            if (numplaSpawn.GetComponent<IngredientControl>().isDone)
                TYKcontrol.numplacheck = true;
            if (MaKamSpawn.GetComponent<IngredientControl>().isDone)
                TYKcontrol.makamcheck = true;

            DestroyIngredient(takrai,takraicheck);
            DestroyIngredient(kha, khacheck);
            DestroyIngredient(shrimp, shrimpcheck);
            DestroyIngredient(homdang, homdangcheck);
            DestroyIngredient(hedfang, hedfangcheck);
            DestroyIngredient(lime, limecheck);
            DestroyIngredient(magrood, magroodcheck);


            //destroy milk
            milkSpawn.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            Destroy(UI[FindUIByName(milkSpawn.name)]);
            UI.RemoveAt(FindUIByName(milkSpawn.name));
            ChangeRectSize(false);
            milkFlag = false;
            milkTemp = 0;
            GetWater.milkHitCount = 0;
            //destroy sugar
            sugarSpawn.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            Destroy(UI[FindUIByName(sugarSpawn.name)]);
            UI.RemoveAt(FindUIByName(sugarSpawn.name));
            ChangeRectSize(false);
            sugarFlag = false;
            sugarTemp = 0;
            GetWater.SugarHitCount = 0;

            //destroy numpla    
            numplaSpawn.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            Destroy(UI[FindUIByName(numplaSpawn.name)]);
            UI.RemoveAt(FindUIByName(numplaSpawn.name));
            ChangeRectSize(false);
            numplaFlag = false;
            numplaTemp = 0;
            GetWater.NumplaHitCount = 0;

            //destroy makam
            MaKamSpawn.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            Destroy(UI[FindUIByName(MaKamSpawn.name)]);
            UI.RemoveAt(FindUIByName(MaKamSpawn.name));
            ChangeRectSize(false);
            MaKamflag = false;
            MaKamtemp = 0;
            GetWater.MakamHitCount = 0;

            Destroy(milkSpawn);
            Destroy(sugarSpawn);
            Destroy(numplaSpawn);
            Destroy(MaKamSpawn);


            //prikpao
            prikpaoflag = false;
            GetWater.PrikPaoHitCount = 0;

            GetWater.ResetWater();

            source.PlayOneShot(DoneFood);
        }
    }

    void checkwaterstatustochangecolortext(Text text, int waterstatus)
    {
        if(waterstatus==1 && isBoiling)
        {
            setTextColor(text, Color.green);
        }
        else
        {
            setTextColor(text, Color.red);
        }
    }

    void checkIsHaveToChangColor(Text text, int numofingredient)
    {
        if (numofingredient > 0)
        {
            setTextColor(text, Color.green);
        }
        else
        {
            setTextColor(text, Color.red);
        }
    }

    void setTextColor(Text text,Color color)
    {
        text.color = color;
    }

}
