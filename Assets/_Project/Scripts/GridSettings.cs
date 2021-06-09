using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GridSettings : MonoBehaviour
{
    [Header("Size Limit")]
    [SerializeField] private int widthMin;
    [SerializeField] private int widthMax;
    [Space(5)]
    [SerializeField] private int depthMin;
    [SerializeField] private int depthMax;
    [Space(15)]
    [Header("Space Limit")]
    [SerializeField] private float horizontalSpaceMin;
    [SerializeField] private float horizontalSpaceMax;
    [Space(5)]
    [SerializeField] private float verticalSpaceMin;
    [SerializeField] private float verticalSpaceMax;

    [Header("Grid Size")]
    [SerializeField] private Slider widthSlider;
    [SerializeField] private Toggle sizeConnectToggle;
    [SerializeField] private Slider depthSlider;
    [Space(5)]
    [SerializeField] private TMP_InputField widthInputField;
    [SerializeField] private TMP_InputField depthInputField;
    [Header("Grid Space")]
    [SerializeField] private Slider horizontalSpaceSlider;
    [SerializeField] private Toggle spaceConnectToggle;
    [SerializeField] private Slider verticalSpaceSlider;
    [Space(5)]
    [SerializeField] private TMP_InputField horizontalSpaceInputField;
    [SerializeField] private TMP_InputField verticalSpaceInputField;

    [Space(15)]
    [SerializeField] private TMP_InputField fileNameInputField;
    [SerializeField] private Button generateGridButton;
    [SerializeField] private Button saveLabyrinthButton;

    #region SIZE
    #region WIDTH
    private int _width;
    public int Width
    {
        get => _width;
        set
        {
            UpdateWidth(value);
        }
    }
    private void UpdateWidth(int value, bool isCalledFromOutside = false)
    {
        if (value < widthMin) value = widthMin;
        else if (value > widthMax) value = widthMax;

        _width = value;
        widthSlider.value = _width;
        widthInputField.text = _width.ToString();

        if (sizeConnectToggle.isOn && !isCalledFromOutside)
        {
            UpdateDepth(value, true);
            return;
        }

        if (Mathf.Abs(_width - Depth) > 90)
            Depth = _width > Depth ? _width - 90 : _width + 90;
    }
    #endregion

    #region DEPTH
    private int _depth;
    public int Depth
    {
        get => _depth;
        set
        {
            UpdateDepth(value);
        }
    }
    private void UpdateDepth(int value, bool isCalledFromOutside = false)
    {
        if (value < depthMin) value = depthMin;
        else if (value > depthMax) value = depthMax;

        _depth = value;
        depthSlider.value = _depth;
        depthInputField.text = _depth.ToString();

        if (sizeConnectToggle.isOn && !isCalledFromOutside)
        {
            UpdateWidth(value, true);
            return;
        }

        if (Mathf.Abs(_depth - Width) > 90)
            Width = _depth > Width ? _depth - 90 : _depth + 90;
    }
    #endregion
    #endregion

    #region SPACE
    #region HORIZONTAL
    private float _horizontalSpace;
    public float HorizontalSpace
    {
        //get => Mathf.Round(_horizontalSpace * 10) / 10;
        get => _horizontalSpace ;
        set
        {
            UpdateHorizontalSpace(value);
        }
    }
    private void UpdateHorizontalSpace(float value, bool isCalledFromOutside = false)
    {
        if (value < horizontalSpaceMin) value = horizontalSpaceMin;
        else if (value > horizontalSpaceMax) value = horizontalSpaceMax;

        _horizontalSpace = value;
        horizontalSpaceSlider.value = _horizontalSpace;
        horizontalSpaceInputField.text = _horizontalSpace.ToString("#.00");

        if (spaceConnectToggle.isOn && !isCalledFromOutside)
            UpdateVerticalSpace(value, true);
    }
    #endregion

    #region VERTICAL
    private float _verticalSpace;
    public float VerticalSpace
    {
        get => Mathf.Round(_verticalSpace * 10) / 10;
        set
        {
            UpdateVerticalSpace(value);
        }
    }
    private void UpdateVerticalSpace(float value, bool isCalledFromOutside = false)
    {
        if (value < verticalSpaceMin) value = verticalSpaceMin;
        else if (value > verticalSpaceMax) value = verticalSpaceMax;

        _verticalSpace = value;
        verticalSpaceSlider.value = _verticalSpace;
        verticalSpaceInputField.text = _verticalSpace.ToString("#.00");

        if (spaceConnectToggle.isOn && !isCalledFromOutside)
            UpdateHorizontalSpace(value, true);
    }
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        widthSlider.minValue = widthMin;
        widthSlider.maxValue = widthMax;
        widthSlider.value = widthMax;

        depthSlider.minValue = depthMin;
        depthSlider.maxValue = depthMax;
        depthSlider.value = depthMax;

        horizontalSpaceSlider.minValue = horizontalSpaceMin;
        horizontalSpaceSlider.maxValue = horizontalSpaceMax;
        horizontalSpaceSlider.value = horizontalSpaceMax;

        verticalSpaceSlider.minValue = verticalSpaceMin;
        verticalSpaceSlider.maxValue = verticalSpaceMax;
        verticalSpaceSlider.value = verticalSpaceMax;

        widthSlider.onValueChanged.AddListener(delegate { WidthSliderValueChanged(); });
        depthSlider.onValueChanged.AddListener(delegate { DepthSliderValueChanged(); });
        widthInputField.onEndEdit.AddListener(delegate { WidthInputFieldEndEdit(); });
        depthInputField.onEndEdit.AddListener(delegate { DepthInputFieldEndEdit(); });

        horizontalSpaceSlider.onValueChanged.AddListener(delegate { HorizontalSpaceSliderValueChanged(); });
        verticalSpaceSlider.onValueChanged.AddListener(delegate { VerticalSpaceSliderValueChanged(); });
        horizontalSpaceInputField.onEndEdit.AddListener(delegate { HorizontalSpaceInputFieldEndEdit(); });
        verticalSpaceInputField.onEndEdit.AddListener(delegate { VerticalSpaceInputFieldEndEdit(); });

        generateGridButton.onClick.AddListener(() => GenerateGrid());
        saveLabyrinthButton.onClick.AddListener(() => SaveLabyrinth());

        widthSlider.value = widthSlider.minValue;
        depthSlider.value = depthSlider.minValue;
        horizontalSpaceSlider.value = horizontalSpaceSlider.minValue;
        verticalSpaceSlider.value = verticalSpaceSlider.minValue;

        WidthSliderValueChanged();
        DepthSliderValueChanged();
        HorizontalSpaceSliderValueChanged();
        VerticalSpaceSliderValueChanged();
    }

    #region  SIZE
    private void WidthSliderValueChanged()
    {
        Width = (int)widthSlider.value;
    }

    private void WidthInputFieldEndEdit()
    {
        int.TryParse(widthInputField.text, out int inputValue);
        Width = inputValue;
    }

    private void DepthSliderValueChanged()
    {
        Depth = (int)depthSlider.value;
    }

    private void DepthInputFieldEndEdit()
    {
        int.TryParse(depthInputField.text, out int inputValue);
        Depth = inputValue;
    }
    #endregion

    #region  SPACE
    private void HorizontalSpaceSliderValueChanged()
    {
        HorizontalSpace = horizontalSpaceSlider.value;
    }

    private void HorizontalSpaceInputFieldEndEdit()
    {
        float.TryParse(horizontalSpaceInputField.text, out float inputValue);
        HorizontalSpace = inputValue;
    }

    private void VerticalSpaceSliderValueChanged()
    {
        HorizontalSpace = verticalSpaceSlider.value;
    }

    private void VerticalSpaceInputFieldEndEdit()
    {
        float.TryParse(verticalSpaceInputField.text, out float inputValue);
        VerticalSpace = inputValue;
    }
    #endregion

    private void GenerateGrid()
    {
        GridGenerator.CallGenerateGrid(new GridSettingsMold()
        {
            width = Width,
            depth = Depth,
            horizontalSpace = HorizontalSpace,
            verticalSpace = verticalSpaceSlider.value
        });
    }

    private void SaveLabyrinth()
    {
        string fileName = fileNameInputField.text;

        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("File Name can not be empty!");
            return;
        }

        LabyrinthSaver.CallSaveLabyrinth?.Invoke(fileNameInputField.text);
    }
}
