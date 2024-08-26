using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDivision : MonoBehaviour
{
    private Color originColor;

    public RectTransform rectTransform;
    public Image image;

    public Color onSpendEnergyColor;
    public Color onAddEnergyColor;
    public float energyBlinkDuration;
    public int blinkLoops;

    public bool isSpended = false;

    public void Start()
    {
        originColor = image.color;
    }

    public void OnSpendEnergy()
    {
        isSpended = true;

        Blink(onSpendEnergyColor);

    }

    public void OnAddEnergy()
    {
        isSpended = false;

        Blink(onAddEnergyColor);
    }

    public void Blink(Color blinkColor)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(image.DOColor(blinkColor, energyBlinkDuration / 2));
        seq.Append(image.DOColor(originColor, energyBlinkDuration / 2));

        seq.SetLoops(blinkLoops);

        Sequence seq2 = DOTween.Sequence();

        seq2.Append(seq);
        seq2.Append(image.DOFade(0, 0.1f));

        seq.Play();
    }
}
