using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
	public int spinSpeed = 1000;

	GameObject ball;

	// Start is called before the first frame update
	void Start()
	{

	}


	// Update is called once per frame
	void Update()
	{
		ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
		//��Ҷ��ת
		GetComponentsInChildren<Transform>()[1].transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}

	//���뷶Χʱ�������ź�
	void OnTriggerEnter(Collider other)
	{
		if (ball.name != "PaperBall")
		{
			ball.GetComponent<AddForces>().fanAffected = true;
		}
	}

	//�뿪��Χʱ�������ź�
	private void OnTriggerExit(Collider other)
	{
		ball.GetComponent<AddForces>().fanAffected = false;
	}
}