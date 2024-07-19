using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System;

public class RollingScript : MonoBehaviour
{
    
    Vector3 vector1 = new Vector3(195.0f, 386.0f, 0f);
    Vector3 vector2 = new Vector3(-17.0f, 105.0f, 0f);
    Vector3 vector3 = new Vector3(-185f, 394f, 0f);

    
    [SerializeField]
    private GameObject leftBall, midBall, rightBall;

    private void Start()
    {
        

        rightBall.transform.DOLocalMoveX(vector1.x, 2f);
        rightBall.transform.DOLocalRotate(new Vector3(0, 0, 345), 2f, RotateMode.FastBeyond360);



        midBall.transform.DOLocalMoveY(vector2.y, 0.5f);

        leftBall.transform.DOLocalMoveX(vector3.x, 2f);
        leftBall.transform.DOLocalRotate(new Vector3(0, 0, -345), 2f, RotateMode.FastBeyond360);


    }

    

    
    
}
