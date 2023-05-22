using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Storage;
using Firebase.Extensions;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

public class loadFile : MonoBehaviour
{

    private static string base_url = "gs://rowmatch-2e3c6.appspot.com";
    private string file_url = "RM_A";
    private static FirebaseStorage storage;
    private static StorageReference storageRef;
    private StorageReference levelsRef;
    private StorageReference levelRef;
    private GameData gameData;
    // Start is called before the first frame update

    private const long maxAllowedSize = 1 * 1024 * 1024;

    private byte[] fileContents;

    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.GetReferenceFromUrl(base_url);
        levelsRef = storageRef.Child("levels");
    }

    public async Task<byte[]> LoadFile(int level){
        UnityEngine.Debug.Log("Ever here?");
         if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            return null;
        }else{
            
            levelRef = levelsRef.Child(file_url + level.ToString() + ".txt");
            UnityEngine.Debug.Log(file_url + level.ToString() + ".txt");
            
            await levelRef.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
                if (task.IsFaulted || task.IsCanceled) {
                    Debug.LogException(task.Exception);
                }
                else {
                    fileContents = task.Result;
                }
            });
        
            return fileContents;
        }
    }   

   

    void Update()
    {
        
    }
}
