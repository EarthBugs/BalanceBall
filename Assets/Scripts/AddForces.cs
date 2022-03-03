using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForces : MonoBehaviour
{
	//�����
	GameObject mainCamera;
	//����ʹ�õ���
	GameObject ball;
	//�������
	Rigidbody rigidbody;
	//�Ƿ񱻷���Ӱ��
	public bool fanAffected;

	// Start is called before the first frame update
	void Start()
	{
		//��ʼ�������
		mainCamera = GameObject.Find("Main Camera");
	}

	void FixedUpdate()
	{
		//��ʼ��RigidBody
		rigidbody = GameObject.Find("MainNode").GetComponent<MainLogic>().ball.GetComponent<Rigidbody>();
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

		//��ȡ���������
		int camDirection = mainCamera.GetComponent<CameraFollow>().camDirection;

		//�ƶ�����
		//camDirection�������ָ��0=�ϣ�1=����2=����3=��
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
		{
			if(camDirection == 0)
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
		if(fanAffected && rigidbody.velocity.magnitude <= 5)
		{
			rigidbody.AddForce(0, 150, 0);
		}
	}

	void AddForceNorth()
	{
		if (rigidbody.velocity.x <= 5)
		{
			rigidbody.AddForce(0, 0, 100);
		}
	}

	void AddForceWest()
	{
		if (rigidbody.velocity.x >= -5)
		{
			rigidbody.AddForce(-100, 0, 0);
		}
	}

	void AddForceSouth()
	{
		if (rigidbody.velocity.magnitude >= -5)
		{
			rigidbody.AddForce(0, 0, -100);
		}
	}
	void AddForceEast()
	{
		if (rigidbody.velocity.x <= 5)
		{
			rigidbody.AddForce(100, 0, 0);
		}
	}
}