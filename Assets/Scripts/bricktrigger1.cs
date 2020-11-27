using UnityEngine;
using System.Collections;

public class bricktrigger1 : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "ball")
		{
			PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 100);
			PlayerPrefs.Save();
			Destroy(gameObject);
		}

		if (collision.gameObject.tag == "laser")
		{
			Destroy(collision.gameObject);
			PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 100);
			PlayerPrefs.Save();
			Destroy(gameObject);
		}

	}

    void OnTriggerEnter2D(Collider2D col)
	{
		
	}
}
