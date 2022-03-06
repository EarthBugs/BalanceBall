using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
	public int id = -1;

	//状态，0=等待第一次显示，1=正在渐入，2=正在计时，3=正在渐出，-1=已显示过
	public int state = 0;

	//目标显示时长
	public float duration = 2.0f;
	//计时器
	float timer = 0;

	//目标透明度
	float targetAlpha = 1;
	//透明度变化速度
	public float alphaSpeed = 2.0f;
	//当前对象的CanvasGroup组件
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
		
		//渐入
		if (state == 1)
		{
			FadeIn();
			if (canvasGroup.alpha == 1)
				state = 2;
		}
		//开始cd
		if (state == 2)
		{
			//时长为-1，则一直显示
			if (duration != -1)
            {
				timer += Time.deltaTime;
				if (timer >= duration)
					state = 3;
            }
		}
		//渐出
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
		//防止重复显示
		if (state != -1)
			state = 1;
	}
	public void Hide()
	{
		//判断是否是终点，时长为-1时一般是终点，此时不隐藏
		if (duration != -1)
        {
			//若被调用，则应该是物体离开了碰撞箱，应当加快渐出速度以免与下一个UI重合
			alphaSpeed = 10;
			state = 3;
		}			
	}

	void FadeIn()
	{
		if (canvasGroup.alpha != targetAlpha)
		{
			//渐入显示
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, alphaSpeed * Time.deltaTime);
			if (canvasGroup.alpha >= 0.99f)
				canvasGroup.alpha = targetAlpha;
		}
	}
	void FadeOut()
	{
		if (canvasGroup.alpha != 0)
		{
			//渐出显示
			canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, alphaSpeed * Time.deltaTime);
			if (Mathf.Abs(targetAlpha - canvasGroup.alpha) <= 0.01f)
				canvasGroup.alpha = 0;
		}
	}
}
