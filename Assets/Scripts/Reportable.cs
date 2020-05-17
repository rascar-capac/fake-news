using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Reportable : MonoBehaviour
{
    public bool IsReported => isReported;
    [SerializeField] private Color reportedColor = Color.gray;

    private bool isReported;
    private Button cardButton;
    private Color normalColor;

    public void ReportPost()
    {
        if(!isReported)
        {
            isReported = true;
            ColorBlock reportedColors = cardButton.colors;
            reportedColors.normalColor = reportedColor;
            reportedColors.selectedColor = reportedColor;
            cardButton.colors = reportedColors;
        }
        else
        {
            isReported = false;
            ColorBlock safeColors = cardButton.colors;
            safeColors.normalColor = normalColor;
            safeColors.selectedColor = normalColor;
            cardButton.colors = safeColors;
        }
    }

    private void Awake()
    {
        isReported = false;
        cardButton = GetComponent<Button>();
        normalColor = cardButton.colors.normalColor;
    }
}
