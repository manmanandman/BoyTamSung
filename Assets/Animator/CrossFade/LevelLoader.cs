using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    [HideInInspector]
    public float duration_transition;

    private void Awake()
    {
        duration_transition = transition.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        //Debug.Log(duration_transition);
    }

    public void TriggerTransitionNextScene()
    {

        transition.SetTrigger("Start");
    }
}
