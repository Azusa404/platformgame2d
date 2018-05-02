//相机平滑思路就是让相机的transform.position去追player的position，用Mathf.SmoothDamp取值就行了。velocity是相机的移动速度。
//相机边界就是bound的时候让相机的position限定在Mathf.Clamp的范围里
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    public float smoothTime;

    public Vector3 minCamera;
    public Vector3 maxCamera;
    public bool bound;
    
    [SerializeField]private Vector2 velocity;



	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void FixedUpdate () {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bound)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamera.x, maxCamera.x), 
                Mathf.Clamp(transform.position.y, minCamera.y, maxCamera.y),
                //Mathf.Clamp(transform.position.z, minCamera.z, maxCamera.z)
                transform.position.z);
        }
    }
}
