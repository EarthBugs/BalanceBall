using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDropObject : MonoBehaviour
{
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
        if (other.gameObject == GameObject.Find("MainNode").GetComponent<MainLogic>().ball)
        {
            //SceneMgr
        }
        else
            other.gameObject.SetActive(false);
    }
}
