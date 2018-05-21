///4月30日：Animator：添加人物的idle与run动画效果
///5月1日：canDoubleJump添加二段跳。心得：有键盘输入的地方尽量别用else，检测速度跟不上
///5月2日：添加相机平滑CameraFollow
///5月2日2：添加相机移动范围bound
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Fire 开火
    public GameObject bulletToRight;   
    public GameObject bulletToLeft;   
    public Transform firePoint; 
    public bool fire;           
    public float fireRate = 0.5f;

    //Stats 人物属性
    public int curHP;
    public int maxHP = 5;

    //Touch 触摸UI
    public bool moveRight;
    public bool moveLeft;
    public bool jump;

    Animator _anim;//动画
    public bool canDoubleJump = true;//二段跳


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

        curHP = maxHP;
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
        //if (!Input.GetKey(KeyCode.LeftArrow) && !moveLeft && !Input.GetKey(KeyCode.RightArrow) && !moveRight)
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

        //跳跃&二段跳
        if (Input.GetKeyDown(KeyCode.Space) || jump)
        {
            if (onGround)
            {
                cat2d.velocity = new Vector2(cat2d.velocity.x, jumpheight);
                jump = false;
                canDoubleJump = true;
                //_anim.SetTrigger("jump");
                Debug.Log("normaljump");
            }
            if (!onGround)
            {
                if (canDoubleJump)
                {
                    jump = false;
                    canDoubleJump = false;
                    cat2d.velocity = new Vector2(cat2d.velocity.x, jumpheight);
                    Debug.Log("Double jump");
                }
            }
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

        //检查血量
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
        if (curHP <= 0)
        {
            //
            //StartCoroutine(DelayScript.run(() => { Reborn(); }, 3));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Restart : " + SceneManager.GetActiveScene().name);
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
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("1234"))
    //    {
    //        Death();

    //        //Reborn();
    //        StartCoroutine(DelayScript.run(() =>
    //        {
    //            Reborn();
    //        }, 3));
    //    }
    //}

    void Death()
    {
        Instantiate(Explode, transform.position, transform.rotation);
        enabled = false;
        //transform.position = new Vector3(transform.position.x-3f, transform.position.y, transform.position.z);
        transform.position = startPoint.position;
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
    public void Damage(int dmg)
    {
        curHP -= dmg;
    }
}
  