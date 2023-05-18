using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelButton : MonoBehaviour
{
    public int level;
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    public string levelToLoad;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        PlayerPrefs.SetInt("currentLevel", level);
        SceneManager.LoadScene(levelToLoad);
    }
}
