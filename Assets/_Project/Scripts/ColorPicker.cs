using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IPointerEnterHandler 
{
    [SerializeField] private Image selectedColorIndicator;
    [SerializeField] private Image colormap;

    private void OnMouseOver()
    {
        PickColor(new Vector3
        {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = 0f
        });
    }

    public void PickColor(Vector3 mousePos)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Texture2D selectedTexture = colormap.mainTexture as Texture2D;
        selectedColorIndicator.color = selectedTexture.GetPixel((int)eventData.position .x, (int)eventData.position.y);

        print(eventData.position);
    }
}
