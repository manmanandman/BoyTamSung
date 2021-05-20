using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeIngredientState : MonoBehaviour
{
    public GameObject NextState;
    public float TimeToChangeState = 5;
    public GameObject ProgressCanvas;
    public float time = 0;
    public Transform whiteEgg;
    public bool isFirstState = false;
    public AudioSource source;
    public AudioClip changestatesound;
    public Color color;

    private bool rotateCheck = false;
    private bool isplaysound = false;
    private GameObject PanCollision;
    private float HeatEgg = 0;
    private bool isInPan = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isplaysound && !isFirstState)
        {
            source.PlayOneShot(changestatesound);
            isplaysound = true;
        }
        ProgressCanvas.GetComponent<PanProgressControl>().current = 0;
        ProgressCanvas.GetComponent<PanProgressControl>().maximum = TimeToChangeState;
        ProgressCanvas.GetComponent<PanProgressControl>().FoodString = LevelData.foodNameTranslator[gameObject.name];
        ProgressCanvas.GetComponent<PanProgressControl>().NextStateString = "ต่อไป : " + LevelData.foodNameTranslator[NextState.name + "(Clone)"];
        ProgressCanvas.GetComponent<PanProgressControl>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressCanvas.transform.position = new Vector3(gameObject.transform.position.x, whiteEgg.position.y + 0.2f, gameObject.transform.position.z);

        ProgressCanvas.GetComponent<Canvas>().enabled = isInPan;
        if (isInPan && time < TimeToChangeState)
        {                 
            if (PanCollision.GetComponent<PanControl>().enabled)
            {
                if (PanCollision.GetComponentInChildren<FryControl>().isOil)
                    HeatEgg = PanCollision.GetComponent<PanControl>().HeatPan;
                else
                    HeatEgg = 0;
            }
            else
            {
                HeatEgg = 0;
            }
            time += HeatEgg / 2 * Time.deltaTime;

            ProgressCanvas.GetComponent<PanProgressControl>().current = time;
        }
        if(time > TimeToChangeState)
        {
            ProgressCanvas.SetActive(false);
            Instantiate(NextState, gameObject.transform.position, gameObject.transform.rotation);
            try
            {
                gameObject.GetComponent<XROffsetGrabInteractable>().colliders.Clear();
            }
            catch
            {
                gameObject.GetComponent<XRGrabInteractable>().colliders.Clear();
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pan"))
        {
            if (!rotateCheck)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                rotateCheck = true;
            }
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pan"))
        {
            PanCollision = collision.gameObject;
            isInPan = true;
            //this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pan"))
        {
            PanCollision = null;
            isInPan = false;
            rotateCheck = false;
        }

            
    }
}
