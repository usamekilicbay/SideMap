using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool _isQuitting;

    public static void QuitControl(Action targetMethod)
    {
        if (_isQuitting)
        {
            return;
        }

        targetMethod();
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}
