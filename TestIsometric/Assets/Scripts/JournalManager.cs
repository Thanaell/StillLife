using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : Singleton<JournalManager>
{
    string spriteNames = "journal-01-fix";
    Sprite[] sprites;
    public Image journalImage;
    // Use this for initialization
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        journalImage.sprite = sprites[0];
    }

    public void ChooseSprite()
    {
        Debug.Log(DayManager.Instance.currentDay.dayNumber);
        switch (DayManager.Instance.currentDay.dayNumber)
        {
            case 1:
                journalImage.sprite = sprites[0];
                break;
            case 2:
                journalImage.sprite = sprites[1];
                break;
            case 3:
                journalImage.sprite = sprites[2];
                break;
            case 4:
                journalImage.sprite = sprites[3];
                break;
            case 5:
                journalImage.sprite = sprites[4];
                break;
            case 6:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
