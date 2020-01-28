using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 8.0f;
    public float topBound = 8.3f;
    public float bottomBound = -8.3f;
    public Vector2 startingPosition = new Vector2(-13.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = (Vector3)startingPosition;


    }

    // Update is called once per frame
    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (transform.localPosition.y >= topBound)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, topBound, transform.localPosition.z);
            }
            else
            {
                transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;
            }

        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            if(transform.localPosition.y <= bottomBound)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, bottomBound, transform.localPosition.z);
            }
            else
            {
                transform.localPosition += Vector3.down * moveSpeed * Time.deltaTime;
            }
        }

    }
}
