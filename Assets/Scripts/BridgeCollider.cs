using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollider : MonoBehaviour
{
    //׹����ʱ��ʱ��
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
            //׹����ʱ��ʱ����ʼ��ʱ
            dropTimer += Time.deltaTime;

            //�����壨��ײ�䣩��ʼ�����ƶ�
            this.transform.position += new Vector3(5 * Time.deltaTime, 0, 0);
            //����ʱ��ʱ���ﵽʱ�䣬�Ӷ���ͷȡ�����壬�򿪸���������Ӱ��
            if (dropTimer >= 0.5 && dropQueue.Count > 0)
                dropQueue.Dequeue().GetComponent<Rigidbody>().useGravity = true;

            //��ʱ���ﵽһ��ʱ��ʱ�رո����壬����������
            if (dropTimer >= 10)
                this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����ײ����������ʼ���������򽫸������������
        GameObject ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
        Debug.Log(other.gameObject.name);
        if (other.gameObject == ball)
            working = true;
        else
            dropQueue.Enqueue(other.gameObject);
    }
}
