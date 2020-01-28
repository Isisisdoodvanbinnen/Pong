using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float topBound = 8.3f;
    public float bottomBound = -8.3f;
    public Vector2 startingPosition = new Vector2(13.0f, 0.0f);


    private GameObject ball;
    private Vector2 ballPos;


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = (Vector3)startingPosition;


    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (!ball)
            ball = GameObject.FindGameObjectWithTag("ball");

        if(ball.GetComponent<Ball> ().ballDitrction == Vector2.right){

            ballPos = ball.transform.localPosition;

            if(transform.localPosition.y > bottomBound && ballPos.y < transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
            }

            if(transform.localPosition.y < topBound && ballPos.y > transform.localPosition.y)
            {
                transform.localPosition += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            }
        }
    }

}
