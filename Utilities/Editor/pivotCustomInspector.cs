using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(adjustPivot))]
public class pivotCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        adjustPivot aPivot = (adjustPivot)target;
        if(GUILayout.Button("Change!"))
        {
            aPivot.changePoint();
            Debug.Log("Done");
        }
    }
}
