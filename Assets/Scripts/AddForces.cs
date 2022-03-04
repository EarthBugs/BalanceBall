using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddForces : MonoBehaviour
{
	//�����
	GameObject mainCamera;
	//����ʹ�õ���
	GameObject ball;
	//�������
	Rigidbody rigidbody;
	//����Ӱ��ķ���
	Transform fan;
	//�Ƿ񱻷���Ӱ��
	public bool fanAffected;
	//����
	public float maxSpeed = 5.0f;
	//Ϊ��ʩ�ӵ�����Ԥ�裩
	public float preSetForce = 100.0f;
	//ʵ����ӵ���
	float force;
	//�Ƿ������ƶ�
	public bool canMove = true;

	// Start is called before the first frame update
	void Start()
	{
		//��ʼ�������
		mainCamera = GameObject.Find("Main Camera");
	}

	void FixedUpdate()
	{
		//��ʼ����
		ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;

		//��ʼ��RigidBody
		rigidbody = GameObject.Find("MainNode").GetComponent<MainLogic>().ball.GetComponent<Rigidbody>();
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

		//����
		if (rigidbody.velocity.magnitude > maxSpeed)
        {
			//��x��z�����٣�����y���ٶ�
			float ySpeed = rigidbody.velocity.y;
			Vector3 speed = rigidbody.velocity.normalized * maxSpeed;
			speed.y = ySpeed;
			rigidbody.velocity = speed;
		}

		//��ȡ���������
		int camDirection = mainCamera.GetComponent<CameraFollow>().camDirection;

		//�ж��Ƿ��ֹ�ƶ�
		if (canMove)
        {
			force = preSetForce;
			//��ȷ����ģʽ
			if (Input.GetKey(KeyCode.LeftShift))
            {
				force = preSetForce * 0.5f;
			}

			//�ƶ�����
			//camDirection�������ָ��0=�ϣ�1=����2=����3=��
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


			//��������
			if (fanAffected)
			{
				//����
				float yDistance = ball.transform.position.y - fan.position.y;
				if (yDistance > 0 && ball.name == "PaperBall")
				{
					Debug.Log(yDistance);
					Debug.Log(Math.Sin(Time.time) * 20);

                    rigidbody.AddForce(0, force / (yDistance * 1.2f), 0);

                    //���������ٶȣ���ֹ����
                    if (rigidbody.velocity.y < 0 && yDistance <= 5)
                    {
                        rigidbody.AddForce(0, 20, 0);
                    }
					//���С����
					rigidbody.AddForce(0, (float)Math.Sin(Time.time * 5) * 20, 0);

                    //������������ֹƮ����Χ
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

		//�����������Ż������ָ�
		rigidbody.drag += 0.1f;
	}
	public void FanDisAffected(Transform fan)
	{
		fanAffected = false;
		this.fan = fan;

		//�����ָ������ֵ
		rigidbody.drag -= 0.1f;
	}
}