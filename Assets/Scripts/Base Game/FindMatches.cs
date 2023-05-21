using System.Diagnostics;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;
using Utils;

public struct Pair{
    public int first;
    public int second;
    public Pair(int first, int second){
        this.first = first;
        this.second = second;
    }
} 


public struct Triple{
    public int first;
    public int second;
    public int third;
    public Triple(int first, int second, int third){
        this.first = first;
        this.second = second;
        this.third = third;
    }
}


public class FindMatches : MonoBehaviour
{   
    private Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }


    public void FindAllMatches(){
        StartCoroutine(FindAllMatchesCo());
    }
    public void GameOver(){
        StartCoroutine(IsGameOver());
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

    // Comlexity: O(width^2 * height^2)
    private IEnumerator IsGameOver(){
        yield return new WaitForSeconds(.2f);

        // row set that contains all the rows that have been matched
        HashSet<int> rowSet = board.rowSet;
    
        // remaining move count
        int moveCount = board.moveCount;

        // starting row
        int pre = 0;

        bool IsGameOver = true;
        for (int cur = 0; cur < board.height; cur++){
            // find the first row that has been matched
            if (!rowSet.Contains(cur) && cur != board.height - 1){
                continue;
            }
            if (cur == board.height - 1 && !rowSet.Contains(cur)){
                cur++;
            }   
            // between pre and cur, there are no matched rows

            // create a dictionary to store the color and its positions between pre and cur
            Dictionary<string, List<Pair>> dict = new Dictionary<string, List<Pair>>();
            for (int i = pre; i < cur; i++){
                for (int j = 0; j < board.width; j++){
                    string name = board.allDots[j, i].tag;
                    Pair elem = new Pair(j, i);
                    if (dict.ContainsKey(name)){
                        dict[name].Add(elem);
                    }else{
                        dict[name] = new List<Pair>();
                        dict[name].Add(elem);
                    }
                }
            }

            // check if there is a possible row match in the future for any color
            foreach (string name in dict.Keys){ // iterate through all the colors
                if (dict[name].Count >= board.width){ 
                    // if there is a color that occurs more than the width of the board, 
                    // then there might be a row match
    
                    // for that color insert all the positions and their distance to any row between pre and cur to a priority queue
                    List<Pair> same_color_elems = dict[name];
                    for (int i = pre; i < cur; i++){ // any row between pre and cur
                        PriorityQueue<Triple, int> pq = new PriorityQueue<Triple, int>();
                        for (int j = 0; j < board.width; j++){
                            foreach(Pair elem in same_color_elems){
                                int dist = Math.Abs(elem.first - j) + Math.Abs(elem.second - i);
                                pq.Enqueue(new Triple(elem.first, elem.second, j), dist);
                            }
                        }

                        // HashSet to store the visited positions and a list to store the minimum distance to each column
                        HashSet<string> visited = new HashSet<string>();
                        List<int> dx = new List<int>();
                        for (int j = 0; j < board.width; j++){
                            dx.Add(-1);
                        }

                        while (pq.Count != 0){ // using priority queue find all minimum row matches
                            Triple elem = pq.Dequeue();
                            if (visited.Contains(elem.first + " " + elem.second) || dx[elem.third] != -1){
                                continue;
                            }
                            visited.Add(elem.first + " " + elem.second);
                            int distance = Math.Abs(elem.first - elem.third) + Math.Abs(elem.second - i);
                            dx[elem.third] = distance;
                        }

                        int sum = dx.AsEnumerable().Sum();  // calculate the sum of all the minimum row matches
                        if (sum <= moveCount){ // if the sum is less than the remaining move count, then there is a possible row match
                            IsGameOver = false;
                        }
                    }
                }
            }

            pre = cur + 1;
            
        }
        //UnityEngine.Debug.Log("IsGameOver is " + IsGameOver);

        if (IsGameOver){
            board.isEndOfGame = true;
        }
    }
}


/*




i,j  



*/

