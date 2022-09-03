using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    //玩家移动速度
    public float m_speed = 1;
    //获得玩家位置组件
    Transform m_transform;
    //射击计时器
    float m_rocketTimer = 0;
    //主角生命
    public int m_life = 3;
    //子弹游戏物体
    public Transform m_rocket;
    //声音文件
    public AudioClip m_shootClip;
    //声音源
    protected AudioSource m_audio;
    //爆炸特效
    public Transform m_explosionFX;
    //目标位置
    protected Vector3 m_targetPos;
    //鼠标射线碰撞层
    public LayerMask m_inputMask;

    void Start()
    {
        m_transform = this.transform;
        m_audio = GetComponent<AudioSource>();//添加代码获取声音源组件
        m_targetPos = transform.position;//添加代码，初始化目标点位置
    }

    
    void Update()
    {
        //纵向移动距离
        float movev = 0;
        //水平移动距离
        float moveh = 0;

        //按上键Z轴正方向递增
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movev += m_speed * Time.deltaTime;
        }
        //按下键Z轴负方向递减
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movev -= m_speed * Time.deltaTime;
        }
        //按左键X轴负方向递减
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveh -= m_speed * Time.deltaTime;
        }
        //按左键X轴正方向递增
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveh += m_speed * Time.deltaTime;
        }

        //移动
        m_transform.Translate(new Vector3(moveh, 0, movev));

        m_rocketTimer -= Time.deltaTime;
        if(m_rocketTimer<=0)
        {
            m_rocketTimer = 0.1f;
            //按空格或鼠标左键发射子弹
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_rocket.rotation);

                //添加代码，播放一次射击声音
                m_audio.PlayOneShot(m_shootClip);
            }
        }
        MoveTo();
    }

    void MoveTo()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;//获得鼠标屏幕位置
            Ray ray=Camera.main.ScreenPointToRay(ms);//将屏幕位置转为射线
            RaycastHit hitinfo;//用来记录射线碰撞信息
            bool iscast=Physics.Raycast(ray,out hitinfo,1000,m_inputMask);
            if(iscast)
            {
                //如果射中目标，记录射线检测碰撞点
                m_targetPos = hitinfo.point;
            }
        }

        //使用Vector3提供的MouseTowards函数，获得朝目标移动的位置
        Vector3 pos=Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed*Time.deltaTime);
        //更新当前位置
        this.m_transform.position=pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag!="PlayerRocket")
        {
            m_life -= 1;
            GameManager.Instance.ChangeLife(m_life);
            if(m_life<=0)
            {
                Instantiate(m_explosionFX,m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
