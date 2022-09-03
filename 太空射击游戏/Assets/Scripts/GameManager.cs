using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;//静态实例

    public Transform m_canvas_main;//显示分数的UI界面
    public Transform m_canvas_gameover;//游戏失败UI界面
    public Text m_text_score;//得分UI文字
    public Text m_text_best;//最高分UI文字
    public Text m_text_life;//生命UI文字

    protected int m_score = 0;//得分数值
    protected static int m_hiscore = 0;//最高分数值
    protected Player m_player;//主角实例

    public AudioClip m_musicClip;//背景音乐
    protected AudioSource m_Audio;//声音源

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_Audio=this.gameObject.AddComponent<AudioSource>();//使用代码添加声音源组件
        m_Audio.clip = m_musicClip;//指定背景音乐
        m_Audio.loop = true;//设置音乐循环播放
        m_Audio.Play();//开始播放音乐
        //通过tag查找主角
        m_player=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //获得UI控件
        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
       m_text_best = m_canvas_main.transform.Find("Text_best").GetComponent<Text>();
        m_text_life = m_canvas_main.transform.Find("Text_life").GetComponent<Text>();


        m_text_score.text = string.Format("分数  {0}", m_score);//初始化UI分数
        m_text_best.text = string.Format("最高分  {0}", m_hiscore);//初始化UI最高分
        m_text_life.text = string.Format("生命  {0}",m_player.m_life);//初始化UI生命值

        //获取重新开始游戏按钮
        var restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();
        restart_button.onClick.AddListener(delegate () {//设置重新开始游戏按钮事件回调
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新开始当前关卡
        });
        m_canvas_gameover.gameObject.SetActive(false);//默认隐藏游戏失败UI
    }

    //增加分数函数
    public void AddScore(int point)
    {
        m_score += point;
        //更新最高分记录
        if(m_hiscore < m_score)
        {
            m_hiscore = m_score;
        }
        m_text_score.text=string.Format("分数  {0}",m_score);
        m_text_best.text=string.Format("最高分  {0}",m_hiscore);
    }

    //改变生命值UI显示
    public void ChangeLife(int life)
    {
        m_text_life.text = string.Format("生命  {0}", life);//更新UI
        if(life <= 0)
        {
            m_canvas_gameover.gameObject.SetActive(true);//如果生命为0，显示游戏失败UI
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
