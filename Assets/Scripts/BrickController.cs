using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{

    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }
    // when something collides with the brick, this function is called
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // This method will cause the game object to disappear
        // if the object only makes contact with a certain object
        if(collision.gameObject.name == "Ball")
        {
            gameController.UpdateScore();
            Destroy(gameObject);
        }
        
    }
}
