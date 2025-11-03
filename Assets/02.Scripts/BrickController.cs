using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour

{
  
    bool isHealMode;

    private void Update()
    {
        isHealMode = ModeSwitcher.Instance.GetCurrentMode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (ballRb != null) 
            {
                Vector2 normal = collision.contacts[0].normal;
                BrickPool.Instance.ReturnBrick(gameObject, isHealMode);

                
                Debug.Log("�浹");
            }
        }
    
    }


}
