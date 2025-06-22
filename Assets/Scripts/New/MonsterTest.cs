using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTest : MonsterRoom
{
    [SerializeField] private float count = 0;
 
    [SerializeField] private float needItemCount = 3f;
    [SerializeField] private float nowItemCount = 0f; 
    
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private Image Image;

    [SerializeField] private int valueTest;
    [SerializeField] private bool jailBreak = false; 
    public override void PlayerInRoom()
    {
        isInRoom = true;
        workType = WorkType.Instinct;
        // manuals[(int)workType].SetActive(true);
        // manuals[(int)workType].transform.position = manualSpawnPoint.transform.position;
        count = 0f;
    }
    public override void Update()
    {
        if (count > 3)
        {
            if (!jailBreak)
            {
                JailBreak();
            }
        }
    }
    private void OnBecameInvisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameInvisible 호출됨");
            count += 1;
        }
    }

    private void OnBecameVisible()
    {
        if (isInRoom)
        {
            Debug.Log("OnBecameVisible 호출됨");
        }
    }

    public void ClickButtonManual(int value)
    {
        if (value == valueTest)
        {
            Result(WorkResult.Good);
        }
        else
        {
            Result(WorkResult.Bad);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("Item enter");
            nowItemCount += 1;
            if (nowItemCount >= needItemCount)
            {
                Result(WorkResult.Good);
            }
        }
    }
    public override void Result(WorkResult result)
    {
        masterRoom.Clear();
        if (result == WorkResult.Good)
        {
            Image.sprite = images[0];
            GameManager.Instance.AddEnergy(10);
        }
        else if (result == WorkResult.SoSo)
        {
            Image.sprite = images[1];
            GameManager.Instance.AddEnergy(5);
        }
        else
        {
            Image.sprite = images[2];
            GameManager.Instance.AddEnergy(1);
        }
    }

    public override void JailBreak()
    {
        jailBreak = true;
        GetComponent<MeshRenderer>().enabled = false;
        masterRoom.doorAnimator.SetTrigger("DoorToggle");
        GameManager.Instance.JailBreak();
    }
}
