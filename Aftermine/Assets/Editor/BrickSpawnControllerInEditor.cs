using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BrickSpawnController))]
public class BrickSpawnControllerInEditor : Editor
{    
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Rebuild"))
        {
            var script = (BrickSpawnController)target;
            script.Rebuild();
        }

        DrawDefaultInspector();
    }
}
