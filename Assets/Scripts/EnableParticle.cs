using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticle : MonoBehaviour
{
    public ParticleSystem particle;
    private float tempAngle = 0f;
    public bool useY = false;
    public bool useXZ = false;
    public float timeLeft = 2f;
    public bool useTime = false;
    public float timeLeftCurrent;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftCurrent = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if(useY)
        {
            float angle = gameObject.transform.rotation.eulerAngles.y;
            if (tempAngle != angle)
            {
                //Debug.Log(angle);
                tempAngle = angle;
            }
            if (angle > 230)
            {
                //Debug.Log("Play");
                PlayPar();
            }
            else
            {
                //Debug.Log("Stop");
                StopPar();
            }

        }
        if(useXZ)
        {
            float angleX = System.Math.Abs(gameObject.transform.rotation.x);
            float angleZ = System.Math.Abs(gameObject.transform.rotation.z);
            if (tempAngle != angleZ)
            {
                //Debug.Log(angleX);
                //Debug.Log(angleZ);
                tempAngle = angleZ;
            }
            if (angleX > 0.5 || angleZ > 0.5)
            {
                //Debug.Log("Play");
                PlayPar();
            }
            else
            {
                //Debug.Log("Stop");
                StopPar();
            }

        }
        if(particle.isPlaying && useTime)
        {
            timeLeftCurrent -= 1 * Time.deltaTime;
            if(timeLeftCurrent <= 0)
            {
                StopPar();
                //particle.gameObject.SetActive(false);
            }
        }

    }

    public void PlayPar()
    {
        if (!particle.isPlaying) particle.Play();
    }

    public void StopPar()
    {
        if (particle.isPlaying) particle.Stop();
    }
}
