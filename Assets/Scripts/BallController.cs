using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject paddle;
    // Start is called before the first frame update
    void Start()
    {
       GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
    }

    public void ResetBall()
    {
        gameObject.transform.position = paddle.transform.position + new Vector3(0, 0.5f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
