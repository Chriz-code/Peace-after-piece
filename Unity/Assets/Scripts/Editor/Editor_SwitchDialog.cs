using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SwitchDialog))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SwitchDialog myScript = (SwitchDialog)target;
        if (GUILayout.Button("Inherit Values"))
        {
            myScript.InheritValues();
        }
    }
}
