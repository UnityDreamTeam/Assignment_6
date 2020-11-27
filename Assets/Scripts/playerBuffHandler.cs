using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBuffHandler : MonoBehaviour
{
	[SerializeField] GameObject ball;
	bool flag;
	bool gameover = false;
	[SerializeField] int fastBrick = 10;
	[SerializeField] int slowBrick = 4;

	Vector3 normalSize;
	bool isBigBall = false; // check if the player take power bigBall
    private float bigBallX;
    private float bigBallY;
	[SerializeField] private int scale_size = 3;

    // Start is called before the first frame update
    void Start()
    {
		normalSize = new Vector3(ball.transform.localScale.x, ball.transform.localScale.y, ball.transform.localScale.z);
		bigBallX = ball.transform.localScale.x * scale_size;
		bigBallY = ball.transform.localScale.y * scale_size;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		if (col.gameObject.tag == "ballsbrick")
		{
			Destroy(col.gameObject);
			Instantiate(ball, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			PlayerPrefs.SetInt("ballcount", PlayerPrefs.GetInt("ballcount") + 1);
			PlayerPrefs.Save();
		}
		if (col.gameObject.tag == "laserbrick")
		{
			Destroy(col.gameObject);
			flag = true;
		}
		if (col.gameObject.tag == "fastbrick")
		{
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("speed", fastBrick);
			PlayerPrefs.Save();
			Rigidbody2D ballclone = GameObject.Find("ball").GetComponent<Rigidbody2D>();
			ballclone.velocity = ballclone.velocity.normalized * PlayerPrefs.GetInt("speed");
		}
		if (col.gameObject.tag == "slowbrick")
		{
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("speed", slowBrick);
			PlayerPrefs.Save();
			Rigidbody2D ballclone = GameObject.Find("ball").GetComponent<Rigidbody2D>();
			ballclone.velocity = ballclone.velocity.normalized * PlayerPrefs.GetInt("speed");
		}
		if (col.gameObject.tag == "death")
		{
			gameover = true;
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "expand")
		{
			transform.localScale = new Vector2(.45f, transform.localScale.y);
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "shrink")
		{
			transform.localScale = new Vector2(.25f, transform.localScale.y);
			Destroy(col.gameObject);
		}
		if (col.gameObject.tag == "bigball")
		{
			if (!isBigBall)
			{
				Destroy(col.gameObject);
				StartCoroutine(bigBallTime());
				isBigBall = true;
			}
        }

		IEnumerator bigBallTime()
		{
			ball.transform.localScale = new Vector2(bigBallX, bigBallY);	
			//yield on a new YieldInstruction that waits for 2 seconds.
			yield return new WaitForSeconds(2);
			ball.transform.localScale = normalSize;
			isBigBall = false;
		}
	}
}
