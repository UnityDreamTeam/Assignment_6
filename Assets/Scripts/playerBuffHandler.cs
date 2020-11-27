using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBuffHandler : MonoBehaviour
{
	public GameObject ball;
	public bool flag;
	public bool gameover = false;
	public GameObject big;
	Vector3 normalSize;
	bool isBigBall = false; // check if the player take power bigBall
	// Start is called before the first frame update
	void Start()
    {
		normalSize = new Vector3(ball.transform.localScale.x, ball.transform.localScale.y, ball.transform.localScale.z);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
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
			PlayerPrefs.SetInt("speed", 10);
			PlayerPrefs.Save();
			Rigidbody2D ballclone = GameObject.Find("ball").GetComponent<Rigidbody2D>();
			ballclone.velocity = ballclone.velocity.normalized * PlayerPrefs.GetInt("speed");
		}
		if (col.gameObject.tag == "slowbrick")
		{
			Destroy(col.gameObject);
			PlayerPrefs.SetInt("speed", 4);
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
			
			ball.transform.localScale = new Vector2(ball.transform.localScale.x * 6 / 2, ball.transform.localScale.y * 6 / 2);
			
			//yield on a new YieldInstruction that waits for 2 seconds.
			yield return new WaitForSeconds(2);

			ball.transform.localScale = normalSize;
			isBigBall = false;
		}
	}
}
