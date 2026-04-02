using UnityEngine;
using UnityEditor;

using System.IO;

public class LuaInstance
{
    // Vídeo base na qual eu encontrei o tutorial: https://www.youtube.com/watch?v=3nTGOefc1AA

    private static readonly string defaultCode = "print(\"Angelo Says Hello!\")";

    [MenuItem("Assets/Create/Angelo's Lua Script", false, 80)]

    public static void CreateInstance()
    {
        var folder = "Assets";

        if (Selection.activeObject != null && AssetDatabase.Contains(Selection.activeObject))
        {
            folder = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (!AssetDatabase.IsValidFolder(folder))
            {
                folder = Path.GetDirectoryName(folder);

                if (string.IsNullOrEmpty(folder))
                    folder = "Assets";
            }
        }

        var fullPath = AssetDatabase.GenerateUniqueAssetPath(folder + "/AngeloLuaScript.lua");

        File.WriteAllText(fullPath, defaultCode);
        AssetDatabase.Refresh();

        var obj = AssetDatabase.LoadAssetAtPath<Object>(fullPath);

        Selection.activeObject = obj;

        EditorGUIUtility.PingObject(obj);
        EditorUtility.FocusProjectWindow();
        EditorGUIUtility.PingObject(obj);
    }
}