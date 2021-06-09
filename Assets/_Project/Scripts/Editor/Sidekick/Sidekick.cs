using Constants;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Sidekick : MonoBehaviour
{
    /// <summary>
    /// % = Ctrl
    /// & = Alt
    /// # = Shift
    /// </summary>

    #region EDITOR

    [MenuItem("Sidekick/Editor/Clear Console %q")] // CTRL + Q
    private static void ClearConsole()
    {
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }


    [MenuItem("Sidekick/Editor/Delete All Player Prefs #F9")] // CTRL + U
    private static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }


    [MenuItem("Sidekick/Editor/Toggle Inspector Lock #F12")] // SHIFT + F12
    private static void ToggleInspectorLock()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        ActiveEditorTracker.sharedTracker.ForceRebuild();
    }

    #endregion

    #region CREATE

    #endregion
}
