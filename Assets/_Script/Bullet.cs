using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    private Rigidbody2D newbullet;

    void Start ()
    {
        newbullet = GetComponent<Rigidbody2D>();
    }


    void Update ()
    {
        newbullet.velocity = new Vector2(bulletSpeed,0); //子弹速度

        GameObject.Destroy(gameObject, 5.0f);

	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("1234"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
