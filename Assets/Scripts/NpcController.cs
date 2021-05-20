using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public GameSystem gameSystem;

    public Text textScore;
    public Animator customerAnimator;
    public Transform npcModel;
    public float speed = 0.02f;
    public float range = 0.03f;
    public float timeCountdown = 20f;
    public QueueCheck queueCheck;
    public NpcCollision npcCollision;
    public NpcHitCheck hitCheck;
    public ServeChecker serveOnDesk;
    public List<string> orderRandom = new List<string>();
    public GameObject ProgressCanvas;

    //public List<GameObject> npcPrefab = new List<GameObject>();
    //public List<Material> shirt = new List<Material>();
    public int orderFault = 0;
    public int orderCount;
    public int orderServed = 0;

    private Transform queuePosition;
    private int queue;
    public string npcState;
    private string npcOldState;
    private Vector3 startPosition;
    private float countdownJump = 2.2f;
    private float countdownAngry = 4.7f;
    private bool isGoingToLeft;
    private float turningTime;
    private float timeCountdownInit;
    //private bool WasPlaySound = false;
    private bool wave = false;
    private GameObject npc;
    private int currentLevel;
    private float delayServe = 0.8f;
    private float delayServeInit;
    private float rawScoreTotal = 0f;

    // Sound NPC
    [Header("SoundEffect")]
    private string tempState = "";
    public string npcStatusForSound = "";
    public AudioSource source;
    public AudioClip punchsound;
    public AudioClip cashsound;
    public AudioClip bellDing;
    public AudioClip bellFail;

    public List<GameObject> NPCDetail = new List<GameObject>();
    public List<Sprite> Xmark = new List<Sprite>();

    int randomNPC;
    NpcDetail npcDetail;
    void Awake()
    {
        currentLevel = GameManager.Instance.currentLevel;
    }
    // Start is called before the first frame update
    void Start()
    {
        orderFault = 0;
        orderServed = 0;
        //Random NPC
        randomNPC = Random.Range(0, NPCDetail.Count);
        //Get NPC Detail Data
        npcDetail = NPCDetail[randomNPC].GetComponent<NpcDetail>();
        npc = Instantiate(npcDetail.NpcPrefab, this.transform);
        //Set NPC Shirt Color
        npc.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = npcDetail.ShirtMaterial;


        textScore = npc.transform.GetChild(9).transform.GetChild(0).GetComponent<Text>();
        customerAnimator = npc.GetComponent<Animator>();
        npcModel = npc.GetComponent<Transform>();
        npcCollision = npc.transform.GetChild(7).GetComponent<NpcCollision>();
        hitCheck = npc.GetComponent<NpcHitCheck>();
        hitCheck.npcCon = gameObject.GetComponent<NpcController>();
        ProgressCanvas = npc.transform.GetChild(10).gameObject;

        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        //Random order
        if (currentLevel == 0 || gameSystem.random)
        {
            var poolOrder = LevelData.levelOrders[currentLevel];
            float r = Random.value;
            if (r < 0.80)
            {
                orderCount = 1;
            }
            else if (r < 0.97)
            {
                orderCount = 2;
            }
            else
            {
                orderCount = 3;
            }
            timeCountdown = 0f;
            for (int i = 0; i < orderCount; i++)
            {
                string temp = (string)poolOrder[(int)(poolOrder.Count * Random.value)];
                orderRandom.Add(temp);
                timeCountdown += LevelData.orderTimeRemain[temp];
            }
        }
        timeCountdownInit = timeCountdown;
        delayServeInit = delayServe;
        ProgressCanvas.GetComponent<ProgressbarControl>().current = timeCountdownInit;
        ProgressCanvas.GetComponent<ProgressbarControl>().maximum = timeCountdownInit;
        ProgressCanvas.GetComponent<Canvas>().enabled = false;

        ProgressCanvas.transform.GetChild(2).gameObject.SetActive(false);
        ProgressCanvas.transform.GetChild(3).gameObject.SetActive(false);

        for (int i = 0;i < orderCount; i++)
        {
            if (i != 0)
            {
                ProgressCanvas.transform.GetChild(i + 1).gameObject.SetActive(true);
                ProgressCanvas.transform.GetChild(i + 1).gameObject.GetComponent<Image>().sprite = Xmark[0];
            }
        }

        npcState = "WALKING_FROM_SPAWN";
        queueCheck = GameObject.Find("QueuePosition").GetComponent<QueueCheck>();
        startPosition = this.transform.position;
        if (startPosition.x < queueCheck.queuePosition4.position.x) // npc come from left
        {
            if (!queueCheck.isQueue4)
            {
                queuePosition = queueCheck.queuePosition4;
                queueCheck.isQueue4 = true;
                queue = 4;
                serveOnDesk = GameObject.Find("Serve4").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue3)
            {
                queuePosition = queueCheck.queuePosition3;
                queueCheck.isQueue3 = true;
                queue = 3;
                serveOnDesk = GameObject.Find("Serve3").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue2)
            {
                queuePosition = queueCheck.queuePosition2;
                queueCheck.isQueue2 = true;
                queue = 2;
                serveOnDesk = GameObject.Find("Serve2").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue1)
            {
                queuePosition = queueCheck.queuePosition1;
                queueCheck.isQueue1 = true;
                queue = 1;
                serveOnDesk = GameObject.Find("Serve1").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
        }
        else if (startPosition.x > queueCheck.queuePosition1.position.x) // npc come from right
        {
            if (!queueCheck.isQueue1)
            {
                queuePosition = queueCheck.queuePosition1;
                queueCheck.isQueue1 = true;
                queue = 1;
                serveOnDesk = GameObject.Find("Serve1").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue2)
            {
                queuePosition = queueCheck.queuePosition2;
                queueCheck.isQueue2 = true;
                queue = 2;
                serveOnDesk = GameObject.Find("Serve2").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue3)
            {
                queuePosition = queueCheck.queuePosition3;
                queueCheck.isQueue3 = true;
                queue = 3;
                serveOnDesk = GameObject.Find("Serve3").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
            else if (!queueCheck.isQueue4)
            {
                queuePosition = queueCheck.queuePosition4;
                queueCheck.isQueue4 = true;
                queue = 4;
                serveOnDesk = GameObject.Find("Serve4").GetComponent<ServeChecker>();
                serveOnDesk.orderRandom = orderRandom;
                serveOnDesk.npcCon = gameObject.GetComponent<NpcController>();
            }
        }
        serveOnDesk.transform.GetChild(0).gameObject.SetActive(false);
        //serveOnDesk.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        PlaySound();
        if (gameSystem.end && npcState != "END")
        {
            clearQueue();
            ProgressCanvas.GetComponent<Canvas>().enabled = false;
            for (int i = 0; i < orderCount; i++)
            {
                orderRandom[i] = "GO_OUT";
            }
            npcState = "END";
            if (gameSystem.pass)
            {
                customerAnimator.SetInteger("success", 1);
            }
            else
            {
                customerAnimator.SetInteger("success", -1);
            }
        }

        if (npcState == "WALKING_FROM_SPAWN") //walk before rotate to the shop
        {
            if (npcCollision.isCol)
            {
                npcState = "START_COLLISION"; // turn npc to not collision with another npc
                npcOldState = "WALKING_FROM_SPAWN";

                if (this.transform.position.x < queuePosition.position.x - range)
                {
                    isGoingToLeft = false;
                }
                else
                {
                    isGoingToLeft = true;
                }
            }

            if (this.transform.position.x < queuePosition.position.x - range) // going to right
            {
                if (npcModel.rotation.eulerAngles.y != 90)
                {
                    npcModel.rotation = Quaternion.Euler(0, 90, 0);
                }
                this.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (this.transform.position.x > queuePosition.position.x + range) // going to left
            {
                if (npcModel.rotation.eulerAngles.y != 270)
                {
                    npcModel.rotation = Quaternion.Euler(0, 270, 0);
                }
                this.transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                npcState = "WALKING_TO_COUNTER";
            }
        }   
        else if (npcState == "WALKING_TO_COUNTER") //walk to stop spot
        {
            if (this.transform.position.z > queuePosition.position.z + range)
            {
                if (npcModel.rotation.eulerAngles.y != 180)
                {
                    npcModel.rotation = Quaternion.Euler(0, 180, 0);
                }
                this.transform.position += Vector3.back * speed * Time.deltaTime;
            }
            else
            {
                npcState = "WAITING";
                npcStatusForSound = "waiting";
            }
        }
        else if (npcState == "WAITING") //waiting for order
        {
            timeCountdown -= 1 * Time.deltaTime;
            if (!wave && timeCountdown < timeCountdownInit / 2)
            {
                customerAnimator.SetBool("isWave", true);
                wave = true;
                npcStatusForSound = "waving";
            }
            else if (wave)
            {
                customerAnimator.SetBool("isWave", false);
            }
            ProgressCanvas.GetComponent<Canvas>().enabled = true;
            ProgressCanvas.GetComponent<ProgressbarControl>().current = timeCountdown;
            customerAnimator.SetBool("isWait", true);
            if (timeCountdown > 0 && !gameSystem.end)
            {
                if (!serveOnDesk.transform.GetChild(0).gameObject.activeSelf)
                {
                    serveOnDesk.transform.GetChild(0).gameObject.SetActive(true);
                }
                serveOnDesk.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = ((int)timeCountdown).ToString();
                if (hitCheck.hit)
                {
                    gameSystem.UpdateScore(-50f, 0f, 0f, 0, 0, 1);
                    textScore.color = Color.red;
                    textScore.text = "-50";
                    ProgressCanvas.GetComponent<Canvas>().enabled = false;
                    for (int i = 0; i < orderCount; i++)
                    {
                        orderRandom[i] = "GO_OUT";
                    }
                    npcState = "FALSE_ORDER";
                    if (currentLevel == gameSystem.lastLevel)
                    {
                        gameSystem.deathCount += 1;
                    }
                    countdownAngry += 1f;
                    customerAnimator.SetBool("isHit", true);
                    npcStatusForSound = "hit";
                }
                //if (serveOnDesk.isServe && serveOnDesk.objectTrigger != null)
                //{
                //    serveOnDesk.isServe = false;
                //    serveOnDesk.objectTrigger = null;
                //}

            }
            else if(!gameSystem.end)
            {
                gameSystem.UpdateScore(-20f, 0f, 0f, 0, 1, 0);
                textScore.color = Color.red;
                textScore.text = "-20";
                //if (!WasPlaySound)
                //{
                //    //source.PlayOneShot(angry);
                //    WasPlaySound = true;
                //}
                ProgressCanvas.GetComponent<Canvas>().enabled = false;
                for (int i = 0; i < orderCount; i++)
                {
                    orderRandom[i] = "GO_OUT";
                }
                npcState = "FALSE_ORDER";
                if (currentLevel == gameSystem.lastLevel)
                {
                    gameSystem.deathCount += 1;
                }
                npcStatusForSound = "timeout";
                customerAnimator.SetBool("isWrong", true);
            }
        }
        else if (npcState == "TRUE_ORDER") //get true order then jump
        {
            if(serveOnDesk.transform.GetChild(0).gameObject.activeSelf)
            {
                StartCoroutine(ClosePanel());
            }
            //serveOnDesk.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
            //if (!WasPlaySound)
            //{
            //    //source.PlayOneShot(cashsound);
            //    WasPlaySound = true;
            //}
            npcStatusForSound = "cashing";
            customerAnimator.SetBool("isOrder", true);
            if (countdownJump <= 0)
            {
                clearQueue();
                npcStatusForSound = "walkingouthappy";
                npcState = "WALKING_FROM_COUNTER";
            }
            countdownJump -= 1 * Time.deltaTime;
        }
        else if (npcState == "FALSE_ORDER") //get false order angry
        {
            if (serveOnDesk.transform.GetChild(0).gameObject.activeSelf)
            {
                StartCoroutine(ClosePanel());
            }
            //serveOnDesk.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
            if (countdownAngry <= 0)
            {
                clearQueue();
                npcStatusForSound = "walkingouthappy";
                npcState = "WALKING_FROM_COUNTER";
            }
            countdownAngry -= 1 * Time.deltaTime;
        }
        else if (npcState == "WALKING_FROM_COUNTER") //rotate and walk out
        {
            customerAnimator.SetBool("isWait", false);
            customerAnimator.SetBool("goOut", true);
            if (npcModel.rotation.eulerAngles.y != 0)
            {
                npcModel.rotation = Quaternion.Euler(0, 0, 0);
            }
            this.transform.position += Vector3.forward * speed * Time.deltaTime;
            if (this.transform.position.z >= startPosition.z - range)
            {
                npcState = "WALKING_OUT_OF_GAME";
            }
        }
        else if (npcState == "WALKING_OUT_OF_GAME") //walk out the screen
        {
            if (npcCollision.isCol)
            {
                npcState = "START_COLLISION"; // turn npc to not collision with another npc
                npcOldState = "WALKING_OUT_OF_GAME";

                if (this.transform.position.x < startPosition.x - range)
                {
                    isGoingToLeft = false;
                }
                else
                {
                    isGoingToLeft = true;
                }
            }

            if (this.transform.position.x < startPosition.x - range) // going to right
            {
                if (npcModel.rotation.eulerAngles.y != 90)
                {
                    npcModel.rotation = Quaternion.Euler(0, 90, 0);
                }
                this.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (this.transform.position.x > startPosition.x + range) // going to left
            {
                if (npcModel.rotation.eulerAngles.y != 270)
                {
                    npcModel.rotation = Quaternion.Euler(0, 270, 0);
                }
                this.transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        else if (npcState == "START_COLLISION")
        {
            npcModel.Rotate(0, -45, 0);
            turningTime = 0.4f;
            npcState = "WALKING_FROM_COLLISION";
        }
        else if (npcState == "WALKING_FROM_COLLISION")
        {
            if(npcOldState == "WALKING_FROM_SPAWN") // check if this x is out of counter range
            {
                if (this.transform.position.x >= queuePosition.position.x - range && this.transform.position.x <= queuePosition.position.x + range)
                {
                    npcState = "WALKING_TO_COUNTER";
                }
            }
            if (npcOldState == "WALKING_OUT_OF_GAME") // check if it back to spawn
            {
                if (this.transform.position.x >= startPosition.x - range && this.transform.position.x <= startPosition.x + range)
                {
                    Destroy(gameObject);
                }
            }
            if (turningTime > 0)
            {
                if (isGoingToLeft)
                {
                    this.transform.position += new Vector3 (-1 / Mathf.Sqrt(2), 0,-1 / Mathf.Sqrt(2)) * speed * Time.deltaTime;
                    //this.transform.Translate(-speed * Time.deltaTime / Mathf.Sqrt(2), 0, -speed * Time.deltaTime / Mathf.Sqrt(2));
                }
                else
                {
                    this.transform.position += new Vector3(1 / Mathf.Sqrt(2), 0, 1 / Mathf.Sqrt(2)) * speed * Time.deltaTime;
                    //this.transform.Translate(speed * Time.deltaTime / Mathf.Sqrt(2), 0, speed * Time.deltaTime / Mathf.Sqrt(2));
                }
                turningTime -= 1 * Time.deltaTime;
            }
            else
            {
                npcState = "TURNING_TO_LINE";
            }
        }
        else if (npcState == "TURNING_TO_LINE")
        {
            npcModel.Rotate(0, 90, 0);
            turningTime = 0.4f;
            npcState = "WALKING_TO_LINE";
        }
        else if (npcState == "WALKING_TO_LINE")
        {
            if (npcOldState == "WALKING_FROM_SPAWN") // check if this x is correct
            {
                if (this.transform.position.x >= queuePosition.position.x - range && this.transform.position.x <= queuePosition.position.x + range)
                {
                    npcState = "WALKING_TO_COUNTER";
                }
            }
            if (npcOldState == "WALKING_OUT_OF_GAME") // check if it back to spawn
            {
                if(this.transform.position.x >= startPosition.x - range && this.transform.position.x <= startPosition.x + range)
                {
                    Destroy(gameObject);
                }
            }
            if (turningTime > 0)
            {
                if (isGoingToLeft)
                {
                    this.transform.position += new Vector3(-1 / Mathf.Sqrt(2), 0, 1 / Mathf.Sqrt(2)) * speed * Time.deltaTime;
                    //this.transform.Translate(-speed * Time.deltaTime / Mathf.Sqrt(2), 0, speed * Time.deltaTime / Mathf.Sqrt(2));
                }
                else
                {
                    this.transform.position += new Vector3(1 / Mathf.Sqrt(2), 0, -1 / Mathf.Sqrt(2)) * speed * Time.deltaTime;
                    //this.transform.Translate(speed * Time.deltaTime / Mathf.Sqrt(2), 0, -speed * Time.deltaTime / Mathf.Sqrt(2));
                }
                turningTime -= 1 * Time.deltaTime;
            }
            else
            {
                npcState = npcOldState;
            }
        }
        else if (npcState == "DELAY_SERVE")
        {
            if (delayServe <= 0)
            {
                npcState = "WAITING";
                delayServe = delayServeInit;
            }
            delayServe -= 1 * Time.deltaTime;
        }
    }

    void clearQueue()
    {
        if (queue == 1)
        {
            queueCheck.isQueue1 = false;
        }
        else if (queue == 2)
        {
            queueCheck.isQueue2 = false;
        }
        else if (queue == 3)
        {
            queueCheck.isQueue3 = false;
        }
        else if (queue == 4)
        {
            queueCheck.isQueue4 = false;
        }
    }

    public void ScoringTrueOrder (float rawScore)
    {
        orderServed++;
        orderFault = 0;
        rawScoreTotal += rawScore;
        if (orderServed >= orderCount)
        {
            if (timeCountdown >= timeCountdownInit / 2)
            {
                gameSystem.UpdateScore(0f, rawScoreTotal, rawScoreTotal * 0.2f, 1, 0, 0);
                textScore.text = "+" + rawScoreTotal.ToString() + "\n+tip20%";
            }
            else
            {
                gameSystem.UpdateScore(0f, rawScoreTotal, 0f, 1, 0, 0);
                textScore.text = "+" + rawScoreTotal.ToString();
            }
            ProgressCanvas.GetComponent<Canvas>().enabled = false;
            npcState = "TRUE_ORDER";
        }
        else
        {
            npcStatusForSound = "correctfood";
            source.PlayOneShot(bellDing);
            npcState = "DELAY_SERVE";
        }
    }

    public void SetXmark()
    {
        ProgressCanvas.transform.GetChild(2).gameObject.SetActive(false);
        ProgressCanvas.transform.GetChild(3).gameObject.SetActive(false);

        for (int i = 0; i < orderCount - orderServed; i++)
        {
            if (i != 0)
            {
                ProgressCanvas.transform.GetChild(i + 1).gameObject.SetActive(true);
                ProgressCanvas.transform.GetChild(i + 1).gameObject.GetComponent<Image>().sprite = Xmark[0];
            }
        }

        for (int i = 0;i < orderFault; i++)
        {
            ProgressCanvas.transform.GetChild(i + 2).gameObject.GetComponent<Image>().sprite = Xmark[1];
        }
    }

    void PlaySound()
    {
        if(tempState != npcStatusForSound) // npc has change state
        {
            source.Stop();
            tempState = npcStatusForSound; // set new temp state
            //Debug.Log(gameObject.name + " changed to state " + npcStatusForSound);
            switch (npcStatusForSound)
            {
                case "waiting": // first time in counter
                    if(npcDetail.waitingSound)
                        source.PlayOneShot(npcDetail.waitingSound);
                    break;

                case "waving": // waving from wait long time    
                    if (npcDetail.waitingSound)
                        source.PlayOneShot(npcDetail.wavingSound);
                    break;

                case "hit":  // get hit
                    source.PlayOneShot(punchsound);
                    if (npcDetail.hitSound)
                        source.PlayOneShot(npcDetail.hitSound);
                    break;

                case "wrongfood": // serve wrong food
                    if (npcDetail.wrongfoodSound)
                        source.PlayOneShot(npcDetail.wrongfoodSound);
                    break;

                case "cashing": // serve complate all food
                    source.PlayOneShot(cashsound);
                    if (npcDetail.cashingSound)
                        source.PlayOneShot(npcDetail.cashingSound);
                    break;
                case "correctfood": // serve complate all food
                    source.PlayOneShot(bellDing);
                    if (npcDetail.correctfoodSound)
                        source.PlayOneShot(npcDetail.correctfoodSound);
                    break;
                case "wrongfoodbutkeepwait": // serve complate all food
                    source.PlayOneShot(bellFail);
                    if (npcDetail.wrongfoodkeepwaitSound)
                        source.PlayOneShot(npcDetail.wrongfoodkeepwaitSound);
                    break;
                case "timeout": // npc timeout very angry
                    if (npcDetail.timeoutSound)
                        source.PlayOneShot(npcDetail.timeoutSound);
                    break;

                default:
                    break;


            }

        }
    }

    IEnumerator ClosePanel ()
    {
        serveOnDesk.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("Close",true);
        yield return new WaitForSecondsRealtime(0.8f);
        serveOnDesk.transform.GetChild(0).gameObject.SetActive(false);
        serveOnDesk.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "";
    }
}
