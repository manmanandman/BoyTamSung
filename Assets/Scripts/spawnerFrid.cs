using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerFrid : MonoBehaviour
{
    public List<GameObject> Ingredient = new List<GameObject>();
    GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnInFrid(int index)
    {
        spawn = Instantiate(Ingredient[index], transform.position, Quaternion.identity) as GameObject;
        spawn.transform.parent = null;
    }
}
