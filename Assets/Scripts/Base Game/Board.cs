using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    [Header("Board Variables")]
    public int width;
    public int height;
    public int moveCount;
    public int highScore = 0;

    [Header("Game Objects")]
    public GameObject tilePrefab;
    public GameObject[] dots;
    public GameObject destroyEffect;
    public GameObject[,] allDots;
    public GameObject gameCompletePanel;

    [Header("Prefabs")]    
    private BackgroundTile[,] allTiles;
    private FindMatches findMatches;
    private ScoreManager scoreManager;
    private SoundManager soundManager;

    [Header("Dictionary for Score and Color")]
    private Dictionary<string, int> scoreTable = new Dictionary<string, int>(){
        {"Indigo", 200},
        {"Green", 150},
        {"Red", 100},
        {"Yellow", 250},
    };
    private Dictionary<string, string> colorTable = new Dictionary<string, string>(){
        {"b", "Indigo"},
        {"g", "Green"},
        {"r", "Red"},
        {"y", "Yellow"},
    };
    private HashSet<int> rowSet = new HashSet<int>();

    
    void Start(){
        findMatches = FindObjectOfType<FindMatches>();
        scoreManager = FindObjectOfType<ScoreManager>();
        soundManager = FindObjectOfType<SoundManager>();
        SetUp();
    }

    private void SetUp(){
        if (PlayerPrefs.HasKey("currentLevel")){
            LoadLevel10();
        } else {
            RandomSetUp();
        }
    }
    private void RandomSetUp(){
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        for (int i = 0; i < width; i++){
            for (int j = 0; j < height; j++){
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                int dotToUse = Random.Range(0, dots.Length);
                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                allDots[i, j] = dot;
            }
        }
        findMatches.FindAllMatches();
    }

    private void LoadLevel10(){
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        width = PlayerPrefs.GetInt("currentWidth");
        height = PlayerPrefs.GetInt("currentHeight");
        moveCount = PlayerPrefs.GetInt("currentMoveNumber");
        highScore = PlayerPrefs.GetInt("currentHighScore");
        string currentCandyType = PlayerPrefs.GetString("currentCandyType");
        
        string[] dots = currentCandyType.Split(',');
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        
        for (int i = 0; i < height; i++){
            for (int j = 0; j < width; j++){
                Vector2 tempPosition = new Vector2(j, i);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                GameObject dotToUse = FindDot(colorTable[dots[i*width + j]]);
                GameObject dot = Instantiate(dotToUse, tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                allDots[j, i] = dot;
            }
        }

    }

    private void LoadLevel20(){
        
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        string path = "Levels/RM_A" + currentLevel.ToString();
        TextAsset levelData = Resources.Load<TextAsset>(path);
        string[] lines = levelData.text.Split('\n');
        width = int.Parse(lines[1].Substring(12));
        height = int.Parse(lines[2].Substring(13));
        moveCount = int.Parse(lines[3].Substring(12));

        string line = lines[4].Substring(6);
        string[] dots = line.Split(',');
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        
        for (int i = 0; i < height; i++){
            for (int j = 0; j < width; j++){
                Vector2 tempPosition = new Vector2(j, i);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                GameObject dotToUse = FindDot(colorTable[dots[i*width + j]]);
                GameObject dot = Instantiate(dotToUse, tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                allDots[j, i] = dot;
            }
        }
    }

    private GameObject FindDot(string tag){
        for (int i = 0; i < dots.Length; i++){
            if (dots[i].tag == tag){
                return dots[i];
            }
        }
        return null;
    }
    public void updateScore(int row, string tag){
        if (!rowSet.Contains(row)){
            soundManager.PlayDestroySound();
            scoreManager.IncreaseScore(scoreTable[tag]);
            rowSet.Add(row);
        }
    }

    public void MakeMove(){
        scoreManager.DecreaseMove();
        moveCount--;
        if (moveCount == 0){
            PlayerPrefs.SetInt("currentScore", scoreManager.GetScore());
            PlayerPrefs.Save();
            gameCompletePanel.SetActive(true);
        }
    }
}
