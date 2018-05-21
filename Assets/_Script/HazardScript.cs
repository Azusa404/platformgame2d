using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour {

    public float w;
    public float speed;

    private catController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<catController>();
    }
	

	void Update () {

        // transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x + w * Mathf.Sin(Time.time * speed) * Time.timeScale, transform.position.y, transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Damage(1);
        }
    }
}
