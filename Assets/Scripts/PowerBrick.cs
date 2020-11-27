using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBrick : MonoBehaviour
{

    [SerializeField] public GameObject power;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 100);
            PlayerPrefs.Save();
            Instantiate(power, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("count", PlayerPrefs.GetInt("count") + 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 100);
            PlayerPrefs.Save();
            Instantiate(power, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        
    }
}
