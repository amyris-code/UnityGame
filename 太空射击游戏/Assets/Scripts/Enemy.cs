using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{
    public float m_speed = 1;//�ƶ��ٶ�
    public float m_life = 10;//����ֵ
    protected float m_rotSpeed = 30;//��ת�ٶ�

    internal Renderer m_renderer;//ģ����Ⱦ���
    internal bool m_isActiv = false; //�Ƿ񼤻�

    public Transform m_explosionFX;

    //ÿ����һ�����˻�õ�һ������
    public int m_point = 10;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer= this.GetComponent<Renderer>();
    }

    private void OnBecameVisible()
    {
        m_isActiv = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        if(m_isActiv && !this.m_renderer.isVisible)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove()
    {
        //�����ƶ�
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        //ǰ��(��Z�Ḻ����)
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PlayerRocket")//���ײ�������ӵ�
        {
            Rocket rocket = other.GetComponent<Rocket>();//����ӵ��ϵ�Rocket�ű����
            if (rocket != null)
            {
                m_life -= rocket.m_power;
                if(m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);//��Ӵ��룬����UI�ϵķ���
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);//��������
                }
                else
                {
                    print(gameObject.name+"'s life is "+m_life);
                }
            }
        }
        else if(other.tag=="Player")//���ײ������
        {
            m_life = 0;
            Instantiate(m_explosionFX,transform.position, Quaternion.identity);
            Destroy(this.gameObject);//��������
        }
    }

    
}
