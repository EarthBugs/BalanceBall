using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    //�洢��ǰ����ʹ�õ���
    public GameObject ball;
    //����֡��
    public int frameRate = 60;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("WoodenBall");

        Application.targetFrameRate = frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
