using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cellSprite;
    public int height;
    public int coordinateZ;
    public int coordinateX;

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            cellSprite.color = WallSettingsPanel.activeHeightButton.color;
            height = WallSettingsPanel.activeHeightButton.height;
        }
        else if (Input.GetMouseButton(1))
        {
            cellSprite.color = Color.white;
            height = 0;
        }
    }
}
