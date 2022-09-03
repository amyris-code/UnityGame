using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy
{
    public Transform m_rocket;
    protected float m_fireTimer = 2.0f;
    protected Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }

    protected override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if(m_fireTimer <= 0)
        {
            m_fireTimer = 2.0f;
            if(m_player != null && m_player.position.z < transform.position.z)
            {
                //使用向量减法获取朝向主角位置的方向
                Vector3 relativePos = m_player.position - transform.position;
                Instantiate(m_rocket, transform.position, Quaternion.LookRotation(relativePos));
            }
            else
            {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                if(obj!=null)
                {
                    m_player = obj.transform;
                }
            }
        }
        transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
