using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayLevelButton : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject parent;
    private Button myButton;
    private Image myImage;
    private GameData gameData;

    private int level;

    void Start()
    {
        parent = transform.parent.gameObject;
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
        gameData = parent.GetComponent<LevelButton>().gameData;
        level = parent.GetComponent<LevelButton>().level;
        ButtonActivate();
    }

    void ButtonActivate(){
        if (gameData.saveData.isActive[level - 1]){
            myButton.interactable = true;
            myImage.sprite = parent.GetComponent<LevelButton>().activeSprite;
        } else {
            myButton.interactable = false;
            myImage.sprite = parent.GetComponent<LevelButton>().lockedSprite;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
