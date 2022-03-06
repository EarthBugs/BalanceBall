using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
	//��ǰ�Ƿ�������ʾUI
	public bool uiIsShow = false;
	//�洢��ǰ����ʹ�õ���
	public GameObject ball;
	//����֡��
	public int frameRate = 60;

	// Start is called before the first frame update
	void Start()
	{
		Application.targetFrameRate = frameRate;

		//����ѡ�е��򣬽��ó�ѡ���������
		Transform[] transforms = GameObject.Find("Balls").GetComponentsInChildren<Transform>();
		foreach (var balls in transforms)
			if (balls.name != this.ball.name && balls.name != "Balls")
			{
				balls.gameObject.SetActive(false);
			}
			else if (balls.name == this.ball.name)
				balls.gameObject.SetActive(true);
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
