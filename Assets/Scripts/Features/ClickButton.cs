using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public ClickData clickData;

    private Action<float> onClickAction;
    private Action<int> onEnergyCostSubstract;
    private Func<float> getAutoclaimerModifier;

    public void Setup(Action<float> onClickAction, Action<int> onEnergyCostSubstract, Func<float> getAdditionalModifiers)
    {
        this.onClickAction = onClickAction;
        this.onEnergyCostSubstract = onEnergyCostSubstract;
        this.getAutoclaimerModifier = getAdditionalModifiers;
    }

    public void OnClick()
    {
        if(clickData == null) return;

        if (!GameManager.Instance.HasEnergy()) return;

        onEnergyCostSubstract?.Invoke(clickData.onClickEnergyCost);
        onClickAction.Invoke(clickData.GetClickValue() + getAutoclaimerModifier.Invoke() * (clickData.autoClaimerValueAddPercent / 100f) / clickData.divider);
    }
}
