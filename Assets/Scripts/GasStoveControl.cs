using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GasStoveControl : MonoBehaviour
{
    public GameObject Stove4;
    public List<GameObject> firePrefab = new List<GameObject>();
    


    private float rotationValue = 0;
    public int IsOnFire = 0;
    

    void Start()
    {
        //print(IsOnFire);
    }

    void Update()
    {
        rotationValue = Stove4.gameObject.transform.rotation.eulerAngles.z;

        if ((rotationValue >= 300) || (rotationValue <= 60)) // Gas stove is off
        {
            //Debug.Log("GasStove = off");
            foreach (GameObject fire in firePrefab)
            {
                fire.SetActive(false);
            }     
            IsOnFire = 0;
        }
        else if ((rotationValue >= 60) && (rotationValue <= 130)) // Gas stove is on with low fire
        {
            //Debug.Log("GasStove = on low");
            foreach (GameObject fire in firePrefab)
            {
                fire.SetActive(true);
                var main = fire.GetComponent<ParticleSystem>().main;
                main.simulationSpeed = 1f;
                main.startColor = Color.white;

            }
            IsOnFire = 1;
        }
        else if ((rotationValue >= 130) && (rotationValue <= 220)) // Gas stove is on with medium fire
        {
            //Debug.Log("GasStove = on medium");
            foreach (GameObject fire in firePrefab)
            {
                fire.SetActive(true);
                var main = fire.GetComponent<ParticleSystem>().main;
                main.simulationSpeed = 2f;
                main.startColor = Color.yellow;
            }
            IsOnFire = 2;
        }
        else if ((rotationValue >= 220) && (rotationValue <= 300)) // Gas stove is on with high fire
        {
            //Debug.Log("GasStove = on high");
            foreach (GameObject fire in firePrefab)
            {
                fire.SetActive(true);
                var main = fire.GetComponent<ParticleSystem>().main;
                main.simulationSpeed = 3f;
                main.startColor = Color.red;
            }
            IsOnFire = 3;
        }
    }

}