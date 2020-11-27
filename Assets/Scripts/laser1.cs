using UnityEngine;
using System.Collections;

public class laser1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y >= 4)
		{
			Destroy(gameObject);
		}
	}
	
}
