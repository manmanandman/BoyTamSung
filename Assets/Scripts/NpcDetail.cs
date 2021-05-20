using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDetail : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject NpcPrefab;
    [Header("Shirt Color")]
    public Material ShirtMaterial;
    [Header("Voice Over")]
    public AudioClip waitingSound;
    public AudioClip wavingSound;
    public AudioClip hitSound;
    public AudioClip wrongfoodSound;
    public AudioClip cashingSound;
    public AudioClip timeoutSound;
    public AudioClip correctfoodSound;
    public AudioClip wrongfoodkeepwaitSound;
}
