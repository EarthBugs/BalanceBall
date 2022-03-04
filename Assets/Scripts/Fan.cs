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
		//扇叶旋转
		GetComponentsInChildren<Transform>()[1].transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
	}

	//进入范围时，设置状态
	void OnTriggerEnter(Collider other)
	{
		GameObject.Find("Balls").GetComponent<AddForces>().FanAffected(this.transform);
	}

	//离开范围时，设置状态
	private void OnTriggerExit(Collider other)
	{
		GameObject.Find("Balls").GetComponent<AddForces>().FanDisAffected(this.transform);
	}
}