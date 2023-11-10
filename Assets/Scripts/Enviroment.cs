using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class Enviroment : MonoBehaviour
{
    [SerializeField] GameObject baseModelPrefab,baseParent;
    [SerializeField] int baseCount;
    public void CreateBase()
    {
       
        float zPos = 0;
        float xPos = 0;
        for (int i = 0; i < baseCount; i++)
        {
            GameObject baseModel = Instantiate(baseModelPrefab, transform.position, Quaternion.identity);
            baseModel.transform.parent = baseParent.transform;
            xPos += 100;
            if (i % 11==0)
            {
                zPos += 100;
                xPos = 0;
            }
            baseModel.transform.localPosition = new Vector3( xPos, 0, zPos);
        }
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(Enviroment))]
public class AddMeshesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Enviroment AddMeshestoObjectsManager = (Enviroment)target;
        if (GUILayout.Button("Create Base"))
        {
            AddMeshestoObjectsManager.CreateBase();
        }
    }
}
#endif
