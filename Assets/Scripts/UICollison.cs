using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollison : MonoBehaviour
{
	public int id;

	UIFade uiFade;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		//�����ײ���Ƿ�����
		if (other == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
        {
			//���Ҷ�Ӧid������
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
