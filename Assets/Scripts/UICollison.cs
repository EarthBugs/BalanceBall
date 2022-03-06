using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollison : MonoBehaviour
{
	public int id;

	UIFade uiFade;

	private void OnTriggerEnter(Collider other)
	{
		//检查碰撞的是否是球
		if (other.gameObject == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
        {
			//查找对应id的物体
			Transform[] transforms = GameObject.Find("UI").GetComponentsInChildren<Transform>();
			if (transform != null)
			{
				foreach (Transform transform in transforms)
				{
					UIFade uiFade = transform.gameObject.GetComponent<UIFade>();
					if (uiFade == null)
						continue;
					else if (uiFade.id == id)
						this.uiFade = uiFade;
				}
				uiFade.Show();
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		uiFade.Hide();
	}
}
