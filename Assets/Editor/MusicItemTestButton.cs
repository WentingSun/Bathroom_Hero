using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseMusicItem), true)]
[CanEditMultipleObjects]
public class MusicItemTestButton : Editor
{
    public override void OnInspectorGUI()
    {
        // 显示默认的 Inspector GUI
        DrawDefaultInspector();

        // 获取目标脚本
        BaseMusicItem example = (BaseMusicItem)target;

        // 添加一个按钮
        if (GUILayout.Button("UnSelected()"))
        {
            // 调用目标脚本的测试函数
            example.UnSelected();
        }
        if (GUILayout.Button("BeSelected()"))
        {
            example.BeSelected();
        }
    }
}

[CustomEditor(typeof(MopGuitar), true)]
public class MopGuitarTestButton : Editor
{
    public override void OnInspectorGUI()
    {
        // 显示默认的 Inspector GUI
        DrawDefaultInspector();

        // 获取目标脚本
        MopGuitar example = (MopGuitar)target;

         // 添加一个按钮
        if (GUILayout.Button("UnSelected()"))
        {
            // 调用目标脚本的测试函数
            example.UnSelected();
        }
        if (GUILayout.Button("BeSelected()"))
        {
            example.BeSelected();
        }

        // 添加一个按钮
        if (GUILayout.Button("Simulate Hand Move in"))
        {
            // 调用目标脚本的测试函数
            example.SimulateTriggerEnter();
        }
        
    }
}