using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBrick : MonoBehaviour
{
    [SerializeField] public GameObject power;
    private int count;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        count = PlayerPrefs.GetInt("count");
        score = PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            PlayerPrefs.SetInt("count", ++count);
            score += 100;
            PlayerPrefs.SetInt("score", score);
            PlayerPrefs.Save();
            Instantiate(power, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
