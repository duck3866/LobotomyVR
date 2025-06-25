using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Animator doorAnimator_L;
    public Animator doorAnimator_R;

    public float needEnergy;
    public float nowEnergy;
    [SerializeField] private TextMeshProUGUI liveHumanText;
    [SerializeField] private List<GameObject> humanObjects;
    [SerializeField] private List<Siren> sirens;
    [SerializeField] private List<MonsterRoom> monsterRooms = new List<MonsterRoom>();

    [SerializeField] private GameObject report;
    [SerializeField] private TextMeshProUGUI reportRank;
    private bool jailBreak = false;
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
        jailBreak = false;
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
        if (nowEnergy >= needEnergy)
        {
            ClearGame();
        }
    }
    /// <summary>
    /// 직원 죽었을때 발동하는 함수
    /// </summary>
    /// <param name="human"></param>
    public void DeleteHumans(GameObject human)
    {
        humanObjects.Remove(human);
        liveHumanText.text = "People:"+humanObjects.Count;
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
        if (!jailBreak)
        {
            foreach (var siren in sirens)
            {
                siren.StartSiren();
            }
            doorAnimator_L.SetTrigger("OpenDoor");
            doorAnimator_R.SetTrigger("OpenDoor");
            audioManager.PlayBackgroundMusic(sirenClip,BGMVolume);
        }
        jailBreak = true;
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
            
            doorAnimator_L.SetTrigger("CloseDoor");
            doorAnimator_R.SetTrigger("CloseDoor");
            jailBreak = false;
        }
    }

    public void MonsterEnter(GameObject monster)
    {
        foreach (var human in humanObjects)
        {
            human.GetComponent<Human>().FindMonster(monster);
        }
    }
    public void MonsterDie(GameObject monster)
    {
        foreach (var human in humanObjects)
        {
            human.GetComponent<Human>().ResetTarget(monster);
        }
    }

    /// <summary>
    /// 에너지를 전부 충족하여 게임이 클리어 될때 호출하는 함수
    /// </summary>
    public void ClearGame()
    {
        report.SetActive(true);
        switch (humanObjects.Count)
        {
            case 3:
                reportRank.text = "A";
                break;
            case 2:
                reportRank.text = "B";
                break;
            default:
                reportRank.text = "C";
                break;
        }
    }
}
