using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball1 : MonoBehaviour {
	
	GameObject script1; 
	int ballcount=0;
	int lasercount=0;
	int slowcount=0;
	int fastcount=0;
	int expandcount=0;
	int shrinkcount=0;
	int deathcount=0;
	int bigcount=0;
	public GUIStyle style1;
	public GameObject balls;
	public GameObject laser;
	public GameObject slow;
	public GameObject fast;
	public GameObject expand;
	public GameObject shrink;
	public GameObject death;
	public GameObject bigball;
	public Text gscore;
	float y1=0;
	float y2=0;

	AudioSource[] audioSource;
	readonly int player_sound = 0;
	readonly int brick_sound = 1;

	[SerializeField] protected KeyCode keyToPress;
	[SerializeField] int speed;
	readonly int fastSpeed = 8;
	void Start()
	{
		audioSource = gameObject.GetComponents<AudioSource>();
		setSpeed(0);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		this.transform.parent = player.GetComponent<Transform>();
	}

    private void Update()
    {
		if (Input.GetKeyDown(keyToPress))
		{
			this.transform.parent = null;
			this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			setSpeed(speed);
			audioSource[player_sound].enabled = true;
			audioSource[brick_sound].enabled = true;
		}
	}

    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("speed") == 4 || PlayerPrefs.GetInt("speed") == 10)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = rb.velocity.normalized * PlayerPrefs.GetInt("speed");
            Invoke("speed", 8);
        }

        y1 = transform.position.y;
        Invoke("check", 1);
        if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("pscore"))
        {
            for (int i = 1; i <= (PlayerPrefs.GetInt("score") - PlayerPrefs.GetInt("pscore")); i++)
            {
                PlayerPrefs.SetInt("pscore", PlayerPrefs.GetInt("pscore") + i);
                //gscore.text = PlayerPrefs.GetInt("pscore").ToString();
                PlayerPrefs.Save();
            }
        }
    }

    void check()
	{
		y2=transform.position.y;
		if(y1==y2)
		{
			GetComponent<Rigidbody2D>().velocity=new Vector2(transform.position.x, Random.Range(-1,1)).normalized*PlayerPrefs.GetInt("speed");
		}
	}

    void setSpeed(int speed)
    {
        PlayerPrefs.SetInt("speed", speed);
        PlayerPrefs.Save();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = rb.velocity.normalized * PlayerPrefs.GetInt("speed");
    }

    void OnCollisionEnter2D(Collision2D col) 
	{
		if (col.gameObject.tag=="Player") 
		{
			audioSource[player_sound].Play();
			//GetComponent<Rigidbody2D>().velocity =  new Vector2(Random.Range(-2f,2f), Random.Range(1,4f)).normalized*PlayerPrefs.GetInt("speed");

			float delta_x = col.gameObject.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x;
			GetComponent<Rigidbody2D>().velocity = new Vector2(-delta_x, 1).normalized * PlayerPrefs.GetInt("speed");
		}
		if(col.gameObject.tag=="diamond")
		{
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score")+1000);
			PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count")+1);
			PlayerPrefs.Save();
			Destroy(col.gameObject);
		}
		if(col.gameObject.tag=="brick")
		{
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score")+100);
			PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count")+1);
			PlayerPrefs.Save();
			audioSource[brick_sound].Play();
			/*if(ballcount==(int)Random.Range(0,50))
			{
				 Instantiate(balls, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(lasercount==(int)Random.Range(0,50))
			{
				 Instantiate(laser, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(slowcount==(int)Random.Range(0,50))
			{
				Instantiate(slow, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(fastcount==(int)Random.Range(0,50))
			{
				Instantiate(fast, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(expandcount==(int)Random.Range(0,50))
			{
				Instantiate(expand, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(shrinkcount==(int)Random.Range(0,50))
			{
				Instantiate(shrink, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(deathcount==(int)Random.Range(0,50))
			{
				Instantiate(death, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			if(bigcount==(int)Random.Range(0,50))
			{
				Instantiate(bigball, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
			}
			*/
			Destroy(col.gameObject);
			//ballcount=lasercount=slowcount=fastcount=expandcount=shrinkcount=deathcount=bigcount=(int)Random.Range(0,50);
		}
	}
	
/*	void OnGUI()
	{
		if(PlayerPrefs.GetInt("count")>=PlayerPrefs.GetInt("bricks"))
		{
			GUI.Label(new Rect(Screen.width/2-50f, Screen.height/2, 100, 10), "LEVEL "+PlayerPrefs.GetInt("level"), style1);
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//script1.GetComponent<Player>().enabled=false;
			Invoke("next", 2);
		}
	}*/

    void next()
	{

		if(PlayerPrefs.GetInt("level")==2)
		{
			PlayerPrefs.SetInt("bricks", 38);
			PlayerPrefs.SetInt("count", 0);
			PlayerPrefs.SetInt("ballcount", 0);
			PlayerPrefs.Save();
		}
		if(PlayerPrefs.GetInt("level")==1)
		{
			PlayerPrefs.SetInt("bricks", 33);
			PlayerPrefs.SetInt("count", 0);
			PlayerPrefs.Save();
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
