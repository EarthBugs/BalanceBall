using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
	public int id = -1;

	//״̬��0=�ȴ���һ����ʾ��1=���ڽ��룬2=���ڼ�ʱ��3=���ڽ�����-1=����ʾ��
	public int state = 0;

	//Ŀ����ʾʱ��
	public float duration = 2.0f;
	//��ʱ��
	float timer = 0;

	//Ŀ��͸����
	float targetAlpha = 1;
	//͸���ȱ仯�ٶ�
	public float alphaSpeed = 2.0f;
	//��ǰ�����CanvasGroup���
	private CanvasGroup canvasGroup;

	// Start is called before the first frame update
	void Start()
	{
		canvasGroup = this.GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (canvasGroup == null)
			return;
		
		//����
		if (state == 1)
		{
			FadeIn();
			if (canvasGroup.alpha == 1)
				state = 2;
		}
		//��ʼcd
		if (state == 2)
		{
			//ʱ��Ϊ-1����һֱ��ʾ
			if (duration != -1)
            {
				timer += Time.deltaTime;
				if (timer >= duration)
					state = 3;
            }
		}
		//����
		if (state == 3)
		{
			FadeOut();
			if (canvasGroup.alpha <= 0.01f)
			{
				state = -1;
				this.gameObject.SetActive(false);
			}
		}
	}

	public void Show()
	{
		//��ֹ�ظ���ʾ
		if (state != -1)
			state = 1;
	}
	public void Hide()
	{
		//�ж��Ƿ����յ㣬ʱ��Ϊ-1ʱһ�����յ㣬��ʱ������
		if (duration != -1)
        {
			//�������ã���Ӧ���������뿪����ײ�䣬Ӧ���ӿ콥���ٶ���������һ��UI�غ�
			alphaSpeed = 10;
			state = 3;
		}			
	}

	void FadeIn()
	{
		if (canvasGroup.alpha != targetAlpha)
		{
			//������ʾ
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, alphaSpeed * Time.deltaTime);
			if (canvasGroup.alpha >= 0.99f)
				canvasGroup.alpha = targetAlpha;
		}
	}
	void FadeOut()
	{
		if (canvasGroup.alpha != 0)
		{
			//������ʾ
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, alphaSpeed * Time.deltaTime);
			if (Mathf.Abs(targetAlpha - canvasGroup.alpha) <= 0.01f)
				canvasGroup.alpha = 0;
		}
	}
}
