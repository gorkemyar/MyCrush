using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    private GameObject level;
    public TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        level = transform.parent.gameObject;
        SetScoreText();
    }

    void SetScoreText(){
        if (level.GetComponent<LevelButton>().isActive){
            scoreText.text = "Highest Score: " + level.GetComponent<LevelButton>().highScores.ToString(); 
        } else {
            scoreText.text = "Locked Level";
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
