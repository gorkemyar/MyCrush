using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMoves : MonoBehaviour
{
    public Dictionary<int, int> moveNumber = new Dictionary<int, int>();
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    void SetUp(){
        for (int i = 1; i <= 10; i++){
            TextAsset levelText = Resources.Load<TextAsset>("Levels/RM_A" + i);
            string[] lines = levelText.text.Split('\n');
            int tmp = int.Parse(lines[3].Substring(12));
            moveNumber.Add(i, tmp);
        }
    }

    public int GetMoveNumber(int level){
        if (moveNumber.ContainsKey(level)){
            return moveNumber[level];
        } else {
            return 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
