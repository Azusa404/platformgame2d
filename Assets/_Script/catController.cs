///4月30日：Animator：添加人物的idle与run动画效果
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catController : MonoBehaviour {

    public Rigidbody2D cat2d;
    public float movespeed;
    public float jumpheight;
    public Transform groundcheck;
    public LayerMask whatisground;
    public float groundcheckRadius;
    public bool facingrRight;   //面向
    public Transform startPoint;//重生点
    public GameObject Explode;  //死亡特效
    public GameObject bulletToRight;   //开火
    public GameObject bulletToLeft;   //开火
    public Transform firePoint; //开火
    public bool fire;           //开火
    public float fireRate = 0.5f;//开火
    public bool moveRight;
    public bool moveLeft;
    public bool jump;
    Animator _anim;//动画



    private bool onGround;
    private Vector3 theScale;
    private float h;
    private float nextFire = 0;
    
    void Start()
    {
        cat2d = GetComponent<Rigidbody2D>();
        facingrRight = true;
        cat2d.transform.position = startPoint.position;
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ////检测键盘输入（两种方法）
        
        if (Input.GetKey(KeyCode.LeftArrow) || moveLeft|| Input.GetKey(KeyCode.RightArrow) || moveRight)
        {
        //左
            if (Input.GetKey(KeyCode.LeftArrow)|| moveLeft)
        {
            cat2d.velocity = new Vector2(-movespeed, cat2d.velocity.y);
        }

        //右
        if (Input.GetKey(KeyCode.RightArrow)|| moveRight)
        {
            cat2d.velocity = new Vector2(movespeed, cat2d.velocity.y);
        }

            _anim.SetBool("run", true);
        }
        else
        {
            _anim.SetBool("run", false);
        }

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        //cat2d.AddForce(movement * movespeed);

        //检测是否在跳跃
        onGround = Physics2D.OverlapCircle(groundcheck.position, groundcheckRadius, whatisground);

        //跳跃
        if ((Input.GetKey(KeyCode.Space)||jump) && onGround)
        {
            cat2d.velocity = new Vector2(cat2d.velocity.x, jumpheight);
            jump = false;
            //_anim.SetTrigger("jump");
        }

        //开火功能
        if ((Input.GetKey(KeyCode.Z) || fire) && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
            _anim.SetTrigger("fire");
            if (facingrRight)
            {
                Instantiate(bulletToRight, firePoint.position, firePoint.rotation);
                fire = false;
            }
            else
            {
                Instantiate(bulletToLeft, firePoint.position, firePoint.rotation);
                fire = false;
            }
            
        }
        //判断面向是否正确，不正确则翻转
        h = cat2d.velocity.x;
        if (h > 0 && !facingrRight)
        {
            Flip();
        }
        if (h < 0 && facingrRight)
        {
            Flip();
        }

    }
        //判断动画状态

    void Flip()
    {   //翻转x轴scale
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //翻转面向标记
        facingrRight = !facingrRight;
        //Debug.Log("Flip");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("1234"))
        {
            Death();
            Reborn();
        }
    }

    void Death()
    {
        Instantiate(Explode, transform.position, transform.rotation);
        enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Renderer>().enabled = false;
        Debug.Log("You've been Dead");
    }

    void Reborn()
    {
        transform.position = startPoint.position;
        enabled = true;
        GetComponent<Renderer>().enabled = true;
    }
}
  