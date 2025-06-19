using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manual : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    [SerializeField] private int pageIndex = 0;
    [SerializeField] private int pageSize = 3;
    [SerializeField] private Image nowImage;
    
    public void OnClickPageUp()
    {
        pageIndex  = (pageIndex + 1) % pageSize;
        nowImage.sprite = images[pageIndex];    
    }
}
