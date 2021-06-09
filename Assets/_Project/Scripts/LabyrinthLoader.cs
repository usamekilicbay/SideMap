using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LabyrinthLoader : MonoBehaviour
{
    [SerializeField] TextAsset textAsset;
    [SerializeField] Camera mainCam;
    public static Action CallLoadLabyrinth;

    private void Awake()
    {
        CallLoadLabyrinth += LoadLabyrinth;
    }

    private void OnDestroy()
    {
        CallLoadLabyrinth += LoadLabyrinth;
    }

    public void LoadLabyrinth()
    {
        GenerateLabyrinth(JsonUtility.FromJson<LabyrinthMold>(textAsset.ToString()));
    }

    private void GenerateLabyrinth(LabyrinthMold labyrinth)
    {
        mainCam.orthographicSize = labyrinth.gridWidth * labyrinth.gridDepth * .01f;

        Transform labyrinthParent = new GameObject().transform;
        labyrinthParent.localScale = Vector3.one;
        labyrinthParent.position = Vector3.zero;
        labyrinthParent.name = "Labyrinth";

        labyrinthParent.gameObject.AddComponent<LabyrinthRotator>();

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        MeshRenderer groundMeshRenderer = ground.GetComponent<MeshRenderer>();
        Bounds groundBounds = groundMeshRenderer.bounds;
        float groundHeight = groundBounds.size.y * .5f;
        ground.transform.localScale = new Vector3(labyrinth.gridWidth, 1f, labyrinth.gridDepth);
        ground.transform.position = Vector3.zero;
        ground.transform.SetParent(labyrinthParent);
        ground.name = "Ground";
        groundMeshRenderer.material.color = ColorCode.GetHexColor(ColorCode.DARK);

        for (int i = 0; i < labyrinth.cells.Length; i++)
        {
            Vector3Int cell = labyrinth.cells[i];

            if (cell.y == 0) continue;

            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            MeshRenderer wallMeshRenderer = wall.GetComponent<MeshRenderer>();
            Vector3 wallSize = wallMeshRenderer.bounds.size;

            Vector3 scale = new Vector3
            {
                x = 1f,
                y = cell.y,
                z = 1f
            };

            Vector3 pos = new Vector3
            {
                x = cell.x - labyrinth.gridWidth * .5f + wallSize.x * .5f,
                y = scale.y * wallSize.y * .5f + groundHeight,
                z = cell.z - labyrinth.gridDepth * .5f + wallSize.z * .5f
            };

            wall.transform.localScale = scale;
            wall.transform.position = pos;
            wall.transform.SetParent(labyrinthParent);
            wall.name = $"[{cell.z},{cell.x}]";

            wallMeshRenderer.material.color = cell.x == 0 || cell.x == labyrinth.gridWidth - 1 || cell.z == 0 || cell.z == labyrinth.gridDepth - 1
                ? ColorCode.GetHexColor(ColorCode.YELLOW)
                : ColorCode.GetHexColor(ColorCode.LIGHT_RED);
        }
    }

    //if (cell.x == 0 && cell.z == 0 ||  // X:0 - Z:0
    //    cell.x == labyrinth.gridWidth - 1 && cell.z == labyrinth.gridDepth - 1 || // X:9 - Z:9
    //    cell.x == 0 && cell.z == labyrinth.gridDepth - 1 || // X:0 - Z:9
    //    cell.z == 0 && cell.x == labyrinth.gridWidth - 1) // X:9 - Z:0
    //{
    //    CreateShape(wall);
    //}
    //else
    //{
    //    if (cell.x == 0 || cell.x == labyrinth.gridWidth - 1)
    //    {
    //        scale.x = .2f;

    //        pos.x += cell.x == 0
    //            ? groundBounds.min.x + scale.x * .5f
    //            : groundBounds.max.x - scale.x * .5f;
    //    }

    //    if (cell.z == 0 || cell.z == labyrinth.gridDepth - 1)
    //    {
    //        scale.z = .2f;

    //        pos.z += cell.z == 0
    //            ? groundBounds.min.z + scale.z * .5f
    //            : groundBounds.max.z - scale.z * .5f;
    //    }
    //}
}
