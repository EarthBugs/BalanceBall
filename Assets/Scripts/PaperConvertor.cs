using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaperConvertor : MonoBehaviour
{
	//����״̬��0=����������ڵȴ�����룬1=���ѽ������ڹظǣ�2=�ظ�������ڹ�����3=����������ڿ��ǣ�
	//4=���������������5=����������ڹظǣ�6=�ظ��������cd��7=cd������ڿ���
	public int state = 0;
	//����ʱ��
	public float duration = 2.5f;
	//������ʱ��
	float workTimer = 0f;
	//���¿���cdʱ��
	public float cdTime = 3.0f;
	//cd��ʱ��
	float cdTimer = 0;
	//���ظ��ٶ�
	public float lidSpeed = 0.1f;
	//�ײ������ٶ�
	public float bottomSpeed = 0.05f;

	Transform bottom;
	Transform lid0;
	Transform lid1;
	public GameObject ball;
	GameObject dropObject;

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

		//������ʼ�ظ�
		if (state == 1 && (lid0.localScale.x <= 0.99 || lid1.localScale.x <= 0.99))
		{
			CloseLid();
			if (lid0.localScale.x >= 0.99 && lid1.localScale.x >= 0.99)
            {
				state = 2;
            }
		}
		//������ʱ����ʼ��ʱ
		if (state == 2)
        {
			workTimer -= Time.deltaTime;

			//�жϵ��������
			bool isBall = false;
			Transform[] transforms = GameObject.Find("Balls").GetComponentsInChildren<Transform>();
			foreach (Transform transform in transforms)
			{
				if (transform.gameObject == dropObject)
                {
					isBall = true;
					break;
				}
			}
			//��������������ٴ����岢����convertor
			if (!isBall)
            {
				dropObject.SetActive(false);
				state = 7;
			}
			//���������
			else
            {
				//��ֹ�û�����
				GameObject.Find("Balls").GetComponent<AddForces>().canMove = false;
            }
		}
		//������ʱ����ʱ����
		if (state == 2 && workTimer <= 0)
		{
			Convert();
			state = 3;
        }
		//������������
		if (state == 3)
		{
			OpenLid();
			if (lid0.localScale.x <= 0.01 && lid1.localScale.x <= 0.01)
			{
				state = 4;
			}
		}
		//�ײ�����
		if (state == 4)
        {
			BottomRise();
			if(bottom.localPosition.y >= 0.44)
            {
				state = 5;
				cdTimer = cdTime;
			}
        }
		//cd��ʼ�ظ�
		if (state == 5)
		{
			CloseLid();
			if (lid0.localScale.x >= 0.99 && lid1.localScale.x >= 0.99)
			{
				//�����û�����
				GameObject.Find("Balls").GetComponent<AddForces>().canMove = true;

				state = 5;
			}
		}
		//cd��ʱ����ʼ��ʱ
		if (state == 6)
        {
			cdTimer -= Time.deltaTime;
        }
		//cd��ʱ����ʱ����
		if (state == 6 && cdTimer <= 0)
        {
			BottomDrop();
			if(bottom.localPosition.y <= -0.55f)
            {
				state = 7;
            }
		}
		//cd��������
		if (state == 7)
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
			dropObject = other.transform.gameObject;
		}
	}

	//�رո���
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
	//�򿪸���
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

	//�ײ�����
	void BottomRise()
    {
		Vector3 targetPosition = new Vector3(0, 0.45f, 0);
		bottom.localPosition = Vector3.Lerp(bottom.localPosition, targetPosition, bottomSpeed);
		//Ϊ��ʩ��һ��С�������Ը������״̬
		ball.GetComponent<Rigidbody>().AddForce(0, 1, 0);
	}
	//�ײ��½�
	void BottomDrop()
	{
		Vector3 targetPosition = new Vector3(0, -0.6f, 0);
		bottom.localPosition = Vector3.Lerp(bottom.localPosition, targetPosition, bottomSpeed);
	}

	//ת��
	void Convert()
	{
		//��������λ����Ϣ
		Vector3 lastPosition = ball.transform.position;
		//���ؾɵ���
		ball.SetActive(false);
		//�����Ϊ����
		ball = GameObject.Find("Balls").transform.Find("PaperBall").gameObject;
		//����MainLogic����
		GameObject.Find("MainNode").GetComponent<MainLogic>().SetBall(ball);
		//��������λ��
		ball.transform.position = lastPosition;
		//������Ϊ�ɼ�
		ball.SetActive(true);
	}
}