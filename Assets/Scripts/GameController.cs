using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField]private PlayerInput playerInput;
    // SerializeField: allows the variable to stay private yet
    // can still be seen in Unity UI

    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction launchBall;

    // setting a flag/boolean to check condition of an action
    private bool isPaddleMoving;
    // reference for the boolean
    [SerializeField]private GameObject paddle;

    //variables for speed
    [SerializeField]private float paddleSpeed;

    // Direction for the paddle
    private float moveDirection;

    [SerializeField] private GameObject brick;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score;
    [SerializeField] private TMP_Text endGameText;
    [SerializeField] private TMP_Text livesText;
    private int lives;

    private BallController ballController;

    [SerializeField] private TMP_Text restartText;
    [SerializeField] private TMP_Text launchText;

    // Start is called before the first frame update
    void Start()
    {
        // Activating the Action Map
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("MovePaddle");
        restart = playerInput.currentActionMap.FindAction("RestartGame");
        quit = playerInput.currentActionMap.FindAction("QuitGame");
        launchBall = playerInput.currentActionMap.FindAction("LaunchBall");

        // .started/.canceled: it's an action that tells me when a certain
        // action has happened in the game... it's a listener
        move.started += Move_Started;
        move.canceled += Move_canceled;
        restart.started += Restart_started;
        quit.started += Quit_started;
        launchBall.started += LaunchBall_started;

        isPaddleMoving = false;
        // I highlighted everything, right click, Quick Actions and
        // Refactoring, Extract Method, then make my new method name
        CreateAllBricks();

        endGameText.gameObject.SetActive(false);

        ballController = GameObject.FindObjectOfType<BallController>();

        lives = 3;
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();

        restartText.gameObject.SetActive(false);

        launchText.gameObject.SetActive(true);


        

    }

    private void LaunchBall_started(InputAction.CallbackContext obj)
    {
        ballController.LaunchBall();
        launchText.gameObject.SetActive(false);
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();

        if(score >= 4000)
        {
            endGameText.text = "You Win";
            endGameText.gameObject.SetActive(true);
            ballController.StopBall();
            paddle.SetActive(false);
            restartText.gameObject.SetActive(true);
        }
    }

    private void CreateAllBricks()
    {
        Vector2 brickPos = new Vector2(-9, 4.5f);


        for (int j = 0; j < 4; j++)
        {
            brickPos.y -= 1;
            brickPos.x = -9;

            for (int i = 0; i < 10; i++)
            {
                brickPos.x += 1.6f;
                Instantiate(brick, brickPos, Quaternion.identity);
            }
        }
    }

    // When the move has ended (I've let go of the button)
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
        restartText.gameObject.SetActive(false);
    }

    // When the move has started (I started pressing a button)
    private void Move_Started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;
    }


    // A fixed frame rate update
    private void FixedUpdate()
    {
        if(isPaddleMoving)
        {
            // move the paddle/ manipulate velocity of paddle
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2( paddleSpeed * moveDirection, 0);
        }
        else
        {
            // stop paddle from moving
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaddleMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }

    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives.ToString();
        launchText.gameObject.SetActive(true);

        if(lives == 0)
        {
            endGameText.text = "You Lose";
            endGameText.gameObject.SetActive(true);
            ballController.StopBall();
            paddle.SetActive(false);
            restartText.gameObject.SetActive(true);

        }
    }

    public void OnDestroy()
    {
        move.started -= Move_Started;
        move.canceled -= Move_canceled;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
        launchBall.started -= LaunchBall_started;
    }
}
