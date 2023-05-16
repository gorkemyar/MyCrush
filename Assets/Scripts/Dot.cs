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
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;

    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if (Math.Abs(targetX - transform.position.x) > .1){
            // move towards to target
            tempPosition = new UnityEngine.Vector2(targetX, transform.position.y);
            transform.position = UnityEngine.Vector2.Lerp(transform.position, tempPosition, .4f);
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
        Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces(){
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width){
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height){
            otherDot = board.allDots[column, row+1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        } else if  (swipeAngle > 135 || swipeAngle <= -135 && column > 0){
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0){
            otherDot = board.allDots[column, row - 1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }


    }
}
