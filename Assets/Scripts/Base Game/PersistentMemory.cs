using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentMemory : MonoBehaviour
{
    public static PersistentMemory Instance {get; set; }

    public int currentLevel = 0;

    public int currentHeight = 0;

    public int currentWidth = 0;

    public int currentMoveNumber = 0;

    public int currentHighScore = 0;

    public int currentScore = 0;

    public string currentCandyType = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
