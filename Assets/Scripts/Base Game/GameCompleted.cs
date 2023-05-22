using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameCompleted : MonoBehaviour
{
    public GameObject gameCompletedPanel;
    
    private GameData gameData;
    private int score;
    private int currentHighScore;

    private int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        if (gameData != null){
            gameData.Load();
        }
    }

    public void GameFinished(){
        gameCompletedPanel.SetActive(false);

        score = PersistentMemory.Instance.currentScore;
        currentLevel = PersistentMemory.Instance.currentLevel;
        currentHighScore = gameData.saveData.highScores[currentLevel-1];
        if (score > currentHighScore){
            gameData.saveData.highScores[currentLevel-1] = score;
            gameData.saveData.isActive[currentLevel-1] = true;
            gameData.saveData.isActive[currentLevel] = true;
            gameData.Save();
            
            SceneManager.LoadScene("Congratulations");
        }else{
            gameData.saveData.isActive[currentLevel-1] = true;
            gameData.saveData.isActive[currentLevel] = true;
            gameData.Save();
            SceneManager.LoadScene("MainScreen");    
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
