using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject paddle;
    private bool isBallInPlay;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
        gameController = GameObject.FindAnyObjectByType<GameController>();
    }

    public void ResetBall()
    {
        gameObject.transform.position = paddle.transform.position + new Vector3(0, 0.5f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isBallInPlay = false;

    }

    public void LaunchBall()
    {
        if(!isBallInPlay)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
            isBallInPlay = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isBallInPlay)
        {
            gameObject.transform.position = paddle.transform.position + new Vector3(0, 0.5f);
        }
        else if(gameObject.transform.position.y < -6)
        {
            // Lose a life
            gameController.LoseLife();
            // Reset the ball
            ResetBall();
        }
    }

    public void StopBall()
    {
        gameObject.SetActive(false);
    }
}
