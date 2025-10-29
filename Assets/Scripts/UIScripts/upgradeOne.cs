using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class UpgradeOne : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    //Current upgrade
    public Button upgrade;

    //list of upgrades this will open up
    [SerializeField] private List<GameObject> nextUpgrades = new List<GameObject>();
    [SerializeField] private GameObject upgradeLines;
    [SerializeField] private UnityEvent upgradeEffect;
    [SerializeField] private float cost;

    [Header("Tooltip Content")]
    [SerializeField] private string title;
    [SerializeField] private string description;

    public bool obtained = false;
    public bool off = false;
    string currentUpgrade;

    void Start()
    {
        currentUpgrade = upgrade.transform.parent.gameObject.name;
    }

    public void clicked()
    {
        if(GameManager.instance.playerCoins >= cost)
        {
            //money!
            GameManager.instance.playerCoins -= cost;

            //sets upgrade to obtained, and pushes the effects
            obtained = true;
            GameManager.instance.upgrades.Add(currentUpgrade);
            upgradeEffect.Invoke();
        }
        //GameManager.instance.playerHealth += 100; //<--- this was me testing how to add to the player's health. this is how you access those variables
    }
    public void OnPointerEnter(PointerEventData pointerEventData)

    {

        tooltipManager._instance.SetandShow(title,description,cost);

    }



    public void OnPointerExit(PointerEventData pointerEventData)

    {

        tooltipManager._instance.hideTool();

    }


    // private void OnPointerEnter()
    // {
    //     print("mouse enetr");
    //     tooltipManager._instance.SetandShow(upgradeText);
    // }

    // private void OnMouseExit()
    // {
    //     tooltipManager._instance.hideTool();
    // }
    public void Update()
    {
        if (obtained && !off)
        {
            upgrade.GetComponent<Button>().interactable = false;
            off = true;
            if (nextUpgrades != null)
            {
                foreach (GameObject nextUpgrade in nextUpgrades)
                {
                    nextUpgrade.transform.Find("Button").gameObject.SetActive(true);
                }
            }

            if (upgradeLines != null)
                upgradeLines.SetActive(true);
        }
    }
}
