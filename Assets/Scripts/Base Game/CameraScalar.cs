using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour
{

    private Board board;
    private float aspectRatio = 0.625f;
    private float padding = 2.0f;
    private float yOffset = 1.0f;
    private float cameraOffset = -10;

    private int width;
    private int height;

    void Start()
    {
        board = FindObjectOfType<Board>();
        width = PersistentMemory.Instance.currentWidth;
        height = PersistentMemory.Instance.currentHeight;
        if (board != null){
            RepositionCamera(width - 1, height - 1);
        }
    }
    void RepositionCamera(float x,  float y){
        UnityEngine.Vector3 temp = new UnityEngine.Vector3(x/2,y/2 + yOffset, cameraOffset);
        transform.position = temp; 
        if (width >= height){
            Camera.main.orthographicSize = (width / 2 + padding) / aspectRatio;
        }else{
            Camera.main.orthographicSize = height / 2 + padding;
        }
    }
}
