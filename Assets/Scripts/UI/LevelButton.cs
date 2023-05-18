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
    public TMP_Text levelText;
    public TMP_Text scoreText;
    public string levelToLoad;
    public Image myImage;

    public Button myButton;
    public GameData gameData;
    
    private int move;
    private int highScore;
    // Start is called before the first frame update
    void Start()
    {   
        gameData = FindObjectOfType<GameData>();
        SetAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAll(){
        SetLevelText();
        SetScoreText();
        ButtonActivate();
    }

    void SetLevelText(){
        if (gameData != null && level != 0 && levelText != null){
            move = gameData.saveData.moveNumber[level - 1];
            levelText.text =  "Level " + level.ToString() +  " - Moves " + move.ToString();
        }
    }

    void SetScoreText(){
        //UnityEngine.Debug.Log("ScoreText: " + level.ToString() + " " + gameData.saveData.isActive[level - 1].ToString() + " " + gameData.saveData.highScores[level - 1].ToString() );
        if (gameData != null && level != 0 && gameData.saveData.isActive[level - 1] && scoreText != null){
            highScore = gameData.saveData.highScores[level - 1];
            scoreText.text = "Highest Score: " + highScore.ToString();
        }else{
            scoreText.text = "Locked Level";
        }   
    }

    void ButtonActivate(){
        if (gameData != null && level != 0 && gameData.saveData.isActive[level - 1] && myButton != null && myImage != null){
            myButton.interactable = true;
            myImage.sprite = activeSprite;
        } else {
            myButton.interactable = false;
            myImage.sprite = lockedSprite;
        }
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
