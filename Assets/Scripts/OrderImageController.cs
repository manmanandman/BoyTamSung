using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderImageController : MonoBehaviour
{
    public NpcController npcController;
    public Image[] image;
    public Sprite[] sprites;

    private int count;
    void Start()
    {
        image[0].sprite = null;
        image[0].enabled = false;
        image[1].sprite = null;
        image[1].enabled = false;
        image[2].sprite = null;
        image[2].enabled = false;

        npcController = this.GetComponentInParent<NpcController>();
        count = npcController.orderCount;

        int i = 0;
        foreach (string order in npcController.orderRandom)
        {
            if (!order.Equals("GO_OUT"))
            {
                foreach (Sprite sprite in sprites)
                {
                    if (sprite.name.Equals("IMG_" + order))
                    {
                        image[i].sprite = sprite;
                        image[i].enabled = true;
                        i++;
                        break;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (npcController.orderCount - npcController.orderServed < count)
        {
            count--;
            int i = 0;
            foreach (string order in npcController.orderRandom)
            {
                if (!order.Equals("GO_OUT"))
                {
                    foreach (Sprite sprite in sprites)
                    {
                        if (sprite.name.Equals("IMG_" + order))
                        {
                            image[i].sprite = sprite;
                            image[i].enabled = true;
                            i++;
                            break;
                        }
                    }
                }
            }
            image[count].enabled = false;
        }
        if (npcController.orderFault >= npcController.orderCount - npcController.orderServed || npcController.gameSystem.end)
        {
            image[0].enabled = false;
            image[1].enabled = false;
            image[2].enabled = false;
        }
    }
}
