using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //����Ŀ��
    public GameObject ball;
    //�洢�����ָ��0=�ϣ�1=����2=����3=��
    public int camDirection = 0;
    //�����ٶȣ������ȣ�
    public float speed = 1f;
    //�����ƫ����
    Vector3 offset = new Vector3(0, 3, -6);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ball = GameObject.Find("MainNode").GetComponent<MainLogic>().ball;
        
        //�����ƽ������
        this.transform.position = Vector3.Lerp(this.transform.position,
            ball.transform.position + offset, Time.deltaTime * speed);
        rotateCam();
        camLookAt();
    }

    //�ֶ���ת�����
    void rotateCam()
    {
        //Debug.Log("camDirection" + camDirection + ", offset" + offset);

        //˳ʱ����ת
        if (Input.GetKeyDown("q"))
        {
            camDirection = (camDirection + 1) % 4;
            //�޸�offset��0=�ϣ�1=����2=����3=��
            if (camDirection == 0)
            {
                offset = new Vector3(0, 5, -6);
            }
            else if (camDirection == 1)
            {
                offset = new Vector3(-6, 5, 0);
            }
            else if (camDirection == 2)
            {
                offset = new Vector3(0, 5, 6);
            }
            else if (camDirection == 3)
            {
                offset = new Vector3(6, 5, 0);
            }
        }
        //��ʱ����ת
        if (Input.GetKeyDown("e"))
        {
            if (camDirection <= 0)
                camDirection = 3;
            else
                camDirection -= 1;
            //�޸�offset��0=�ϣ�1=����2=����3=��
            if (camDirection == 0)
            {
                offset = new Vector3(0, 5, -6);
            }
            else if (camDirection == 1)
            {
                offset = new Vector3(-6, 5, 0);
            }
            else if (camDirection == 2)
            {
                offset = new Vector3(0, 5, 6);
            }
            else if (camDirection == 3)
            {
                offset = new Vector3(6, 5, 0);
            }
        }
    }

    //�����ָ����
    void camLookAt()
    {
        //���������
        this.transform.LookAt(ball.transform.position);
    }

    void ChangeBall(GameObject ballToChange)
    {
        ball = ballToChange;
    }
}