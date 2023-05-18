using System.Diagnostics;
using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private int column;
    private int row;
    public int targetX;
    public int targetY;

    private GameObject otherDot;
    private Board board;
    private UnityEngine.Vector2 firstTouchPosition;
    private UnityEngine.Vector2 finalTouchPosition;
    private UnityEngine.Vector2 tempPosition;
    public float swipeAngle = 0;

    public bool isMatched = false;
    private bool isEffect = true;
    // Start is called before the first frame update

    private FindMatches findMatches;
    void Start()
    {
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMatched){
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            if (isEffect){
                GameObject particle = Instantiate(board.destroyEffect, transform.position, UnityEngine.Quaternion.identity);
                Destroy(particle, .5f);
                mySprite.color = new UnityEngine.Color(0f, 0f, 0f, .2f);
                board.updateScore(row, this.gameObject.tag);
                isEffect = false;
            }
        }


        targetX = column;
        targetY = row;
        if (Math.Abs(targetX - transform.position.x) > .1){
            // move towards to target
            tempPosition = new UnityEngine.Vector2(targetX, transform.position.y);
            transform.position = UnityEngine.Vector2.Lerp(transform.position, tempPosition, .4f);
            findMatches.FindAllMatches();
        }else{
            // direct set position
            tempPosition = new UnityEngine.Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }

        if (Math.Abs(targetY - transform.position.y) > .1){
            // move towards to target
            tempPosition = new UnityEngine.Vector2(transform.position.x, targetY);
            transform.position = UnityEngine.Vector2.Lerp(transform.position, tempPosition, .4f);
            findMatches.FindAllMatches();
        }else{  
            // direct set position
            tempPosition = new UnityEngine.Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column, row] = this.gameObject;
        }

    }
    private void OnMouseDown(){
        firstTouchPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition);
    }

    private void OnMouseUp(){
        finalTouchPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle(){
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        UnityEngine.Debug.Log(swipeAngle);
        if (!isMatched){
            MovePieces();
            board.MakeMove();
        }
        
    }

    void MovePieces(){
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width-1){
            otherDot = board.allDots[column + 1, row];
            if (!otherDot.GetComponent<Dot>().isMatched){
                otherDot.GetComponent<Dot>().column -= 1;
                column += 1;
            }
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height-1){
            otherDot = board.allDots[column, row+1];
            if (!otherDot.GetComponent<Dot>().isMatched){
                otherDot.GetComponent<Dot>().row -= 1;
                row += 1;
            }
        } else if  (swipeAngle > 135 || swipeAngle <= -135 && column > 0){
            otherDot = board.allDots[column - 1, row];
            if (!otherDot.GetComponent<Dot>().isMatched){
                otherDot.GetComponent<Dot>().column += 1;
                column -= 1;
            }
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0){
            otherDot = board.allDots[column, row - 1];
            if (!otherDot.GetComponent<Dot>().isMatched){
                otherDot.GetComponent<Dot>().row += 1;
                row -= 1;
            }
        }
    }

    // void FindMatches(){

    //     bool flag = true;
    //     for (int i = 0; i < board.width; i++){
    //         if (board.allDots[i, row].tag != this.gameObject.tag){
    //             flag = false;
    //             break;
    //         }
    //     }

    //     isMatched = flag;

    // }
}
