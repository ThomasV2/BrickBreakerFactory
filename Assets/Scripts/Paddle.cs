using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    public float PaddleSpeed = 1f;
    [HideInInspector]
    public bool BallRunning = false;

    private Vector3 playerPos = new Vector3(0, -4.2f, 0);


    void Update()
    {
        float xPos = playerPos.x;

		#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.Space) && BallRunning == false)
		{
			GameObject.Find("Ball").GetComponent<Ball>().Eject();
			BallRunning = true;
		}
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xPos = transform.position.x + Time.deltaTime * PaddleSpeed * -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            xPos = transform.position.x + Time.deltaTime * PaddleSpeed;
        }
		#else
		foreach (Touch touch in Input.touches) 
		{
			if (BallRunning == false)
			{
			GameObject.Find("Ball").GetComponent<Ball>().Eject();
			BallRunning = true;
			}
			xPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z)).x;
			break;
		}
		#endif
        playerPos = new Vector3(Mathf.Clamp(xPos, -2.44f, 2.44f), -4.2f, 0f);
        transform.position = playerPos;
    }

}
