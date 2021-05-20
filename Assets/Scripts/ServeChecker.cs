using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ServeChecker : MonoBehaviour
{
    public GameSystem gameSystem;
    //public string orderName = null;
    //public bool isServe = false;
    public List<string> orderRandom;
    public NpcController npcCon;

    public GameObject objectTrigger = null;
    //public List<GameObject> listTrigger = null;
    //public bool destroy = false;
    public bool trueOrder;

    //private int i = 0;
    void Start()
    {
        trueOrder = false;
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
    }

    private void Update()
    {
        //if(destroy && !gameSystem.end)
        //{
        //    GameObject temp = listTrigger.Find(x => x.name.Equals(orderName));
        //    try
        //    {
        //        temp.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
        //    }
        //    catch
        //    {
        //        temp.GetComponent<XRGrabInteractable>().colliders.Clear();
        //    }
        //    if (temp != null)
        //    {
        //        orderName = null;
        //        print("Delete " + temp.name);
        //        listTrigger.Remove(temp);
        //        Destroy(temp);
        //    }
        //    destroy = false;
        //    isServe = false;
        //    objectTrigger = null;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //isServe = true;
        //objectTrigger = collision.gameObject;
        //GameObject temp = objectTrigger;
        //temp.name = temp.name + "~" + i;
        //i++;
        //print("Got " + temp.name);
        //listTrigger.Add(temp);
        if ((!gameSystem.end || gameSystem.currentLevel == 0) && npcCon.npcState == "WAITING")
        {
            objectTrigger = collision.gameObject;
            bool eggIsOK = objectTrigger.name.Contains("State2") || objectTrigger.name.Contains("State3");
            bool hasTasteControl = objectTrigger.gameObject.GetComponent<BowlControl>();
            bool trueOrderButFalseState = false;
            string netOrderName = objectTrigger.name.Split('(')[0];
            bool trueOrder = false;
            int orderIndex = 0;
            int orderIndexFalseState = 0;
            foreach (string order in orderRandom)
            {
                if (netOrderName.Split('_')[0].Equals(order.Split('_')[0]))
                {
                    trueOrderButFalseState = true;
                    orderIndexFalseState = orderIndex;
                }
                if (order.Equals(netOrderName))
                {
                    trueOrder = true;
                    break;
                }
                orderIndex++;
            }
            if (trueOrder)
            {
                float rawScore = LevelData.orderScores[objectTrigger.name];
                orderRandom[orderIndex] = "GO_OUT";
                npcCon.orderRandom[orderIndex] = "GO_OUT";
                //gameSystem.debugLog.text = hasTasteControl.ToString();
                if (hasTasteControl)
                {
                    float scoreaddon = 0f;
                    if (!objectTrigger.GetComponent<BowlControl>().numplacheck)
                    {
                        scoreaddon += -5f;
                    }
                    if (!objectTrigger.GetComponent<BowlControl>().sugarcheck)
                    {
                        scoreaddon += -5f;
                    }
                    if (!objectTrigger.GetComponent<BowlControl>().milkcheck)
                    {
                        scoreaddon += -5f;
                    }
                    if (!objectTrigger.GetComponent<BowlControl>().makamcheck)
                    {
                        scoreaddon += -5f;
                    }
                    npcCon.ScoringTrueOrder(rawScore + scoreaddon);
                }
                else
                {
                    npcCon.ScoringTrueOrder(rawScore);
                }
                npcCon.SetXmark();
                npcCon.textScore.color = Color.green;
                try
                {
                    collision.gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
                }
                catch
                {
                    collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
                }
                Destroy(collision.gameObject);
                objectTrigger = null;
            }
            else if (eggIsOK && trueOrderButFalseState)
            {
                float rawScore = LevelData.orderScores[objectTrigger.name] - 20f;
                orderRandom[orderIndexFalseState] = "GO_OUT";
                npcCon.orderRandom[orderIndexFalseState] = "GO_OUT";
                npcCon.ScoringTrueOrder(rawScore);
                npcCon.SetXmark();
                npcCon.textScore.color = Color.green;
                try
                {
                    collision.gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
                }
                catch
                {
                    collision.gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
                }
                Destroy(collision.gameObject);
                objectTrigger = null;
            }
            else if (objectTrigger.CompareTag("Food")) //wrong food
            {
                //source.PlayOneShot(bellFail);
                npcCon.npcStatusForSound = "wrongfoodbutkeepwait";
                npcCon.orderFault++;
                gameSystem.UpdateScore(-35f, 0f, 0f, 0, 1, 0);
                npcCon.SetXmark();
                npcCon.textScore.color = Color.red;
                npcCon.textScore.text = "-35";
                npcCon.ProgressCanvas.GetComponent<Canvas>().enabled = false;
                if (npcCon.orderFault >= npcCon.orderCount - npcCon.orderServed)
                {
                    npcCon.npcState = "FALSE_ORDER";
                    if (gameSystem.currentLevel == gameSystem.lastLevel)
                    {
                        gameSystem.deathCount += 1;
                    }
                    npcCon.customerAnimator.SetBool("isWrong", true);
                    npcCon.npcStatusForSound = "wrongfood";
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //isServe = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //listTrigger.Remove(collision.gameObject);
        //isServe = false;
        //objectTrigger = null;
    }

    
}
