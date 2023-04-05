using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipReserche : MonoBehaviour
{
    public static TooltipReserche singelton;
    private Text _description;
    private Text tooltipTextAmmunition;
    private Text tooltipTextMaterial;
    private Text tooltipTextTime;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        singelton = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipTextAmmunition = transform.Find("TextAmm").GetComponent<Text>();
        tooltipTextMaterial = transform.Find("TextMat").GetComponent<Text>();
        tooltipTextTime = transform.Find("TextTime").GetComponent<Text>();
        _description = transform.Find("Description").GetComponent<Text>();
        HideToolTip();
    }
    public void ShowTooltip(string tooltipTextAmmunition, string tooltipTextMaterial, string tooltipTextTime, string descriptionb)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
        gameObject.SetActive(true);
        this.tooltipTextAmmunition.text = tooltipTextAmmunition;
        this.tooltipTextMaterial.text = tooltipTextMaterial;
        this.tooltipTextTime.text = tooltipTextTime;
        _description.text = descriptionb;
        /*float maxTextWidth = Mathf.Max(Mathf.Max(this.tooltipTextMaterial.preferredWidth, this.tooltipTextAmmunition.preferredWidth), Mathf.Max(_header.preferredWidth, _description.preferredWidth));
        backgroundRectTransform.sizeDelta = new Vector2 (maxTextWidth, backgroundRectTransform.sizeDelta.y);*/
    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
