using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour {

    public float speed = 30;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col) {
        // LeftPaddle or RightPaddle
        if((col.gameObject.name == "LeftPaddle") || 
            (col.gameObject.name == "RightPaddle"))
        {
            HandlePaddleHit(col);
        }
        //WallBottom or WallTop
        if ((col.gameObject.name == "WallBottom") ||
            (col.gameObject.name == "WallTop"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }
        //LeftGoal or RightGoal
        if ((col.gameObject.name == "LeftGoal") ||
            (col.gameObject.name == "RightGoal"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            //Score
            if(col.gameObject.name == "RightGoal")
            {
                IncreaseTestUIScore("LeftScoreUI");
                transform.position = new Vector2(0, 0);
                speed = 30;
                rigidBody.velocity = Vector2.left * speed;
            }

            if (col.gameObject.name == "LeftGoal")
            {
                IncreaseTestUIScore("RightScoreUI");
                transform.position = new Vector2(0, 0);
                speed = 30;
                rigidBody.velocity = Vector2.right * speed;
            }

            
        }
    }

    void HandlePaddleHit(Collision2D col)
    {
        float y = BallHitPaddleWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);

        Vector2 dir = new Vector2();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);

        if (col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;
            speed = speed + 1;
        }

        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;
            speed = speed + 4;
        }

        rigidBody.velocity = dir * speed;
        
        

    }

    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void IncreaseTestUIScore(string textUIName)
    {
        var textUIComp = GameObject.Find(textUIName).GetComponent<Text>();

        int score = int.Parse(textUIComp.text);

        score++;

        textUIComp.text = score.ToString();

    }


}
