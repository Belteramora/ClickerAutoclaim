using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private RectTransform clickValueTextRect;
    private RectTransform autoClaimTextRect;
    private Vector2 originClickValueTextPosition;
    private Vector2 originAutoClaimTextPosition;


    [Header("Main Currency")]
    public TMP_Text mainCurrencyAmount;

    [Header("Click Parameters")]
    public ClickButton clickButton;
    public TMP_Text clickValueText;
    public float clickTextMoveDuration;
    public float clickValueShowDelay;
    public float clickValueHideDuration;

    [Header("Energy Bar")]
    public EnergyBar energyBar;

    [Header("AutoClaimer")]
    public AutoClaimer autoClaimer;
    public TMP_Text autoClaimText;
    public float autoClaimTextMoveDuration;
    public float autoClaimValueShowDelay;
    public float autoClaimValueHideDuration;


    // Start is called before the first frame update
    void Start()
    {
        //UI general setup
        GameManager.Instance.onMainCurrencyChanged += UpdateMainCurrency;
        GameManager.Instance.onEnergySpended += UpdateEnergyBar;

        //Setup click button
        Action<float> onClickAction = GameManager.Instance.ChangeMainCurrency;
        onClickAction += ShowClickValue;
        clickButton.Setup(onClickAction, GameManager.Instance.SpendEnergy, CalculateAdditionalModifiers);  
        
        //Setup click anim
        clickValueTextRect = clickValueText.GetComponent<RectTransform>();
        originClickValueTextPosition = clickValueTextRect.anchoredPosition;

        //Setup max energy
        energyBar.SetEnergy(GameManager.Instance.GetMaxEnergy());

        //Setup autoclaim
        Action<float> onAutoClaimAction = GameManager.Instance.ChangeMainCurrency;
        onAutoClaimAction += ShowAutoClaimValue;
        autoClaimer.Setup(onAutoClaimAction);

        //Setup autoclaim anim
        autoClaimTextRect = autoClaimText.GetComponent<RectTransform>();
        originAutoClaimTextPosition = autoClaimTextRect.anchoredPosition;

    }

    public void ShowClickValue(float value)
    {
        var sign = "";
        if (value > 0) sign = "+";
        else if(value < 0) sign = "-";

        clickValueText.text = sign + " " + value.ToString();

        Sequence seq = DOTween.Sequence();

        seq.OnStart(() =>
        {
            clickValueTextRect.anchoredPosition = originClickValueTextPosition;
        });

        seq.Append(clickValueTextRect.DOAnchorPosY(originClickValueTextPosition.y + 40f, autoClaimTextMoveDuration));
        seq.Join(clickValueText.DOFade(1, autoClaimTextMoveDuration));
        seq.AppendInterval(clickValueShowDelay);
        seq.Append(clickValueText.DOFade(0, clickValueHideDuration));

        seq.Play();
    }

    public void ShowAutoClaimValue(float value)
    {
        autoClaimText.text = "+ " + value.ToString();

        Sequence seq = DOTween.Sequence();

        seq.OnStart(() =>
        {
            autoClaimTextRect.anchoredPosition = originAutoClaimTextPosition;
        });

        seq.Append(autoClaimTextRect.DOAnchorPosY(originAutoClaimTextPosition.y - 20f, autoClaimTextMoveDuration));
        seq.Join(autoClaimText.DOFade(1, autoClaimTextMoveDuration));
        seq.AppendInterval(autoClaimValueShowDelay);
        seq.Append(autoClaimText.DOFade(0, autoClaimValueHideDuration));

        seq.Play();
    }

    public void UpdateMainCurrency(float mainCurrencyValue)
    {
        mainCurrencyAmount.text = mainCurrencyValue.ToString(); 
    }

    public void UpdateEnergyBar(int amount)
    {
        energyBar.UpdateEnergyAmount(amount);
    }

    public float CalculateAdditionalModifiers()
    {
        return autoClaimer.autoClaimerData.baseValue;
    }
}
