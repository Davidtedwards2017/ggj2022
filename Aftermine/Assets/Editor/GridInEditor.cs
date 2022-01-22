using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid))]
public class GridInEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Rebuild"))
        {
            var script = (Grid) target;
            script.Rebuild();
        }

        DrawDefaultInspector();
    }

}
