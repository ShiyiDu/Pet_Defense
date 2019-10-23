using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;

// Drop this in some Editor folder
public static class AnimatorControllerTool
{
    [MenuItem("CONTEXT/RuntimeAnimatorController/Enforce Transition Settings")]
    public static void FixTransitions(MenuCommand command)
    {
        var path = AssetDatabase.GetAssetPath(command.context);
        path = Regex.Replace(path, "^Assets", Application.dataPath);
        AssetDatabase.StartAssetEditing();
        try {
            // We can't use serialization to read the properties, so we rely on editing the asset text file instead
            // This naturally requires asset files to be serialized as text
            // You can achieve this by Edit/Project Settings with Asset Serialization Mode set to Force Text
            var txt = File.ReadAllText(path);
            txt = Regex.Replace(txt, @"(?<param>m_HasExitTime:\s+)\d+", "${param}0");
            txt = Regex.Replace(txt, @"(?<param>m_HasFixedDuration:\s+)\d+", "${param}1");
            txt = Regex.Replace(txt, @"(?<param>m_TransitionDuration:\s+)[\.\d]+", "${param}0");
            txt = Regex.Replace(txt, @"(?<param>m_TransitionOffset:\s+)[\.\d]+", "${param}0");
            File.WriteAllText(path, txt);
        } catch (System.Exception e) { Debug.LogException(e); }
        AssetDatabase.StopAssetEditing();
        AssetDatabase.Refresh();
    }
}
