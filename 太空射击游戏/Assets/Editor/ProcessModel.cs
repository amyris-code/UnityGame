using UnityEditor;
using UnityEngine;
class ProcessModel : AssetPostprocessor
{
    private void OnPostprocessModel(GameObject input)
    {
        //只处理名为"Enemy2b"的模型
        if (input.name != "Enemy2b")
        {
            return;
        }
        //取得导入模型的相关信息
        ModelImporter importer = assetImporter as ModelImporter;
        //将该模型从工程中读出来
        GameObject tar=AssetDatabase.LoadAssetAtPath<GameObject>(importer.assetPath);
        //将这个模型创建为Prefab
        GameObject prefab = PrefabUtility.CreatePrefab("Assets/Prefabs/Enemy2c.prefab", tar);
        //设置Prefab的tag
        prefab.tag = "Enemy";

        //查找碰撞模型
        foreach(Transform obj in prefab.GetComponentsInChildren<Transform>())
        {
            if(obj.name=="col")
            {
                //取消碰撞模型的显示
                MeshRenderer r= obj.GetComponent<MeshRenderer>();
                r.enabled = false;

                //添加Mesh 碰撞体
                if(obj.gameObject.GetComponent<MeshCollider>() == null)
                {
                    obj.gameObject.AddComponent<MeshCollider>();
                }

                //设置碰撞体的tag
                obj.tag = "Enemy";
            }
        }
        //设置刚体
        Rigidbody rigid=prefab.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;

        //为prefab添加声音组件
        prefab.AddComponent<AudioSource>();

        //获得子弹的Prefab
        GameObject rocket = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyRocket.prefab");

        //获得爆炸效果的Prefab
        GameObject fx = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FX/Explosion.prefab");

        //添加敌人脚本
        SuperEnemy enemy=prefab.AddComponent<SuperEnemy>();
        enemy.m_life = 50;
        enemy.m_point = 50;
        enemy.m_rocket = rocket.transform;
        enemy.m_explosionFX = fx.transform;
    }
   

}
