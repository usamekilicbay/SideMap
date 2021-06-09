using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct LabyrinthMold
{
    public int gridDepth;
    public int gridWidth;
    public Vector3Int[] cells;
}

public class LabyrinthSaver : MonoBehaviour
{
    public static Action<string> CallSaveLabyrinth;

    private void Awake()
    {
        CallSaveLabyrinth += CreateLabyrinth;
    }

    private void OnDestroy()
    {
        CallSaveLabyrinth -= CreateLabyrinth;
    }

    private void CreateLabyrinth(string fileName)
    {
        LabyrinthMold labyrinth = new LabyrinthMold
        {
            gridDepth = GridGenerator.gridDepth,
            gridWidth = GridGenerator.gridWidth
        };

        labyrinth.cells = new Vector3Int[labyrinth.gridDepth * labyrinth.gridWidth];

        for (int i = 0; i < GridGenerator.cells.Count; i++)
        {
            if (GridGenerator.cells[i].height == 0) continue;

            labyrinth.cells[i] = new Vector3Int
            {
                x = GridGenerator.cells[i].coordinateX,
                y = GridGenerator.cells[i].height,
                z = GridGenerator.cells[i].coordinateZ
            };
        }

        SaveLabyrint(labyrinth, fileName);
    }

    public static void SaveLabyrint(LabyrinthMold labyrinth, string fileName)
    {
        string jsonString = JsonUtility.ToJson(labyrinth);
        File.WriteAllText($"{AssetPath.LABYRINTHDATA}{fileName}.json", jsonString);
    }
}
