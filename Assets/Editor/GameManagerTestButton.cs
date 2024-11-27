using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager), true)]
public class GameManagerTestButton : Editor
{
    public override void OnInspectorGUI()
    {
        // 显示默认的 Inspector GUI
        DrawDefaultInspector();

        // 获取目标脚本
        GameManager example = (GameManager)target;

        // 添加一个按钮
        if (GUILayout.Button("Watch Mirror State"))
        {
            // 调用目标脚本的测试函数
            example.UpdatePlayerState(PlayerState.playerWatchMirror);
        }
        if (GUILayout.Button("Dont Watch Mirror State"))
        {
            example.UpdatePlayerState(PlayerState.playerDontWatchMirror);
        }
        if (GUILayout.Button("Player Selects Mop"))
        {
            example.UpdatePlayerState(PlayerState.playerSelectMop);
        }
        if (GUILayout.Button("Player Selects Tubelight"))
        {
            example.UpdatePlayerState(PlayerState.playerSelectTubelight);
        }
        if (GUILayout.Button("Player Selects Nothing"))
        {
            example.UpdatePlayerState(PlayerState.PlayerSelectNothing);
        }
    }
}
