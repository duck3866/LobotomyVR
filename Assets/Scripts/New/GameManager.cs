using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;
    public AudioClip bgmClip;
    public AudioClip sirenClip;
    [Range(0,100)] public float BGMVolume;
    public Slider energySlider;
    
    public float needEnergy;
    public float nowEnergy;
    
    [SerializeField] private List<Siren> sirens;
    [SerializeField] private List<MonsterRoom> monsterRooms = new List<MonsterRoom>();
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

    public void Start()
    {
        audioManager.PlayBackgroundMusic(bgmClip,1f);
    }

    /// <summary>
    /// 작업 에너지 추가 
    /// </summary>
    /// <param name="energy"></param>
    public void AddEnergy(int energy)
    {
        nowEnergy += energy;
        energySlider.value = nowEnergy / needEnergy;
    }
    /// <summary>
    /// 사이렌 키는 함수 환상체 탈출할때 호출
    /// </summary>
    /// <param name="monsterRoom">탈출한 환상체</param>
    public void JailBreak(MonsterRoom monsterRoom)
    {
        if (!monsterRooms.Contains(monsterRoom))
        {
            monsterRooms.Add(monsterRoom);
        }
        foreach (var siren in sirens)
        {
            siren.StartSiren();
        }
        audioManager.PlayBackgroundMusic(sirenClip,BGMVolume);
    }
    /// <summary>
    /// 사이렌 끄는 함수 환상체가 전부 제압될때 꺼짐
    /// </summary>
    /// <param name="monsterRoom">제압된 환상체</param>
    public void ClearJailBreak(MonsterRoom monsterRoom)
    {
        if (monsterRooms.Contains(monsterRoom))
        {
            monsterRooms.Remove(monsterRoom);
        }
        bool isSafe = monsterRooms.Count == 0;
        if (isSafe)
        {
            foreach (var siren in sirens)
            {
                siren.EndSiren();
            }   
            audioManager.PlayBackgroundMusic(bgmClip,BGMVolume);
        }
    }
    /// <summary>
    /// 에너지를 전부 충족하여 게임이 클리어 될때 호출하는 함수
    /// </summary>
    public void ClearGame()
    {
        
    }
}
