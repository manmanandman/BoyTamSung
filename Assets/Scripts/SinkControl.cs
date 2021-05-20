using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkControl : MonoBehaviour
{
    public ParticleSystem particle;

    public List<Collider> colliders = new List<Collider>();

    public MeshRenderer box;

    public Material red;
    public Material green;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colliders.Count == 0)
        {
            box.material = red;
            if (particle.isPlaying) particle.Stop();
        }
        else
        {
            box.material = green;
            if (!particle.isPlaying) particle.Play();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) { colliders.Add(other); }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other)) { colliders.Remove(other); }
    }

}
