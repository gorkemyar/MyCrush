using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelText : MonoBehaviour
{

    public TMP_Text levelText;
    private GameObject level;

    private GameObject levelGrid;
    // Start is called before the first frame update
    void Start()
    {
        level = transform.parent.gameObject;
        levelGrid = level.transform.parent.gameObject;
        SetLevelText();   
    }

    void SetLevelText(){
        int levelNum = level.GetComponent<LevelButton>().level;
        levelText.text = "Level " + levelNum.ToString() +  " - Moves " + level.GetComponent<LevelButton>().moves.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        SetLevelText();
    }
}
