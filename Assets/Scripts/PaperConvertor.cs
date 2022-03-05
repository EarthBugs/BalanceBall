using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaperConvertor : MonoBehaviour
{
	//工作状态，0=开盖完成正在等待球进入，1=球已进入正在工作，2=工作完成正在开盖，
	//3=开盖完成正在升起，4=升起完成正在关盖，5=关盖完成正在cd，6=cd完成正在开盖
	public int state = 0;
	//工作时长
	public float duration = 3.0f;
	//工作计时器
	float workTimer = 0f;
	//重新开放cd时长
	public float cdTime = 3.0f;
	//cd计时器
	float cdTimer = 0;
	//开关盖速度
	public float lidSpeed = 0.1f;
	//底部升起速度
	public float bottomSpeed = 0.05f;

	Transform bottom;
	Transform lid0;
	Transform lid1;
	public GameObject ball;

	// Start is called before the first frame update
	void Start()
	{
		bottom = this.transform.parent.GetChild(0);
		lid0 = this.transform.parent.GetChild(1);
		lid1 = this.transform.parent.GetChild(2);
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(state);

		//工作开始关盖
		if (state == 1 && (lid0.localScale.x <= 0.99 || lid1.localScale.x <= 0.99))
        {
			//禁止用户输入
			GameObject.Find("Balls").GetComponent<AddForces>().canMove = false;

			CloseLid();
		}
		//工作计时器开始计时
		if (state == 1)
        {
			workTimer -= Time.deltaTime;
        }
		//工作计时器计时结束
		if (state == 1 && workTimer <= 0)
		{
			Convert();
			state = 2;
        }
		//工作结束开盖
		if (state == 2)
		{
			OpenLid();
			if (lid0.localScale.x <= 0.01 && lid1.localScale.x <= 0.01)
			{
				state = 3;
			}
		}
		//底部上升
		if (state == 3)
        {
			BottomRise();
			if(bottom.localPosition.y >= 0.44)
            {
				state = 4;
				cdTimer = cdTime;
			}
        }
		//cd开始关盖
		if (state == 4)
		{
			CloseLid();
			if (lid0.localScale.x >= 0.99 && lid1.localScale.x >= 0.99)
			{
				//允许用户输入
				GameObject.Find("Balls").GetComponent<AddForces>().canMove = true;

				state = 5;
			}
		}
		//cd计时器开始计时
		if (state == 5)
        {
			cdTimer -= Time.deltaTime;
        }
		//cd计时器计时结束
		if (state == 5 && cdTimer <= 0)
        {
			BottomDrop();
			if(bottom.localPosition.y <= -0.55f)
            {
				state = 6;
            }
		}
		//cd结束开盖
		if (state == 6)
        {
			OpenLid();
			if (lid0.localScale.x <= 0.01 && lid1.localScale.x <= 0.01)
			{
				state = 0;
			}
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (state == 0)
		{
			state = 1;
			workTimer = duration;
			ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
		}
	}

	//关闭盖子
	void CloseLid()
	{
		//lid0
		Vector3 lid0Scale = lid0.localScale;
		lid0Scale.x += (1 - lid0Scale.x) * lidSpeed;
		lid0.localScale = lid0Scale;
		//lid1
		Vector3 lid1Scale = lid1.localScale;
		lid1Scale.x += (1 - lid1Scale.x) * lidSpeed;
		lid1.localScale = lid1Scale;
	}
	//打开盖子
	void OpenLid()
    {
		//lid0
		Vector3 lid0Scale = lid0.localScale;
		lid0Scale.x -= lid0Scale.x * lidSpeed;
		lid0.localScale = lid0Scale;
		//lid1
		Vector3 lid1Scale = lid1.localScale;
		lid1Scale.x -= lid1Scale.x * lidSpeed;
		lid1.localScale = lid1Scale;
	}

	//底部上升
	void BottomRise()
    {
		Vector3 targetPosition = new Vector3(0, 0.45f, 0);
		bottom.localPosition = Vector3.Lerp(bottom.localPosition, targetPosition, bottomSpeed);
		//为球施加一个小的力，以更新球的状态
		ball.GetComponent<Rigidbody>().AddForce(0, 1, 0);
	}
	//底部下降
	void BottomDrop()
	{
		Vector3 targetPosition = new Vector3(0, -0.6f, 0);
		bottom.localPosition = Vector3.Lerp(bottom.localPosition, targetPosition, bottomSpeed);
	}

	//转换
	void Convert()
	{
		//保留旧球位置信息
		Vector3 lastPosition = ball.transform.position;
		//隐藏旧的球
		ball.SetActive(false);
		//球更新为新球
		ball = GameObject.Find("Balls").transform.Find("PaperBall").gameObject;
		//更改MainLogic的球
		GameObject.Find("MainNode").GetComponent<MainLogic>().SetBall(ball);
		//更改新球位置
		ball.transform.position = lastPosition;
		//新球设为可见
		ball.SetActive(true);
	}
}
