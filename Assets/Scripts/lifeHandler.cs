using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeHandler : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] string trigger;
    

        void OnCollisionEnter2D(Collision2D other)
        {
               if (other.gameObject.tag == trigger)
               {
                    if (life == 1)
                    {
                            life--;
                            Destroy(this.gameObject);
                    }
                    else if (life > 1)
                    {
                              life--;
                    }
                }
        }
}
