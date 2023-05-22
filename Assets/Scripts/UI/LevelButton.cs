using System.Threading.Tasks;
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

    private loadFile loadFiles;
    public Button myButton;
    public GameData gameData;
    
    private int move;
    private int highScore;

    // Start is called before the first frame update
    void Start()
    {   
        gameData = FindObjectOfType<GameData>();
        loadFiles = FindObjectOfType<loadFile>();
        SetAll();
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
        if (level > 10 && Application.internetReachability == NetworkReachability.NotReachable){
            levelText.text = "No Internet Connection";
        }
    }

    void SetScoreText(){
        if (gameData != null && level != 0 && gameData.saveData.isActive[level - 1] && scoreText != null){
            if (level <= 10 || Application.internetReachability != NetworkReachability.NotReachable){
                highScore = gameData.saveData.highScores[level - 1];
                scoreText.text = "Highest Score: " + highScore.ToString();
            }else{
                scoreText.text = "Locked Level";
            }
        }else{
            scoreText.text = "Locked Level";
        }   
    }

    void ButtonActivate(){
        if (gameData != null && level != 0 && gameData.saveData.isActive[level - 1] && myButton != null && myImage != null){
            if (level <= 10 || Application.internetReachability != NetworkReachability.NotReachable){
                myButton.interactable = true;
                myImage.sprite = activeSprite;
            }else{
                myButton.interactable = false;
                myImage.sprite = lockedSprite;
            }
        } else {
            myButton.interactable = false;
            myImage.sprite = lockedSprite;
        }
    }

    public async void Play(){
        if (gameData != null && level != 0 && level <= 10 && level != 2){
            PersistentMemory.Instance.currentLevel = level;
            PersistentMemory.Instance.currentWidth = gameData.saveData.width[level - 1];
            PersistentMemory.Instance.currentHeight = gameData.saveData.height[level - 1];
            PersistentMemory.Instance.currentMoveNumber = gameData.saveData.moveNumber[level - 1];
            PersistentMemory.Instance.currentHighScore = gameData.saveData.highScores[level - 1];
            PersistentMemory.Instance.currentCandyType = gameData.saveData.candyType[level - 1];
            PersistentMemory.Instance.currentScore = 0;

            SceneManager.LoadScene(levelToLoad);
        }else {
            if (Application.internetReachability != NetworkReachability.NotReachable){
                byte[] fileContents = await loadFiles.LoadFile(level);
                await Task.Run(() => SetParameters(fileContents));                
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private async void SetParameters(byte[] fileContents){
        string level_str = "";
        string width_str = "";
        string height_str = "";
        string move_str = "";
        string grid_str = "";
        
        int i = GetNextCharacter(fileContents, 0, ':') + 1;
        int j = GetNextCharacter(fileContents, i, '\n');

        level_str = System.Text.Encoding.UTF8.GetString(fileContents, i, j - i);

        i = GetNextCharacter(fileContents, j, ':') + 1;
        j = GetNextCharacter(fileContents, i, '\n');

        UnityEngine.Debug.Log("Test1");
        width_str = System.Text.Encoding.UTF8.GetString(fileContents, i, j - i);

        i = GetNextCharacter(fileContents, j, ':') + 1;
        j = GetNextCharacter(fileContents, i, '\n');

        height_str = System.Text.Encoding.UTF8.GetString(fileContents, i, j - i);

        i = GetNextCharacter(fileContents, j, ':') + 1;
        j = GetNextCharacter(fileContents, i, '\n');

        move_str = System.Text.Encoding.UTF8.GetString(fileContents, i, j - i);

        i = GetNextCharacter(fileContents, j, ':') + 1;
        j = fileContents.Length;

        grid_str = System.Text.Encoding.UTF8.GetString(fileContents, i, j - i);
        grid_str = grid_str.Replace(" ", "");

        int level = int.Parse(level_str);
        int width = int.Parse(width_str);
        int height = int.Parse(height_str);
        int moveCount = int.Parse(move_str);
        UnityEngine.Debug.Log("moveCount: " + moveCount);
        int highScore = gameData.saveData.highScores[level - 1];


        // Set the current level
        PersistentMemory.Instance.currentLevel = level;
        PersistentMemory.Instance.currentWidth = width;
        PersistentMemory.Instance.currentHeight = height;
        PersistentMemory.Instance.currentMoveNumber = moveCount;
        PersistentMemory.Instance.currentHighScore = highScore;
        PersistentMemory.Instance.currentCandyType = grid_str;
        PersistentMemory.Instance.currentScore = 0;

        // Set the level data
        gameData.saveData.width[level-1] = width;
        gameData.saveData.height[level-1] = height;
        gameData.saveData.moveNumber[level-1] = moveCount;
    }
     private int GetNextCharacter(byte[] fileContents, int start, char character){
        int i = start;
        while ((char)fileContents[i] != character){
            i++;
        }
        return i;
    }
}
