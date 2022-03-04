using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //跟随目标
    public GameObject ball;
    //存储摄像机指向，0=南，1=西，2=北，3=东
    public int camDirection = 0;
    //跟随速度（灵敏度）
    public float speed = 1f;
    //摄像机偏移量
    Vector3 offset = new Vector3(0, 3, -6);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
        
        //摄像机平滑跟随
        this.transform.position = Vector3.Lerp(this.transform.position,
            ball.transform.position + offset, Time.deltaTime * speed);
        rotateCam();
        camLookAt();
    }

    //手动旋转摄像机
    void rotateCam()
    {
        //Debug.Log("camDirection" + camDirection + ", offset" + offset);

        //顺时针旋转
        if (Input.GetKeyDown("q"))
        {
            camDirection = (camDirection + 1) % 4;
            //修改offset，0=南，1=西，2=北，3=东
            if (camDirection == 0)
            {
                offset = new Vector3(0, 5, -6);
            }
            else if (camDirection == 1)
            {
                offset = new Vector3(-6, 5, 0);
            }
            else if (camDirection == 2)
            {
                offset = new Vector3(0, 5, 6);
            }
            else if (camDirection == 3)
            {
                offset = new Vector3(6, 5, 0);
            }
        }
        //逆时针旋转
        if (Input.GetKeyDown("e"))
        {
            if (camDirection <= 0)
                camDirection = 3;
            else
                camDirection -= 1;
            //修改offset，0=南，1=西，2=北，3=东
            if (camDirection == 0)
            {
                offset = new Vector3(0, 5, -6);
            }
            else if (camDirection == 1)
            {
                offset = new Vector3(-6, 5, 0);
            }
            else if (camDirection == 2)
            {
                offset = new Vector3(0, 5, 6);
            }
            else if (camDirection == 3)
            {
                offset = new Vector3(6, 5, 0);
            }
        }
    }

    //摄像机指向球
    void camLookAt()
    {
        //摄像机跟随
        this.transform.LookAt(ball.transform.position);
    }

    void ChangeBall(GameObject ballToChange)
    {
        ball = ballToChange;
    }
}