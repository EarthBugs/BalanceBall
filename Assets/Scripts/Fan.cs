using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
	public int spinSpeed = 1000;

	GameObject ball;

	// Update is called once per frame
	void Update()
	{
		//��Ҷ��ת
		GetComponentsInChildren<Transform>()[1].transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}

	//���뷶Χʱ������״̬
	void OnTriggerEnter(Collider other)
	{
		//�����ײ���Ƿ�����
		if (other == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
			GameObject.Find("Balls").GetComponent<AddForces>().FanAffected(this.transform);
	}

	//�뿪��Χʱ������״̬
	private void OnTriggerExit(Collider other)
	{
		//�����ײ���Ƿ�����
		if (other == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
			GameObject.Find("Balls").GetComponent<AddForces>().FanDisAffected(this.transform);
	}
}