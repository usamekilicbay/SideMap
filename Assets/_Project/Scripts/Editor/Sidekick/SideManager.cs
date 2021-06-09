using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Constants.GameObjectName;

public class SideManager : MonoBehaviour
{
    [MenuItem(MenuItemPath.SIDE_MANAGER + MenuItemPath.CREATE + "All Managers #F7")]
    private static void CreateAllManagers()
    {
        CreateEventManager(true);
        CreateGameManager(true);
    }


    #region EVENT
    [MenuItem(MenuItemPath.SIDE_MANAGER + MenuItemPath.CREATE + "Event Manager")]
    private static void CallCreateEventManager() => CreateEventManager();

    private static void CreateEventManager(bool bunchCall = false)
    {
        if (GameObject.Find(Manager.EVENT))
        {
            Warning.ManagerAlreadyExistErrorDisplay(Manager.EVENT, bunchCall);

            return;
        }

        GameObject eventManager = new GameObject(Manager.EVENT, typeof(EventManager));
        SetParent(eventManager);
    }
    #endregion

    #region GAME
    [MenuItem(MenuItemPath.SIDE_MANAGER + MenuItemPath.CREATE + "Game Manager")]
    private static void CallCreateGameManager() => CreateGameManager();

    private static void CreateGameManager(bool bunchCall = false)
    {
        if (GameObject.Find(Manager.GAME))
        {
            Warning.ManagerAlreadyExistErrorDisplay(Manager.GAME, bunchCall);

            return;
        }

        GameObject gameManager = new GameObject("Game Manager", typeof(GameManager));
        SetParent(gameManager);
    }
    #endregion

    private static void SetParent(GameObject manager)
    {
        manager.transform.SetParent(GetParent());
    }

    private static Transform GetParent()
    {
        GameObject managerParent = GameObject.Find("MANAGERS");

        return managerParent ? managerParent.transform : new GameObject("MANAGERS").transform;
    }
}
