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
	//����
	public float maxSpeed = 5.0f;
	//Ϊ��ʩ�����Ĺ���
	public float power = 50.0f;
	//�Ƿ��ֹ����
	public bool canMove = true;

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

		//�ж��Ƿ��ֹ����
		if (canMove)
        {
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
				Debug.Log("1");
				rigidbody.AddForce(0, 10, 0);
			}
		}
	}

	void AddForceNorth()
	{
		rigidbody.AddForce(0, 0, power);
	}
	void AddForceWest()
	{
		rigidbody.AddForce(-power, 0, 0);
	}
	void AddForceSouth()
	{
		rigidbody.AddForce(0, 0, -power);
	}
	void AddForceEast()
	{
		rigidbody.AddForce(power, 0, 0);
	}

	//void AddForceNorth()
	//{
	//	float speed = rigidbody.velocity.magnitude;
	//	if (speed == 0)
	//		rigidbody.AddForce(0, 0, power);
	//	else
	//		rigidbody.AddForce(0, 0, power / speed);
	//}

	//void AddForceWest()
	//{
	//	float speed = rigidbody.velocity.magnitude;
	//	if (speed == 0)
	//		rigidbody.AddForce(-power, 0, 0);
	//	else
	//		rigidbody.AddForce(-(power / speed), 0, 0);
	//}

	//void AddForceSouth()
	//{
	//	float speed = rigidbody.velocity.magnitude;
	//	if (speed == 0)
	//       {
	//           rigidbody.AddForce(0, 0, -power);
	//       }
	//       else
	//           rigidbody.AddForce(0, 0, -(power / speed));
	//}
	//void AddForceEast()
	//{
	//	float speed = rigidbody.velocity.magnitude;
	//	if (speed == 0)
	//		rigidbody.AddForce(power, 0, 0);
	//	else
	//		rigidbody.AddForce(power / speed, 0, 0);
	//}
}