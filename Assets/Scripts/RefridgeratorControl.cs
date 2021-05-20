using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefridgeratorControl : MonoBehaviour
{
    public List<GameObject> iteminrefridgerator = new List<GameObject>();

    private Transform initTransform;
    // Start is called before the first frame update
    void Start()
    {
        initTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(initTransform.rotation.eulerAngles.y > 260)
        {
            foreach (GameObject iteminrefrid in iteminrefridgerator)
            {
                iteminrefrid.GetComponent<SpawnWhenChangePos>().enabled = true;
            }
        }
        else 
        {
            foreach (GameObject iteminrefrid in iteminrefridgerator)
            {
                iteminrefrid.GetComponent<SpawnWhenChangePos>().enabled = false;
            }
        }

    }


}
