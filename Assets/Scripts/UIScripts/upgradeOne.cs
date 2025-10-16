using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using JetBrains.Annotations;

public class UpgradeOne : MonoBehaviour
{
    public Button upgrade;

    public bool obtained = false;
    string currentUpgrade;

    void Start()
    {
        currentUpgrade = upgrade.transform.parent.gameObject.name;
    }

    public void clicked()
    {
        //GameManager.instance.playerHealth += 100; //<--- this was me testing how to add to the player's health. this is how you access those variables
        obtained = true;

        GameManager.instance.upgrades.Add(currentUpgrade);

    }

    public void Update()
    {
        if(obtained){
            upgrade.GetComponent<Button>().interactable = false;

            string currentUpgradeNumberText = currentUpgrade.Replace("Upgrade", "");
            int currentUpgradeNumber = int.Parse(currentUpgradeNumberText);
            currentUpgradeNumber += 1;
            string nextUpgradeName = "Upgrade" + currentUpgradeNumber;
            string nextUpgradeLinesName = "Upgrade" + currentUpgradeNumber + "Lines";

            GameObject nextUpgrade = GameObject.Find(nextUpgradeName);
            GameObject nextUpgradeLines = GameObject.Find(nextUpgradeLinesName);

            if (nextUpgrade != null)
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
}
