using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForces : MonoBehaviour
{
    //摄像机
    GameObject mainCamera;
    //刚体组件
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //初始化摄像机
        mainCamera = GameObject.Find("Main Camera");
        //初始化RigidBody
        rigidbody = this.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        //获取摄像机方向
        int camDirection = mainCamera.GetComponent<CameraFollow>().camDirection;

        //移动控制
        //camDirection：摄像机指向，0=南，1=西，2=北，3=东
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            if(camDirection == 0)
            {
                addForceNorth();
            }
            if (camDirection == 1)
            {
                addForceEast();
            }
            if (camDirection == 2)
            {
                addForceSouth();
            }
            if (camDirection == 3)
            {
                addForceWest();
            }
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {

            if (camDirection == 0)
            {
                addForceWest();
            }
            if (camDirection == 1)
            {
                addForceNorth();
            }
            if (camDirection == 2)
            {
                addForceEast();
            }
            if (camDirection == 3)
            {
                addForceSouth();
            }
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {

            if (camDirection == 0)
            {
                addForceSouth();
            }
            if (camDirection == 1)
            {
                addForceWest();
            }
            if (camDirection == 2)
            {
                addForceNorth();
            }
            if (camDirection == 3)
            {
                addForceEast();
            }
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {

            if (camDirection == 0)
            {
                addForceEast();
            }
            if (camDirection == 1)
            {
                addForceSouth();
            }
            if (camDirection == 2)
            {
                addForceWest();
            }
            if (camDirection == 3)
            {
                addForceNorth();
            }
        }
    }

    void addForceNorth()
    {
        if (rigidbody.velocity.magnitude <= 50)
        {
            rigidbody.AddForce(0, 0, 100);
        }
    }

    void addForceWest()
    {
        if (rigidbody.velocity.magnitude <= 50)
        {
            rigidbody.AddForce(-100, 0, 0);
        }
    }

    void addForceSouth()
    {
        if (rigidbody.velocity.magnitude <= 50)
        {
            rigidbody.AddForce(0, 0, -100);
        }
    }
    void addForceEast()
    {
        if (rigidbody.velocity.magnitude <= 50)
        {
            rigidbody.AddForce(100, 0, 0);
        }
    }
}