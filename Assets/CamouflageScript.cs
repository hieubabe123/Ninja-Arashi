using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamouflageScript : MonoBehaviour
{
    private CamouflageScriptableObject camouflageData;
    private int currentDiguiseDuration;

    private void Awake()
    {
        camouflageData = DataManager.instance.currentCamouflageData;
    }

    void Start()
    {
        currentDiguiseDuration = camouflageData.DiguiseDuration;
    }

    void OnEnable()
    {

    }

}
