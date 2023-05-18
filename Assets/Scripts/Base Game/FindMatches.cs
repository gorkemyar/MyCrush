using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{   
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }


    public void FindAllMatches(){
        StartCoroutine(FindAllMatchesCo());
    }
    private IEnumerator FindAllMatchesCo(){
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.height; i++){
            bool isRowMatch = true;
            GameObject firstDot = board.allDots[0, i];
            for (int j = 1; j < board.width; j++){
                GameObject currentDot = board.allDots[j, i];
                if (currentDot.tag != firstDot.tag){
                    isRowMatch = false;
                    break;
                }                
            }
            if (isRowMatch){
                for (int j = 0; j < board.width; j++){
                    GameObject currentDot = board.allDots[j, i];
                    currentDot.GetComponent<Dot>().isMatched = true;
                }
            }
        }
    }
}
