using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    private GameObject parent;

    private GameData gameData;

    private int level;

    private int highScore;

    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        gameData = parent.GetComponent<LevelButton>().gameData;
        level = parent.GetComponent<LevelButton>().level;
        SetScoreText();
    }

    void SetScoreText(){
        if (gameData != null && gameData.saveData.isActive[level - 1]){
            highScore = gameData.saveData.highScores[level - 1];
            scoreText.text = "Highest Score: " + highScore.ToString();
        }else{
            scoreText.text = "Locked Level";
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
