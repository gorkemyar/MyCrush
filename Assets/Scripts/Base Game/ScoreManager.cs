using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [Header("UI Texts")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text moveText;
    public TMP_Text finalScoreText;

    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = PersistentMemory.Instance.currentScore.ToString();
        highScoreText.text = PersistentMemory.Instance.currentHighScore.ToString();
        moveText.text = PersistentMemory.Instance.currentMoveNumber.ToString();
        finalScoreText.text = "Level Completed\nScore: " + PersistentMemory.Instance.currentScore.ToString();
    }
    public void IncreaseScore(int amountToIncrease){
        PersistentMemory.Instance.currentScore += amountToIncrease;
        UpdateHighScore();
    }
    public void UpdateHighScore(){
        if (PersistentMemory.Instance.currentScore > PersistentMemory.Instance.currentHighScore){
            PersistentMemory.Instance.currentHighScore = PersistentMemory.Instance.currentScore;
        }
    }

}
