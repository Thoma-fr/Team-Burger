using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ToolTest : EditorWindow
{
    // Start is called before the first frame updat
    [MenuItem("Tools/ToolTest")]
    static void InitWindow()
    {
        ToolTest window = GetWindow<ToolTest>();
        window.titleContent = new GUIContent("ToolTest");
        window.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
