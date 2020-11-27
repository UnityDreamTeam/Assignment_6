using UnityEngine;
using System.Collections;

public class bricktrigger1 : MonoBehaviour {

	private int count;
	private int score;
	[SerializeField] private int scoreToAdd = 100;
	// Use this for initialization
	void Start ()
	{
		count = PlayerPrefs.GetInt("count");
		score = PlayerPrefs.GetInt("score");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "ball")
		{
			PlayerPrefs.SetInt("count", ++count);
			score += scoreToAdd;
			PlayerPrefs.SetInt("score", score);
			PlayerPrefs.Save();
			Destroy(gameObject);
		}

		if (collision.gameObject.tag == "laser")
		{
			PlayerPrefs.SetInt("count", ++count);
			score += scoreToAdd;
			PlayerPrefs.SetInt("score", score);
			PlayerPrefs.Save();
			Destroy(gameObject);
		}

	}

    void OnTriggerEnter2D(Collider2D col)
	{
		
	}
}
