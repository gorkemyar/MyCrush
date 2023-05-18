using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Congrats : MonoBehaviour
{

    public TMP_Text scoreText;
    private int score =0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("currentScore")){
            score = PlayerPrefs.GetInt("currentScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
      scoreText.text = "Highest Score\n" + score.ToString();
    }
}
