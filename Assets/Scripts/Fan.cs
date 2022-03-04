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
		//��Ҷ��ת
		GetComponentsInChildren<Transform>()[1].transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}

	//���뷶Χʱ������״̬
	void OnTriggerEnter(Collider other)
	{
		GameObject.Find("Balls").GetComponent<AddForces>().FanAffected(this.transform);
	}

	//�뿪��Χʱ������״̬
	private void OnTriggerExit(Collider other)
	{
		GameObject.Find("Balls").GetComponent<AddForces>().FanDisAffected(this.transform);
	}
}