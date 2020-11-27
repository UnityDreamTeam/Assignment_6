using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Tooltip("Speed of movement, in meters per second")]
    [SerializeField] float speed = 1f;
    [SerializeField] Rigidbody2D rb2D;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        Vector2 movementVector = new Vector2(horizontal, 0) * speed * Time.fixedDeltaTime;

        //Update rigidbody position based on movement vector
        rb2D.MovePosition(rb2D.position + movementVector + rb2D.velocity * Time.fixedDeltaTime);
    }

    //diversion ball in random direction when hit the paddle(player)
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ball")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(1, 4f)).normalized * PlayerPrefs.GetInt("speed");
        }
    }

    //Called whenever a trigger has entered this objects BoxCollider2D. The value 'col' is the Collider2D object that has interacted with this one
    /*	void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Ball")
            {                                           //Is the colliding object got the tag "Ball"?
                col.gameObject.GetComponent<Ball1>().SetDirection(transform.position);   //Get the 'Ball' component of the colliding object and call the 'SetDirection()' function to bounce the ball of the paddle
            }
        }*/
}
