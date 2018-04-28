using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {
    //触摸UI部分的操作，用函数返回moveright/left的值来控制control脚本里的方向
    private catController cat2d;

    void Start () {
        cat2d = FindObjectOfType<catController>();
     
	}


    public void leftArrow()
    {
        cat2d.moveRight = false;
        cat2d.moveLeft = true;
    }
    public void rightArrow()
    {
        cat2d.moveRight = true;
        cat2d.moveLeft = false;
    }
    public void releaseLeftArrow()
    {
        cat2d.moveLeft = false;
    }
    public void releaseRightArrow()
    {
        cat2d.moveRight = false;
    }
    public void Jump()
    {
        cat2d.jump = true;
    }
}
