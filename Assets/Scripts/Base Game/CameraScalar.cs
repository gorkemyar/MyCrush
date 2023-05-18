using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour
{
    // Start is called before the first frame update

    private Board board;
    private float aspectRatio = 0.625f;
    private float padding = 2;
    private float yOffset = 1;
    private float cameraOffset = -10;
    void Start()
    {
        board = FindObjectOfType<Board>();
        if (board != null){
            RepositionCamera(board.width - 1, board.height - 1);
        }
    }

    void RepositionCamera(float x,  float y){
        UnityEngine.Vector3 temp = new UnityEngine.Vector3(x/2,y/2 + yOffset, cameraOffset);
        transform.position = temp; 
        if (board.width >= board.height){
            Camera.main.orthographicSize = (board.width / 2 + padding) / aspectRatio;
        }else{
            Camera.main.orthographicSize = board.height / 2 + padding;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
