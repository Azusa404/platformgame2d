using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour {

    public float w;
    public float speed;
    void Start()
    {

    }
	

	void Update () {
        
        // transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x + w*Mathf.Sin(Time.time*speed), transform.position.y, transform.position.z);
	}
}
