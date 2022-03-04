using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
	//存储当前正在使用的球
	public GameObject ball;
	//设置帧率
	public int frameRate = 60;

	// Start is called before the first frame update
	void Start()
	{
		Application.targetFrameRate = frameRate;
		//禁用除选中球外的球
		Transform[] transforms = GameObject.Find("Balls").GetComponentsInChildren<Transform>();
		foreach (var balls in transforms)
			if (balls.name != this.ball.name && balls.name != "Balls")
			{
				Debug.Log(balls.name);
				balls.gameObject.SetActive(false);
			}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetBall(GameObject ballToChange)
	{
		ball = ballToChange;
	}
}
