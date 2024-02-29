using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStatsSO))]
public class PlayerStatsEditor : Editor
{
    private PlayerStatsSO StatsTarget
    {
        get
        {
            return target as PlayerStatsSO;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset Player"))
        {
            StatsTarget.ResetPlayer();
        }
    }
}
