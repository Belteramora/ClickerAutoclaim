using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public GameObject energyDivisionPrefab;
    public RectTransform divisonsContent;
    public EnergyDivision[] energyDivisions;
    public void SetEnergy(int energyAmount)
    {
        var horLayout = divisonsContent.GetComponent<HorizontalLayoutGroup>();

        var divisionWidth = (divisonsContent.rect.width - divisonsContent.rect.width / 20) / energyAmount;

        energyDivisions = new EnergyDivision[energyAmount];
        for(int i = 0; i < energyDivisions.Length; i++)
        {
            energyDivisions[i] = Instantiate(energyDivisionPrefab, divisonsContent).GetComponent<EnergyDivision>();
            energyDivisions[i].rectTransform.sizeDelta = new Vector2(divisionWidth, energyDivisions[i].rectTransform.sizeDelta.y);
        }
    }

    public void UpdateEnergyAmount(int amount)
    {
        Debug.Log("Amount is " + amount);
        for (int i = 0; i < energyDivisions.Length; i++)
        {
            if (i < amount && energyDivisions[i].isSpended)
            {
                energyDivisions[i].OnAddEnergy();
            }
            else if (i >= amount && !energyDivisions[i].isSpended)
            {
                energyDivisions[i].OnSpendEnergy();
            }
        }
    }
}
