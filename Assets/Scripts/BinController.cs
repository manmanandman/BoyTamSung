using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    public List<string> tagToDelete = new List<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(tagToDelete.Contains(collision.gameObject.tag))
        {
            Debug.Log("Delete");
        }
        else
        {   
            if(collision.gameObject.GetComponent<DestoryOnFloor>())
            {
                collision.gameObject.GetComponent<DestoryOnFloor>().isOnFloor = true;
            }
            Debug.Log("No Delete");
        }

    }

    
}
