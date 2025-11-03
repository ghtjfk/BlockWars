using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour

{
  
    bool isHealMode;

    private void Update()
    { //모드 체크
        isHealMode = ModeSwitcher.Instance.GetCurrentMode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { // 공과 벽돌 충돌 시 활성화
        if (collision.gameObject.CompareTag("Ball"))
        {

            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            
            if (ballRb != null) 
            { // 리스폰 위치 추가, normal 벡터 계산 후 튕기기, 깨진 블럭 pool에 리턴
                RespawnBrick.Instance.AddRespawn(transform.position);
                Vector2 normal = collision.contacts[0].normal;
                BrickPool.Instance.ReturnBrick(gameObject, isHealMode);

            }
        }
    
    }


}
