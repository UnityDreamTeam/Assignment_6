using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class countChildren : MonoBehaviour 
{ 
    [SerializeField] public int myLVL;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            SceneManager.LoadScene(++myLVL);
        }
        
    }
}
