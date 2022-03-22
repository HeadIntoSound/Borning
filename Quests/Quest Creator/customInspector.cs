#if (UNITY_EDITOR) 
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(questCreator))]
public class customInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        questCreator qCreator = (questCreator)target;
        if(GUILayout.Button("Add Mission"))
        {
            qCreator.writeFile();
        }
        // if(GUILayout.Button("show missions"))
        // {
            
        // }
    }
}
#endif