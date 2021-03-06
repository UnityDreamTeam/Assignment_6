using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ball1 : MonoBehaviour {
	
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
	[SerializeField] int amount_of_bricks_level_2 = 16;
	[SerializeField] int amount_of_bricks_level_3 = 58;
	[SerializeField] int amount_of_bricks_level_4 = 31;

	[SerializeField] int[] amount_of_bricks_level;

	[SerializeField] protected KeyCode keyToPress;
	[SerializeField] int speed;
    private int screenScale = 2;
    private string textShow = "LEVEL ";
    private float rectWidth = 100;
    private float rectHeight = 10;
    private int diamondAddScroe = 1000;
    private int brickAddScore = 100;
    readonly int fastSpeed = 8;


	void Start()
	{
		audioSource = gameObject.GetComponents<AudioSource>();
		setSpeed(0);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
		this.transform.parent = player.GetComponent<Transform>();
		amount_of_bricks_level = new []{ amount_of_bricks_level_2, amount_of_bricks_level_3, amount_of_bricks_level_4 };
	}

    void Update()
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
            //Invoke("speed", 8);
        }

        y1 = transform.position.y;
        Invoke("check_location", 1);
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

    void check_location()
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
			float delta_x = col.gameObject.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x;
			GetComponent<Rigidbody2D>().velocity = new Vector2(-delta_x, 1).normalized * PlayerPrefs.GetInt("speed");
		}
		if(col.gameObject.tag=="diamond")
		{
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + diamondAddScroe);
			PlayerPrefs.Save();
		}
		if(col.gameObject.tag=="brick")
		{
			PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score")+brickAddScore);
			PlayerPrefs.Save();
			audioSource[brick_sound].Play();

			Debug.Log(PlayerPrefs.GetInt("count") + "");
			if (PlayerPrefs.GetInt("count") >= PlayerPrefs.GetInt("bricks"))
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				//Invoke("next_level", 0.8f);//Invoke the function after 2 seconds
			}
		}
	}

	void OnGUI()
	{
		if (PlayerPrefs.GetInt("count") >= PlayerPrefs.GetInt("bricks"))
		{
		  GUI.Label(new Rect(Screen.width / screenScale - 50f, Screen.height / screenScale, rectWidth, rectHeight), textShow + (PlayerPrefs.GetInt("level") + 1), style1);
		}
	}

    void next_level()
	{
		PlayerPrefs.SetInt("bricks", amount_of_bricks_level[PlayerPrefs.GetInt("level") - 1]);
		PlayerPrefs.SetInt("count", 0);//Set current amount of brick to zero (at the begining of new level)
		PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
		PlayerPrefs.Save();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
