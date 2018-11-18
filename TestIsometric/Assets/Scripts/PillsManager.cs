using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PillsManager : Singleton<PillsManager>
{
    string spriteNames = "pills-1";
    Sprite[] sprites;
    public Image pillsImage;
    // Use this for initialization
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        pillsImage.sprite = sprites[3];
    }

    public void ChooseSprite()
    {
        Debug.Log(DayManager.Instance.currentDay.dayNumber);
        switch (DayManager.Instance.currentDay.dayNumber)
        {
            case 1:
                pillsImage.sprite = sprites[3];
                break;
            case 2:
                pillsImage.sprite = sprites[2];
                break;
            case 3:
                pillsImage.sprite = sprites[1];
                break;
            case 4:
                pillsImage.sprite = sprites[0];
                break;
            case 5:
                pillsImage.sprite = sprites[0];
                break;
            case 6:
                pillsImage.sprite = sprites[0];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
