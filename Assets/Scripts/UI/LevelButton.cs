using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelButton : MonoBehaviour
{

    [Header("Inputs")]
    public int level;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    public string levelToLoad;

    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {   
        gameData = FindObjectOfType<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play(){
        if (gameData != null && level != 0){
            PlayerPrefs.SetInt("currentLevel", level);
            PlayerPrefs.SetInt("currentWidth", gameData.saveData.width[level - 1]);
            PlayerPrefs.SetInt("currentHeight", gameData.saveData.height[level - 1]);
            PlayerPrefs.SetInt("currentMoveNumber", gameData.saveData.moveNumber[level - 1]);
            PlayerPrefs.SetString("currentCandyType", gameData.saveData.candyType[level - 1]);
            PlayerPrefs.SetInt("currentHighScore", gameData.saveData.highScores[level - 1]);
        }
        SceneManager.LoadScene(levelToLoad);
    }
}
