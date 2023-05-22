using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class  SaveData{
    public bool[] isActive;
    public int[] highScores;
    public int[] width;
    public int[] height;
    public int[] moveNumber;
    public string[] candyType;

}
public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;
    // Start is called before the first frame update
    void Awake()
    {
        if (gameData == null){
            DontDestroyOnLoad(gameObject);
            gameData = this;
        } else if (gameData != this){
            Destroy(this.gameObject);
        } 
    }

    public void Save(){
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Create);
        SaveData data = new SaveData();
        data = saveData;
        formatter.Serialize(file, data);
        file.Close();
    }

    public void Start(){
        Load();
    }
    public void Load(){
        if (File.Exists(Application.persistentDataPath + "/gameData.dat")){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            saveData = (SaveData)formatter.Deserialize(file);
            
            file.Close();
        }
    }
    private void OnApplicationQuit(){
        //Save();
    }
    private void OnDisable(){
        //Save();
    }

    private void OnEnable(){
        //Save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
