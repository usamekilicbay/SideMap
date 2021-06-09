using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject heightButtonPrefab;
    [Space(5)]
    [SerializeField] private Transform heightButtonsParent;
    [Space(15)]
    [SerializeField] private List<Color> colors;

    private static List<HeightButton> _heightButtons;
    public static HeightButton activeHeightButton;

    private void Awake()
    {
        Init();
        CreateWallHeightButtons();
    }

    private static void Init()
    {
        _heightButtons = new List<HeightButton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeActiveHeightButton(_heightButtons[0]);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeActiveHeightButton(_heightButtons[1]);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeActiveHeightButton(_heightButtons[2]);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeActiveHeightButton(_heightButtons[3]);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeActiveHeightButton(_heightButtons[4]);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeActiveHeightButton(_heightButtons[5]);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeActiveHeightButton(_heightButtons[6]);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeActiveHeightButton(_heightButtons[7]);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            ChangeActiveHeightButton(_heightButtons[8]);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            ChangeActiveHeightButton(_heightButtons[9]);
    }

    private void CreateWallHeightButtons()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            HeightButton newHeightButton = Instantiate(heightButtonPrefab, heightButtonsParent).GetComponent<HeightButton>();
            _heightButtons.Add(newHeightButton);
            newHeightButton.name = $"Heigth Button {i + 1}";
            newHeightButton.button.image.color = colors[i];
            newHeightButton.heightLevelText.SetText((i + 1).ToString());
            newHeightButton.height = i + 1;
            newHeightButton.color = colors[i];

            if (i != 0)
            {
                newHeightButton.IsChoosenOne = false;
                continue;
            }

            activeHeightButton = newHeightButton;
            newHeightButton.IsChoosenOne = true;
        }
    }

    public static void ChangeActiveHeightButton(HeightButton newActiveHeightButton)
    {
        activeHeightButton = newActiveHeightButton;

        foreach (HeightButton heightButton in _heightButtons)
            heightButton.IsChoosenOne = false;

        activeHeightButton.IsChoosenOne = true;
    }
}
