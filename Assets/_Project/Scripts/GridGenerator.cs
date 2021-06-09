using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridSettingsMold
{
    public int width;
    public int depth;
    public float horizontalSpace;
    public float verticalSpace;
}

public class GridGenerator : MonoBehaviour
{
    public static int gridWidth;
    public static int gridDepth;

    [Range(0f, 1f)]
    [SerializeField] private float generationDelay;

    [Space(15)]
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform grid;

    [Space(15)]
    public static List<Cell> cells;

    private float _cellWidth;
    private float _cellHeigth;

    public static Action<GridSettingsMold> CallGenerateGrid;

    private void Awake()
    {
        CallGenerateGrid += Init;
    }

    private void OnDestroy()
    {
        CallGenerateGrid -= Init;
    }

    private void Init(GridSettingsMold gridSettings)
    {
        ClearGrid();
        cells = new List<Cell>();

        gridWidth = gridSettings.width;
        gridDepth = gridSettings.depth;

        _cellWidth = cellPrefab.GetComponent<SpriteRenderer>().size.x + gridSettings.horizontalSpace;
        _cellHeigth = cellPrefab.GetComponent<SpriteRenderer>().size.y + gridSettings.verticalSpace;

        //mainCam.orthographicSize = (gridDepth * _cellHeigth * 0.5f + gridWidth * _cellWidth * 0.3f);
        mainCam.orthographicSize = gridDepth >= gridWidth
            ? gridDepth * _cellHeigth * 0.55f
            : gridDepth * _cellHeigth * 0.45f + gridWidth * _cellWidth * 0.45f;

        //generationDelay = gridDepth * gridWidth * 0.001f - gridDepth * gridWidth;

        StartCoroutine(GenerateGrid());
    }

    private IEnumerator GenerateGrid()
    {
        yield return new WaitUntil(() => cells.Count == 0);

        for (int z = 0; z < gridDepth; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Cell newCell = Instantiate(cellPrefab, grid).GetComponent<Cell>();
                newCell.transform.position = new Vector3(GetHorizontalPos(x), GetVerticalPos(z), 0);
                newCell.name = $"Cell [{x},{z}]";
                //newCell.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                newCell.coordinateX = x;
                newCell.coordinateZ = z;
                cells.Add(newCell);

                //yield return new WaitForSeconds(generationDelay);
            }
        }
    }

    private float GetHorizontalPos(int width)
    {
        return (_cellWidth * width - (gridWidth * _cellWidth * .5f - _cellWidth * .5f));
    }

    private float GetVerticalPos(int depth)
    {
        return (_cellHeigth * depth - (gridDepth * _cellHeigth * .5f - _cellHeigth * .5f));
    }

    private void ClearGrid()
    {
        if (cells == null) return;

        foreach (Cell cell in cells)
        {
            Destroy(cell.gameObject);
        }

        cells.Clear();
    }
}
