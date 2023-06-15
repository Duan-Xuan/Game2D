using System;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed;
    private bool checkHeal;
    private Rigidbody2D obs;
    private BoxCollider2D obs2;

    private void Start()
    {
        
        obs = gameObject.GetComponent<Rigidbody2D>(); //anh xa
        obs2 = gameObject.GetComponent<BoxCollider2D>(); //anh xa
    }

    void Update()
    {
        Vector2 diChuyen = transform.position;
        if(checkHeal){
            diChuyen.x -= speed * Time.deltaTime;
        }else{
            diChuyen.x += speed * Time.deltaTime;
        }
        transform.position = diChuyen;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            obs.velocity = new Vector2(0, transform.localScale.y) * 5;
            obs.gravityScale = 0;
            obs2.isTrigger = true;
            Destroy(gameObject, 3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Head"){
            checkHeal = !checkHeal;
            quayLai();
        }
        if(collider.gameObject.tag == "Tail"){
            checkHeal = !checkHeal;
            quayLai();
        }
    }

    void quayLai(){
        //đổi hướng mặt của vật khi đổi hướng
        Vector2 huongQuay = transform.localScale;
        huongQuay.x *= -1;
        transform.localScale = huongQuay;
    }
}
