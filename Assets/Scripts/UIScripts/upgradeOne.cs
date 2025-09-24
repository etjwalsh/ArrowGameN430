using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeOne : MonoBehaviour
{
    public Button upgrade;

    public void clicked()
    {
        upgrade.GetComponent<Button>().interactable = false;

        string currentUpgrade = upgrade.transform.parent.gameObject.name;


        print(currentUpgrade);
        string currentUpgradeNumberText = currentUpgrade.Replace("Upgrade", "");
        int currentUpgradeNumber = int.Parse(currentUpgradeNumberText);
        currentUpgradeNumber += 1;
        string nextUpgradeName = "Upgrade" + currentUpgradeNumber;
        string nextUpgradeLinesName = "Upgrade" + currentUpgradeNumber + "Lines";

        GameObject nextUpgrade = GameObject.Find(nextUpgradeName);
        GameObject nextUpgradeLines = GameObject.Find(nextUpgradeLinesName);

        if(nextUpgrade != null)
        {
            nextUpgradeLines.transform.GetChild(0).gameObject.SetActive(true);
            nextUpgrade.transform.GetChild(0).gameObject.SetActive(true);


            print(nextUpgradeName + " activated!");
        }
        else
        {
            print(nextUpgradeName + " not found!");
        }

    }
}
