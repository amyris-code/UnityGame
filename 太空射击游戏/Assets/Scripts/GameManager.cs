using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//��̬ʵ��

    public Transform m_canvas_main;//��ʾ������UI����
    public Transform m_canvas_gameover;//��Ϸʧ��UI����
    public Text m_text_score;//�÷�UI����
    public Text m_text_best;//��߷�UI����
    public Text m_text_life;//����UI����

    protected int m_score = 0;//�÷���ֵ
    protected static int m_hiscore = 0;//��߷���ֵ
    protected Player m_player;//����ʵ��

    public AudioClip m_musicClip;//��������
    protected AudioSource m_Audio;//����Դ

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_Audio=this.gameObject.AddComponent<AudioSource>();//ʹ�ô����������Դ���
        m_Audio.clip = m_musicClip;//ָ����������
        m_Audio.loop = true;//��������ѭ������
        m_Audio.Play();//��ʼ��������
        //ͨ��tag��������
        m_player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //���UI�ؼ�
        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
       m_text_best = m_canvas_main.transform.Find("Text_best").GetComponent<Text>();
        m_text_life = m_canvas_main.transform.Find("Text_life").GetComponent<Text>();


        m_text_score.text = string.Format("����  {0}", m_score);//��ʼ��UI����
        m_text_best.text = string.Format("��߷�  {0}", m_hiscore);//��ʼ��UI��߷�
        m_text_life.text = string.Format("����  {0}",m_player.m_life);//��ʼ��UI����ֵ

        //��ȡ���¿�ʼ��Ϸ��ť
        var restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();
        restart_button.onClick.AddListener(delegate () {//�������¿�ʼ��Ϸ��ť�¼��ص�
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//���¿�ʼ��ǰ�ؿ�
        });
        m_canvas_gameover.gameObject.SetActive(false);//Ĭ��������Ϸʧ��UI
    }

    //���ӷ�������
    public void AddScore(int point)
    {
        m_score += point;
        //������߷ּ�¼
        if(m_hiscore < m_score)
        {
            m_hiscore = m_score;
        }
        m_text_score.text=string.Format("����  {0}",m_score);
        m_text_best.text=string.Format("��߷�  {0}",m_hiscore);
    }

    //�ı�����ֵUI��ʾ
    public void ChangeLife(int life)
    {
        m_text_life.text = string.Format("����  {0}", life);//����UI
        if(life <= 0)
        {
            m_canvas_gameover.gameObject.SetActive(true);//�������Ϊ0����ʾ��Ϸʧ��UI
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
