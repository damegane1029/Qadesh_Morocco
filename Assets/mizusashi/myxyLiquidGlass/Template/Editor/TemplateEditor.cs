using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Template))]
public class TemplateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Template temp = target as Template;
        if (GUILayout.Button("Generate"))
        {
            temp.GenerateShapeTexture();
            AssetDatabase.Refresh();
        }
    }
}
