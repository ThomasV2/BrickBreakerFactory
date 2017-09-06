using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float BallInitialVelocity = 600f;


    private Rigidbody2D rb;
    private Transform paddle;
    private float initPosY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initPosY = this.transform.position.y;
        paddle = this.transform.parent;
    }

    public void Eject()
    {
        this.transform.parent = null;
        rb.AddForce(new Vector2(0f, BallInitialVelocity));
    }

    public void Reload()
    {
        this.rb.velocity = Vector2.zero;
        this.transform.parent = paddle;
        this.transform.position = new Vector3 (paddle.transform.position.x, initPosY, 0f);
        paddle.GetComponent<Paddle>().BallRunning = false;
        this.enabled = false;
    }
}
