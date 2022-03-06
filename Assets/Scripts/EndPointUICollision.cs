using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointUICollision : MonoBehaviour
{
	public int id = -1;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
		{
			this.gameObject.GetComponent<Rigidbody>().useGravity = true;

			//≤È’“÷’µ„UI
			Transform[] transforms = GameObject.Find("UI").GetComponentsInChildren<Transform>();
			if (transform != null)
			{
				foreach (Transform transform in transforms)
				{
					UIFade uiFade = transform.gameObject.GetComponent<UIFade>();
					if (uiFade == null)
						continue;
					else if (uiFade.id == this.id)
						uiFade.Show();
				}
			}
		}
	}
}
