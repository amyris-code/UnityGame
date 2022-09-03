using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    //����ƶ��ٶ�
    public float m_speed = 1;
    //������λ�����
    Transform m_transform;
    //�����ʱ��
    float m_rocketTimer = 0;
    //��������
    public int m_life = 3;
    //�ӵ���Ϸ����
    public Transform m_rocket;
    //�����ļ�
    public AudioClip m_shootClip;
    //����Դ
    protected AudioSource m_audio;
    //��ը��Ч
    public Transform m_explosionFX;
    //Ŀ��λ��
    protected Vector3 m_targetPos;
    //���������ײ��
    public LayerMask m_inputMask;

    void Start()
    {
        m_transform = this.transform;
        m_audio = GetComponent<AudioSource>();//��Ӵ����ȡ����Դ���
        m_targetPos = transform.position;//��Ӵ��룬��ʼ��Ŀ���λ��
    }

    
    void Update()
    {
        //�����ƶ�����
        float movev = 0;
        //ˮƽ�ƶ�����
        float moveh = 0;

        //���ϼ�Z�����������
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movev += m_speed * Time.deltaTime;
        }
        //���¼�Z�Ḻ����ݼ�
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movev -= m_speed * Time.deltaTime;
        }
        //�����X�Ḻ����ݼ�
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveh -= m_speed * Time.deltaTime;
        }
        //�����X�����������
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveh += m_speed * Time.deltaTime;
        }

        //�ƶ�
        m_transform.Translate(new Vector3(moveh, 0, movev));

        m_rocketTimer -= Time.deltaTime;
        if(m_rocketTimer<=0)
        {
            m_rocketTimer = 0.1f;
            //���ո�������������ӵ�
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Instantiate(m_rocket, m_transform.position, m_rocket.rotation);

                //��Ӵ��룬����һ���������
                m_audio.PlayOneShot(m_shootClip);
            }
        }
        MoveTo();
    }

    void MoveTo()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 ms = Input.mousePosition;//��������Ļλ��
            Ray ray=Camera.main.ScreenPointToRay(ms);//����Ļλ��תΪ����
            RaycastHit hitinfo;//������¼������ײ��Ϣ
            bool iscast=Physics.Raycast(ray,out hitinfo,1000,m_inputMask);
            if(iscast)
            {
                //�������Ŀ�꣬��¼���߼����ײ��
                m_targetPos = hitinfo.point;
            }
        }

        //ʹ��Vector3�ṩ��MouseTowards��������ó�Ŀ���ƶ���λ��
        Vector3 pos=Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed*Time.deltaTime);
        //���µ�ǰλ��
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
