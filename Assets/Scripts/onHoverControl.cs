using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onHoverControl : MonoBehaviour
{
    public Material hoveringMaterial;
    private Material[] mats;
    private Material[] tempmats;
    // Start is called before the first frame update
    void Start()
    {
        tempmats = this.GetComponent<MeshRenderer>().materials;
    }

    public void changeToHover()
    {
        mats = this.GetComponent<MeshRenderer>().materials;
        for(int i = 0; i<mats.Length;i++)
        {
            mats[i] = hoveringMaterial;
        }
        this.GetComponent<MeshRenderer>().materials = mats;
    }

    public void changeToNormal()
    {
        this.GetComponent<MeshRenderer>().materials = tempmats;
    }
}
