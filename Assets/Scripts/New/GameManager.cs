using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Slider energySlider;
    [SerializeField] private List<Siren> sirens;
    public float needEnergy;
    public float nowEnergy;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // public void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         JailBreak();
    //     }
    // }

    public void AddEnergy(int energy)
    {
        needEnergy += energy;
        energySlider.value = nowEnergy / needEnergy;
    }

    public void JailBreak()
    {
        foreach (var siren in sirens)
        {
            siren.StartSiren();
        }
    }
    public void ClearJailBreak()
    {
        foreach (var siren in sirens)
        {
            siren.EndSiren();
        }
    }
    public void ClearGame()
    {
        
    }
}
