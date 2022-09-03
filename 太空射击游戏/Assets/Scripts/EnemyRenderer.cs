using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    public Enemy m_enemy;
    // Start is called before the first frame update
    void Start()
    {
        m_enemy=this.GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()//当模型进入屏幕
    {
        m_enemy.m_isActiv = true;
        m_enemy.m_renderer=this.GetComponent<Renderer>();//使Enemy获得Renderer
    }
}
