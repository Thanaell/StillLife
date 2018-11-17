using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : Singleton<PlantManager> {
    string spriteNames = "plante-01";
    Sprite[] sprites;
    
    private SpriteRenderer myPlantSprite;
    // Use this for initialization
    void Start() {
        sprites = Resources.LoadAll<Sprite>(spriteNames);
        myPlantSprite = GetComponent<SpriteRenderer>();
        myPlantSprite.sprite = sprites[0];

    }

    public void ChooseSprite() { 
        Debug.Log(DayManager.Instance.currentDay.dayNumber);
		switch (DayManager.Instance.currentDay.dayNumber)
        {
            case 1:
                myPlantSprite.sprite = sprites[0];
                break;
            case 2:
                Debug.Log("plante 2");
                myPlantSprite.sprite = sprites[1];
                break;
            case 3:
                myPlantSprite.sprite = sprites[2];
                break;
            case 4:
                myPlantSprite.sprite = sprites[3];
                break;
            case 5:
                myPlantSprite.sprite = sprites[4];
                break;
            case 6:
                myPlantSprite.sprite = sprites[4];
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
