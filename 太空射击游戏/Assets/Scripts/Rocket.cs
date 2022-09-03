using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Rocket")]
public class Rocket : MonoBehaviour
{

    public float m_speed = 10;//子弹飞行速度
    public float m_power = 1.0f;//威力

    protected void OnBecameInvisible()
    {
        if(this.enabled) //通过判断是否处于激活状态防止重复删除
        {
            Destroy(this.gameObject); //当离开屏幕后销毁
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Enemy")
        {
            return;
        }
        //Destroy(this.gameObject);
        Destroy(gameObject);
    }
}
