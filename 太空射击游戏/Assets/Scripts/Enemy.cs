using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{
    public float m_speed = 1;//移动速度
    public float m_life = 10;//生命值
    protected float m_rotSpeed = 30;//旋转速度

    internal Renderer m_renderer;//模型渲染组件
    internal bool m_isActiv = false; //是否激活

    public Transform m_explosionFX;

    //每消灭一个敌人获得的一定分数
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
        //左右移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        //前进(向Z轴负方向)
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PlayerRocket")//如果撞到主角子弹
        {
            Rocket rocket = other.GetComponent<Rocket>();//获得子弹上的Rocket脚本组件
            if (rocket != null)
            {
                m_life -= rocket.m_power;
                if(m_life <= 0)
                {
                    GameManager.Instance.AddScore(m_point);//添加代码，更新UI上的分数
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);//自我销毁
                }
                else
                {
                    print(gameObject.name+"'s life is "+m_life);
                }
            }
        }
        else if(other.tag=="Player")//如果撞到主角
        {
            m_life = 0;
            Instantiate(m_explosionFX,transform.position, Quaternion.identity);
            Destroy(this.gameObject);//自我销毁
        }
    }

    
}
