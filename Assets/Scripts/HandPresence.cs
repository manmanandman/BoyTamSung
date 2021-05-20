using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public XRRayInteractor RayInteractor;
    public GameSystem gameSystem;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    private bool menuLastButtonState = false;
    private bool aLastButtonState = false;
    private bool triggerLastButtonState = false;
    private bool gripLastButtonState = false;
    public int triggerPressedTime = 0;
    public int gripPressedTime = 0;
    public bool menubutton = false;
    public bool abutton = false;

    public Vector2 primary2DAxisValue = new Vector2(0f,0f);

    public bool enableMagicSpawnerCanvas = true;
    public GameObject MagicCanvas;
    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
        gameSystem = GameObject.Find("GameSystem").GetComponent<GameSystem>();
        enableMagicSpawnerCanvas = true;
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
            //Debug.Log(item.name + item.characteristics);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller model");
                spawnedController = Instantiate(controllerPrefabs[6], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    void UpdateRayInteractor()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            RayInteractor.gameObject.SetActive(true);
        }
        else
        {
            RayInteractor.gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue) && menuButtonValue)
        //{
        //    Debug.Log("Pressing Menu Button");
        //    gameSystem.PauseGame();
        //}

        // menu button pressed
        bool menuState = false;
        bool menuValue = false;
        menuState = targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuValue) // did get a value
                    && menuValue // the value we got
                    || menuState; // cumulative result from other controllers

        if (menuState != menuLastButtonState) // Button state changed since last frame // trigger when change state
        {
            if (menuState)
            {
                menubutton = !menubutton;
                GameManager.Instance.TriggerPlayPause();
            }
                //Debug.Log("Pressing Menu Button");
            menuLastButtonState = menuState;
        }

        // trigger button pressed
        bool triggerState = false;
        float triggerValue = 0.1f;
            triggerState = targetDevice.TryGetFeatureValue(CommonUsages.trigger, out triggerValue) // did get a value
                        && triggerValue > 0.1f // the value we got
                        || triggerState; // cumulative result from other controllers

        if (triggerState != triggerLastButtonState) // Button state changed since last frame // trigger when change state
        {
            if(triggerState)
                triggerPressedTime += 1;
            triggerLastButtonState = triggerState;
        }

        // a button pressed
        bool aState = false;
        bool aValue = false;
            aState = targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out aValue) // did get a value
                    && aValue // the value we got
                    || aState; // cumulative result from other controllers

        if (aState != aLastButtonState) // Button state changed since last frame // trigger when change state
        {
            if (aState)
            {
                abutton = !abutton;
                TriggerMagicCanvas();
                //gameSystem.debugLog.text = "GetValue" + aState.ToString();
                Debug.Log("Pressing Menu Button");
            }

            aLastButtonState = aState;
        }


        // grip button pressed
        bool gripState = false;
        float gripValue = 0.1f;
        gripState = targetDevice.TryGetFeatureValue(CommonUsages.grip, out gripValue) // did get a value
                    && gripValue > 0.1f // the value we got
                    || gripState; // cumulative result from other controllers

        if (gripState != gripLastButtonState) // Button state changed since last frame
        {
            if(gripState)
                gripPressedTime += 1;
            gripLastButtonState = gripState;
        }


        //if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) 
        //    Debug.Log("Trigger Pressed " + triggerValue);

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            //Debug.Log("Primary Touchpad " + primary2DAxisValue);
        }



        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
        //UpdateRayInteractor();


    }

    public void TriggerMagicCanvas()
    {
        if(enableMagicSpawnerCanvas)
        {
            StartCoroutine(OnOffCanvas());
            //if (MagicCanvas.activeSelf) //want to open
            //{
            //    MagicCanvas.gameObject.SetActive(false);
            //}
            //else
            //{
            //    MagicCanvas.gameObject.SetActive(true);
            //}
            if(MagicCanvas.GetComponent<Canvas>().enabled == true)
            {
                MagicCanvas.GetComponent<Canvas>().enabled = false;
            }
            else
            {
                MagicCanvas.GetComponent<Canvas>().enabled = true;
            }
        }
        MagicCanvas.GetComponent<MagicCanvasController>().UpdateUnlocking();
    }

    IEnumerator OnOffCanvas()
    {
        enableMagicSpawnerCanvas = false;
        yield return new WaitForSecondsRealtime(0.25f);
        enableMagicSpawnerCanvas = true;
    }

}
