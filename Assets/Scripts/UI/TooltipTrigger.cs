using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject someUiObject = eventData.pointerEnter;
        string str = someUiObject.tag;
        switch (str)
        {
            case "UIProduct":
                {
                    TurretBlueprint blueprint = someUiObject.GetComponent<TurretBlueprint>();
                    Tooltip.singelton.ShowTooltip(blueprint.costAmmunition.ToString(), blueprint.costMaterials.ToString(), blueprint.costRecruts.ToString());
                    break;
                }
            case "UIBufRes":
                {
                    CostUp cost = someUiObject.GetComponent<CostUp>();
                    Tooltip.singelton.ShowTooltip(cost.costAmmunition.ToString(), cost.costMaterials.ToString(), cost.costRecruts.ToString());
                    break;
                }
            case "UIReserche":
                {
                    Buff buff = someUiObject.GetComponent<Buff>();
                    TooltipReserche.singelton.ShowTooltip(buff.GetCostAmmunition().ToString(), buff.GetCostMaterial().ToString(),buff.GetTimeRate().ToString(), buff.GetDescription());
                    break;
                }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.singelton.HideToolTip();
        TooltipReserche.singelton.HideToolTip();
    }
}
