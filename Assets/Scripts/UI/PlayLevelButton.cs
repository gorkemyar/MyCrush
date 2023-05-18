using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayLevelButton : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject level;
    private Button myButton;
    private Image myImage;

    void Start()
    {
        level = transform.parent.gameObject;
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();

        ButtonActivate();
    }

    void ButtonActivate(){
        if (level.GetComponent<LevelButton>().isActive){
            myButton.interactable = true;
            myImage.sprite = level.GetComponent<LevelButton>().activeSprite;
        } else {
            myButton.interactable = false;
            myImage.sprite = level.GetComponent<LevelButton>().lockedSprite;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
