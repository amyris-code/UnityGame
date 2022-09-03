using UnityEditor;
using UnityEngine;
class ProcessModel : AssetPostprocessor
{
    private void OnPostprocessModel(GameObject input)
    {
        //ֻ������Ϊ"Enemy2b"��ģ��
        if (input.name != "Enemy2b")
        {
            return;
        }
        //ȡ�õ���ģ�͵������Ϣ
        ModelImporter importer = assetImporter as ModelImporter;
        //����ģ�ʹӹ����ж�����
        GameObject tar=AssetDatabase.LoadAssetAtPath<GameObject>(importer.assetPath);
        //�����ģ�ʹ���ΪPrefab
        GameObject prefab = PrefabUtility.CreatePrefab("Assets/Prefabs/Enemy2c.prefab", tar);
        //����Prefab��tag
        prefab.tag = "Enemy";

        //������ײģ��
        foreach(Transform obj in prefab.GetComponentsInChildren<Transform>())
        {
            if(obj.name=="col")
            {
                //ȡ����ײģ�͵���ʾ
                MeshRenderer r= obj.GetComponent<MeshRenderer>();
                r.enabled = false;

                //���Mesh ��ײ��
                if(obj.gameObject.GetComponent<MeshCollider>() == null)
                {
                    obj.gameObject.AddComponent<MeshCollider>();
                }

                //������ײ���tag
                obj.tag = "Enemy";
            }
        }
        //���ø���
        Rigidbody rigid=prefab.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;

        //Ϊprefab����������
        prefab.AddComponent<AudioSource>();

        //����ӵ���Prefab
        GameObject rocket = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/EnemyRocket.prefab");

        //��ñ�ըЧ����Prefab
        GameObject fx = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FX/Explosion.prefab");

        //��ӵ��˽ű�
        SuperEnemy enemy=prefab.AddComponent<SuperEnemy>();
        enemy.m_life = 50;
        enemy.m_point = 50;
        enemy.m_rocket = rocket.transform;
        enemy.m_explosionFX = fx.transform;
    }
   

}
