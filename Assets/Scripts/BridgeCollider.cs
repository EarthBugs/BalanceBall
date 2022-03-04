using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollider : MonoBehaviour
{
    //坠落延时计时器
    float dropTimer = 0;

    bool working = false;

    Queue<GameObject> dropQueue;

    // Start is called before the first frame update
    void Start()
    {
        dropQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
        {
            //坠落延时计时器开始计时
            dropTimer += Time.deltaTime;

            //此物体（碰撞箱）开始向桥移动
            this.transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
            //当延时计时器达到时间，从队列头取出物体，打开该物体重力影响
            if (dropTimer >= 0.5 && dropQueue.Count > 0)
                dropQueue.Dequeue().GetComponent<Rigidbody>().useGravity = true;

            //计时器达到一定时间时关闭该物体，减少运算量
            if (dropTimer >= 10)
                this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //如果碰撞物体是球，则开始工作，否则将该物体置入队列
        GameObject ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
        Debug.Log(other.gameObject.name);
        if (other.gameObject == ball)
            working = true;
        else
            dropQueue.Enqueue(other.gameObject);
    }
}
