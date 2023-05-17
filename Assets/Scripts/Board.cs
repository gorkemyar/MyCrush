using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] dots;
    public GameObject destroyEffect;
    public GameObject[,] allDots;

    private BackgroundTile[,] allTiles;    
    private FindMatches findMatches;

    private ScoreManager scoreManager;
    private Dictionary<string, int> scoreTable = new Dictionary<string, int>(){
        {"Indigo", 200},
        {"Green", 150},
        {"Red", 100},
        {"Yellow", 250},
    };

    private HashSet<int> rowSet = new HashSet<int>();

    void Start(){
        allTiles = new BackgroundTile[width, height];
        allDots = new GameObject[width, height];
        findMatches = FindObjectOfType<FindMatches>();
        scoreManager = FindObjectOfType<ScoreManager>();
        SetUp();
    }

    private void SetUp(){
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


    public void updateScore(int row, string tag){
        if (!rowSet.Contains(row)){
            scoreManager.IncreaseScore(scoreTable[tag]);
            rowSet.Add(row);
        }
    }
}
