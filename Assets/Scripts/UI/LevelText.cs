using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelText : MonoBehaviour
{

    public TMP_Text levelText;
    private GameObject parent;
    private GameData gameData;

    private int level;

    private int move;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        gameData = parent.GetComponent<LevelButton>().gameData;
        level = parent.GetComponent<LevelButton>().level;
        SetLevelText();   
    }

    void SetLevelText(){
        if (gameData != null){
            move = gameData.saveData.moveNumber[level - 1];
            levelText.text =  "Level " + level.ToString() +  " - Moves " + move.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetLevelText();
    }
}
