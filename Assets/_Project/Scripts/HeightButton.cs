using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeightButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI heightLevelText;
    [SerializeField] private Image checkmark;
    public Color color;
    public int height;

    [SerializeField] private bool isChoosenOne;
    public bool IsChoosenOne
    {
        get => isChoosenOne;
        set
        {
            isChoosenOne = value;
            checkmark.enabled = value;
        }
    }

    private void Awake()
    {
        button.onClick.AddListener(() => WallSettingsPanel.ChangeActiveHeightButton(this));
    }
}
