﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float topBounds = 9.4f;

    public float bottomBounds = -9.4f;
    
    public float moveSpeed = 12.0f;

    public Vector2 ballDitrction = Vector2.left;

    private float playerPaddleHeight, playerPaddlewidth, computerPaddleHeight, computerPaddleWidth, playerPaddleMaxX, playerPaddleMaxY, playerPaddleMinX, playerPaddleMinY, computerPaddleMaxX, computerPaddleMaxY, computerPaddleMinX, computerPaddleMinY, ballWidth, ballHeight;

    private GameObject paddlePlayer, paddleComputer;

    private float bounceAngele;

    private float vx, vy;

    private float maxAngle = 45.0f;

    private bool collidedWithPlayer, collidedWithComputer, collidedWithWall;



    // Start is called before the first frame update
    void Start()
    {
        if (moveSpeed < 0)
            moveSpeed = -1 * moveSpeed;

        paddlePlayer = GameObject.Find("Player_Paddle");
        paddleComputer = GameObject.Find("Computer_Paddle");

        playerPaddleHeight = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        playerPaddlewidth = paddlePlayer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        computerPaddleHeight = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.y;
        computerPaddleWidth = paddleComputer.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        ballHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        ballWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x;

        playerPaddleMaxX = paddlePlayer.transform.localPosition.x + playerPaddlewidth / 2;
        playerPaddleMinX = paddlePlayer.transform.localPosition.x - playerPaddlewidth / 2;

        computerPaddleMaxX = paddleComputer.transform.localPosition.x - computerPaddleWidth / 2;
        computerPaddleMinX = paddleComputer.transform.localPosition.x + computerPaddleWidth / 2;

        bounceAngele = GetRandomBounceAngle();

        vx = moveSpeed * Mathf.Cos(bounceAngele);
        vy = moveSpeed * -Mathf.Sin(bounceAngele);

       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    bool CheckCollision()
    {
        playerPaddleMaxY = paddlePlayer.transform.localPosition.y + playerPaddleHeight / 2;
        playerPaddleMinY = paddlePlayer.transform.localPosition.y - playerPaddleHeight / 2;

        computerPaddleMaxY = paddleComputer.transform.localPosition.y + computerPaddleHeight / 2;
        computerPaddleMinY = paddleComputer.transform.localPosition.y - computerPaddleHeight / 2;

        if (transform.localPosition.x - ballWidth / 2 < playerPaddleMaxX && transform.localPosition.x + ballWidth / 2 > playerPaddleMinX)
        {
            if (transform.localPosition.y - ballHeight / 2 < playerPaddleMaxY && transform.localPosition.y + ballHeight / 2 > +playerPaddleMinY)
            {
                ballDitrction = Vector2.right;
                collidedWithPlayer = true;

                transform.localPosition = new Vector3(playerPaddleMaxX + ballWidth, transform.localPosition.y, transform.localPosition.z);
                return true;
            }
        }

        if(transform.localPosition.x + ballWidth / 2 > computerPaddleMaxX && transform.localPosition.x - ballWidth / 2 < computerPaddleMinX){

            if(transform.localPosition.y - ballHeight / 2 < computerPaddleMaxY && transform.localPosition.y + ballHeight / 2 > computerPaddleMinY)
            {
                ballDitrction = Vector2.left;

                collidedWithComputer = true;

                transform.localPosition = new Vector3(computerPaddleMaxX - ballWidth, transform.localPosition.y, transform.localPosition.z);
                return true;
            }
        }

        if(transform.localPosition.y > topBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, topBounds, transform.localPosition.z);
            collidedWithWall = true;
            return true;
        }

        if(transform.localPosition.y < bottomBounds)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, bottomBounds, transform.localPosition.z);
            collidedWithWall = true;
            return true;
        }

        return false;

    }


    void Move()
    {
        if (!CheckCollision())
        {
            vx = moveSpeed * Mathf.Cos(bounceAngele);

            if (moveSpeed > 0)
            {
                vy = moveSpeed * -Mathf.Sin(bounceAngele);
            }
            else
                vy = moveSpeed * Mathf.Sin(bounceAngele);

            transform.localPosition += new Vector3(ballDitrction.x * vx * Time.deltaTime, vy * Time.deltaTime, 0);
        }
        else
        {
            if (moveSpeed < 0)
                moveSpeed = -1 * moveSpeed;


            if (collidedWithPlayer)
            {

                collidedWithPlayer = false;
                float relativeIntersectY = paddlePlayer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY =( relativeIntersectY / (playerPaddleHeight / 2));

                bounceAngele = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            } else if(collidedWithComputer )
            {
                collidedWithComputer = false;
                float relativeIntersectY = paddleComputer.transform.localPosition.y - transform.localPosition.y;
                float normalizedRelativeIntersectionY = (relativeIntersectY / (computerPaddleHeight / 2));

                bounceAngele = normalizedRelativeIntersectionY * (maxAngle * Mathf.Deg2Rad);
            } else if (collidedWithWall)
            {
                collidedWithWall = false;

                bounceAngele = -bounceAngele;
            }
        }
    }

    float GetRandomBounceAngle(float minDegrees = 160f, float maxDegrees = 260f)
    {
        float minRad = minDegrees * Mathf.PI / 180;
        float maxRad = maxDegrees * Mathf.PI / 180;

        return Random.Range(minRad, maxRad);
    }
}
