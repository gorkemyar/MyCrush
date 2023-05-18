using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public TMP_Text moveText;

    private int score;
    private int currentHighScore;
    private int currentMoveNumber;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("currentLevel")){
            currentHighScore = PlayerPrefs.GetInt("currentHighScore");
            currentMoveNumber = PlayerPrefs.GetInt("currentMoveNumber");
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        highScoreText.text = currentHighScore.ToString();
        moveText.text = currentMoveNumber.ToString();
    }
    public void IncreaseScore(int amountToIncrease){
        score += amountToIncrease;
        UpdateHighScore();
    }

    public void DecreaseMove(){
        currentMoveNumber--;
    }

    public void UpdateHighScore(){
        if (score > currentHighScore){
            currentHighScore = score;
        }
    }

}
