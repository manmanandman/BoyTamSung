using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class swipe_menu : MonoBehaviour
{

    public GameObject scrollbar;
    public Text pageText;
    public Button Left;
    public Button Right;
    public HandPresence RightHand;
    public HandPresence LeftHand;
    float scroll_pos = 0;
    public float[] pos;

    public Text RightDebug;
    bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if(flag)
        {
            scroll_pos = pos[this.GetComponent<SelectLevelControl>().reachedPage];
            flag = false;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            RightDebug.text = (RightHand.primary2DAxisValue.x).ToString();
            if (RightHand.primary2DAxisValue.x > 0.1 || RightHand.primary2DAxisValue.x < -0.1)
            {
                scrollbar.GetComponent<Scrollbar>().value += RightHand.primary2DAxisValue.x / 70;
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else if (LeftHand.primary2DAxisValue.x > 0.1 || LeftHand.primary2DAxisValue.x < -0.1)
            {
                scrollbar.GetComponent<Scrollbar>().value += LeftHand.primary2DAxisValue.x / 70;
                scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            }
            else
            {
                for (int i = 0; i < pos.Length; i++)
                {
                    if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                    {
                        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                        //print(i+1 + " / "+ pos.Length);
                        pageText.text = (i + 1).ToString() + " / " + (pos.Length).ToString();
                        if (i <= 0)
                            Left.GetComponent<Button>().interactable = false;
                        else
                            Left.GetComponent<Button>().interactable = true;
                        if (i + 1 >= pos.Length)
                            Right.GetComponent<Button>().interactable = false;
                        else
                            Right.GetComponent<Button>().interactable = true;
                    }
                }
                if (scroll_pos < 0)
                    scroll_pos = 0;
                if (scroll_pos > 1)
                    scroll_pos = 1;
            }
        }

    }


    public void buttonLeftClick()
    {
        float values = scrollbar.GetComponent<Scrollbar>().value;
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i-1], 0.5f);
                scroll_pos = pos[i - 1];
                Debug.Log("Change Select Level Page from " + i + " to " + (i - 1));
                break;
            }
        }
    }
    public void buttonRightClick()
    {
        float values = scrollbar.GetComponent<Scrollbar>().value;
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i + 1], 0.5f);
                scroll_pos = pos[i + 1];
                Debug.Log("Change Select Level Page from " + i + " to " + (i + 1));
                break;
            }
        }
    }
}
