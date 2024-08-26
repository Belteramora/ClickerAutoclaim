using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClaimer : MonoBehaviour
{
    private float timer = 0;
    private Action<float> onClaimAction;

    public TMP_Text autoClaimRateText;
    public TMP_Text autoClaimRateMetricText;
    public AutoClaimerData autoClaimerData;

    public void Setup(Action<float> onClaimAction)
    {
        this.onClaimAction = onClaimAction;
    }

    // Update is called once per frame
    void Update()
    {
        //������ UI ������ ����� ����������, ����� ���� ����� ��������� SO � ��������
        autoClaimRateText.text = autoClaimerData.baseValue.ToString();
        autoClaimRateMetricText.text = "������/" + autoClaimerData.intervalInSeconds + " ���";

        timer += Time.deltaTime;

        if(timer > autoClaimerData.intervalInSeconds)
        {
            timer = 0;

            onClaimAction?.Invoke(autoClaimerData.baseValue);
        }
    }
}
