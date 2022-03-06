using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDropObject : MonoBehaviour
{
	int id = -1;
	bool gameOver = false;
	UIFade gameOverUI;

	// Update is called once per frame
	void Update()
	{
		if (gameOver && gameOverUI.state == -1)
			GameObject.Find("SceneMgr").GetComponent<SceneMgr>().RestartScene();
	}

	private void OnTriggerEnter(Collider other)
	{
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
						gameOverUI = uiFade;
				}
				gameOverUI.Show();
			}
            gameOver = true;
        }
        else
            other.gameObject.SetActive(false);
	}
}
