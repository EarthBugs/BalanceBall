using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddForces : MonoBehaviour
{
	//摄像机
	GameObject mainCamera;
	//正在使用的球
	GameObject ball;
	//刚体组件
	Rigidbody rigidbody;
	//正在影响的风扇
	Transform fan;
	//是否被风扇影响
	public bool fanAffected;
	//限速
	public float maxSpeed = 5.0f;
	//为球施加的力（预设）
	public float preSetForce = 100.0f;
	//实际添加的力
	float force;
	//是否允许移动
	public bool canMove = true;

	// Start is called before the first frame update
	void Start()
	{
		//初始化摄像机
		mainCamera = GameObject.Find("Main Camera");
	}

	void FixedUpdate()
	{
		//初始化球
		ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;

		//初始化RigidBody
		rigidbody = GameObject.Find("MainNode").GetComponent<MainLogic>().ball.GetComponent<Rigidbody>();
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

		//限速
		if (rigidbody.velocity.magnitude > maxSpeed)
        {
			//对x和z轴限速，保留y轴速度
			float ySpeed = rigidbody.velocity.y;
			Vector3 speed = rigidbody.velocity.normalized * maxSpeed;
			speed.y = ySpeed;
			rigidbody.velocity = speed;
		}

		//获取摄像机方向
		int camDirection = mainCamera.GetComponent<CameraFollow>().camDirection;

		//判断是否禁止移动
		if (canMove)
        {
			force = preSetForce;
			//精确控制模式
			if (Input.GetKey(KeyCode.LeftShift))
            {
				force = preSetForce * 0.5f;
			}

			//移动控制
			//camDirection：摄像机指向，0=南，1=西，2=北，3=东
			if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
			{
				if (camDirection == 0)
				{
					AddForceNorth();
				}
				if (camDirection == 1)
				{
					AddForceEast();
				}
				if (camDirection == 2)
				{
					AddForceSouth();
				}
				if (camDirection == 3)
				{
					AddForceWest();
				}
			}
			if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
			{

				if (camDirection == 0)
				{
					AddForceWest();
				}
				if (camDirection == 1)
				{
					AddForceNorth();
				}
				if (camDirection == 2)
				{
					AddForceEast();
				}
				if (camDirection == 3)
				{
					AddForceSouth();
				}
			}
			if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
			{

				if (camDirection == 0)
				{
					AddForceSouth();
				}
				if (camDirection == 1)
				{
					AddForceWest();
				}
				if (camDirection == 2)
				{
					AddForceNorth();
				}
				if (camDirection == 3)
				{
					AddForceEast();
				}
			}
			if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
			{

				if (camDirection == 0)
				{
					AddForceEast();
				}
				if (camDirection == 1)
				{
					AddForceSouth();
				}
				if (camDirection == 2)
				{
					AddForceWest();
				}
				if (camDirection == 3)
				{
					AddForceNorth();
				}
			}


			//风扇升力
			if (fanAffected)
			{
				//升力
				float yDistance = ball.transform.position.y - fan.position.y;
				if (yDistance > 0 && ball.name == "PaperBall")
				{
					Debug.Log(yDistance);
					Debug.Log(Math.Sin(Time.time) * 20);

                    rigidbody.AddForce(0, force / (yDistance * 1.2f), 0);

                    //降低向下速度，防止浮动
                    if (rigidbody.velocity.y < 0 && yDistance <= 5)
                    {
                        rigidbody.AddForce(0, 20, 0);
                    }
					//添加小浮动
					rigidbody.AddForce(0, (float)Math.Sin(Time.time * 5) * 20, 0);

                    //向内吸力，防止飘出范围
                    Vector3 xzDistance = rigidbody.velocity;
                    xzDistance = fan.position - ball.transform.position;
                    xzDistance.y = 0;
                    rigidbody.AddForce(xzDistance * 0.5f);
                }
			}
		}
	}

	void AddForceNorth()
	{
		rigidbody.AddForce(0, 0, force);
	}
	void AddForceWest()
	{
		rigidbody.AddForce(-force, 0, 0);
	}
	void AddForceSouth()
	{
		rigidbody.AddForce(0, 0, -force);
	}
	void AddForceEast()
	{
		rigidbody.AddForce(force, 0, 0);
	}

	public void FanAffected(Transform fan)
    {
		fanAffected = true;
		this.fan = fan;

		//增加阻力，优化操作手感
		rigidbody.drag += 0.1f;
	}
	public void FanDisAffected(Transform fan)
	{
		fanAffected = false;
		this.fan = fan;

		//阻力恢复成最初值
		rigidbody.drag -= 0.1f;
	}
}