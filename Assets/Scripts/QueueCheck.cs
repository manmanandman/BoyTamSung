using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

public class QueueCheck : MonoBehaviour
{
    public GameSystem gameSystem;

    public Transform queuePosition1;
    public Transform queuePosition2;
    public Transform queuePosition3;
    public Transform queuePosition4;
    public Transform npcSpawnLeft;
    public Transform npcSpawnRight;
    public GameObject npc;
    public bool isQueue1;
    public bool isQueue2;
    public bool isQueue3;
    public bool isQueue4;

    private bool lr;
    private float countdownNewNpc = 0f;
    private int currentLevel;
    private bool onoff;
    private bool toggleOnOff;
    private List<ArrayList> levelOrders;
    private int currentOrder;

    private void Awake()
    {
        currentLevel = GameManager.Instance.currentLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        levelOrders = new List<ArrayList>() { };
        currentOrder = 0;
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        if (currentLevel != 0)
        {
            onoff = true;
            countdownNewNpc = 0f;
        }
        else
        {
            onoff = false;
        }
        isQueue1 = false;
        isQueue2 = false;
        isQueue3 = false;
        isQueue4 = false;
        toggleOnOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOrder < LevelData.levelOrdersFixed[currentLevel].Length)
        {
            if ((float)LevelData.levelDetails[currentLevel][1] - gameSystem.timeRemain >= (float)LevelData.levelOrdersFixed[currentLevel][currentOrder][0])
            {
                levelOrders.Add(LevelData.levelOrdersFixed[currentLevel][currentOrder]) ;
                currentOrder++;
            }
        }
        else if (!gameSystem.random)
        {
            gameSystem.random = true;
            countdownNewNpc = 10f;
        }
        if ((gameSystem.timeRemain > 0 || currentLevel == 0) && onoff && !gameSystem.end)
        {
            lr = Random.value < 0.5;
            if (currentLevel == 0 || gameSystem.random) //TUTORIAL
            {
                if (countdownNewNpc <= 0 && !(isQueue1 && isQueue2 && isQueue3 && isQueue4))
                {
                    if (lr)
                    {
                        Instantiate(npc, npcSpawnLeft);
                    }
                    else
                    {
                        Instantiate(npc, npcSpawnRight);
                    }
                    if (currentLevel == 0)
                    {
                        countdownNewNpc = 0.1f;
                        onoff = toggleOnOff;
                    }
                    else
                    {
                        countdownNewNpc = 10f;
                    }
                }
                else
                {
                    countdownNewNpc -= 1 * Time.deltaTime;
                }
            } //
            else if (levelOrders.Count != 0 && !(isQueue1 && isQueue2 && isQueue3 && isQueue4))
            {
                GameObject npcInit;
                if (lr)
                {
                    npcInit = Instantiate(npc, npcSpawnLeft);
                }
                else
                {
                    npcInit = Instantiate(npc, npcSpawnRight);
                }
                foreach(string order in (List<string>)levelOrders[0][1])
                {
                    npcInit.GetComponent<NpcController>().orderRandom.Add(order);
                }
                npcInit.GetComponent<NpcController>().orderCount = ((List<string>)levelOrders[0][1]).Count;
                npcInit.GetComponent<NpcController>().timeCountdown = (float)levelOrders[0][2];
                levelOrders.RemoveAt(0);
            }
        }
    }

    public void SpawnOneNpc()
    {
        onoff = true;
        toggleOnOff = false;
    }

    public void SpawnMoreNpc()
    {
        onoff = true;
        toggleOnOff = true;
    }
}
