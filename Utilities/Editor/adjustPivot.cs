using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class adjustPivot : MonoBehaviour
{
    [Tooltip("Place the sprites you want to change the pivot point for")]
    [SerializeField] Sprite[] sprites;
    [Tooltip("Specify the new values for the pivot point")]
    [SerializeField] Vector2 pivotPoint;

    public void changePoint()
    {
        foreach (Sprite s in sprites)
        {
            // searches the path of the file
            string path = AssetDatabase.GetAssetPath(s);
            path += ".meta";
            path = path.Replace("Assets", Application.dataPath);

            // reads the file and writes the new data
            string metaFile = File.ReadAllText(path);

            // performs a search for the current aligment value and changes it  
            string al = "alignment: ";
            char aligmentValue = metaFile[metaFile.IndexOf("alignment: ") + al.Length];
            metaFile = metaFile.Replace("alignment: " + aligmentValue.ToString(), "alignment: 9");

            // performs a search for the current pivot point value and changes it
            int start = metaFile.IndexOf("spritePivot:");
            int end = metaFile.IndexOf('}', start);
            string toReplace = metaFile.Substring(start,end-start+1);
            string newValues = "spritePivot: {x: " + pivotPoint.x.ToString().Replace(',','.') + ", y: " + pivotPoint.y.ToString().Replace(',','.') + "}";
            metaFile = metaFile.Replace(toReplace, newValues);
            File.WriteAllText(path, metaFile);
        }
    }
}
