using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour

{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BrickPool.Instance.ReturnBrick(gameObject);

            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (ballRb != null) 
            {
                Vector2 normal = collision.contacts[0].normal;
                ballRb.velocity = Vector2.Reflect(ballRb.velocity, normal);

                Debug.Log("Ãæµ¹");
            }
        }
    
    }


}
