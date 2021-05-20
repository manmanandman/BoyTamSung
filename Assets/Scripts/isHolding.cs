using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHolding : MonoBehaviour
{
    private string temptag;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(collision.gameObject.tag != null)
            {
                temptag = collision.gameObject.tag;
            }

            collision.gameObject.tag = "IsHolding";
            //Debug.Log(collision+" is now "+ collision.gameObject.tag);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if(temptag != null)
            {
                collision.gameObject.tag = temptag;
            }
            //Debug.Log(collision + " is now " + collision.gameObject.tag);
        }
    }

}
