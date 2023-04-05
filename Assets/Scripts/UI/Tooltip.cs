using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{   
    public static Tooltip singelton;
    private Text tooltipTextAmmunition;
    private Text tooltipTextMaterial;
    private Text tooltipTextSolders;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        singelton = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipTextAmmunition = transform.Find("TextAmm").GetComponent<Text>();
        tooltipTextMaterial = transform.Find("TextMat").GetComponent<Text>();
        tooltipTextSolders = transform.Find("TextSol").GetComponent<Text>();
        
    }
    private void Start()
    {
        HideToolTip();
    }

    public virtual void ShowTooltip(string tooltipTextAmmunition, string tooltipTextMaterial, string tooltipTextSolders)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
        gameObject.SetActive(true);
        this.tooltipTextAmmunition.text = tooltipTextAmmunition;
        this.tooltipTextMaterial.text = tooltipTextMaterial;
        this.tooltipTextSolders.text = tooltipTextSolders;
        /*float maxTextWidth = Mathf.Max(this.tooltipTextAmmunition.preferredWidth, Mathf.Max(this.tooltipTextMaterial.preferredWidth, this.tooltipTextSolders.preferredWidth));
        Vector2 backgroundSize = new Vector2(maxTextWidth + 9f,2.8f*this.tooltipTextAmmunition.preferredHeight);
        backgroundRectTransform.sizeDelta = backgroundSize;*/
    }
  
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
