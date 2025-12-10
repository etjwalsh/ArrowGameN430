using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class upgradeScreen : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(wait());
    }

    IEnumerator wait()//waits one frame to ensure all UpgradeOne scripts have initialized
    {
        yield return null; 
        UpgradeOne[] allUpgradeNodes = FindObjectsOfType<UpgradeOne>();
        foreach (UpgradeOne node in allUpgradeNodes)
        {
            int savedLevel = GameManager.instance.GetUpgradeLevel(node.upgradeID);
            node.SetupUpgradeState(savedLevel);
        }
    }
    
    public void dmgUpgrade(float amount)
    {
        GameManager.instance.playerDamage += amount;
    }
    
    public void hpUpgrade(float amount)
    {
        GameManager.instance.playerMaxHealthPre += amount;
        GameManager.instance.maxPlayerHealth = GameManager.instance.playerMaxHealthPre * GameManager.instance.healthMult;
    }
    
    public void coinAddUpgrade(float totalAmount)
    {
        GameManager.instance.moreCoins += totalAmount;
    }

    public void atkSpdUpgrade(float totalAmount)
    {
        GameManager.instance.powerScale += totalAmount;
    }

    public void atkVeloUpgrade(float totalAmount)
    {
        GameManager.instance.maxPower += totalAmount;
    }
    public void multiShotUpgrade(int level)
    {
        GameManager.instance.multiShotLevel = level;
        GameManager.instance.maxArrows = 1 + level;
    }
    public void coinMultUpgrade(float multAmount)
    {
        GameManager.instance.coinMult += multAmount;
    }
    public void dmgMultUpgrade(float multAmount)
    {
        GameManager.instance.dmgMultiplier += multAmount;
    }
    public void defUpgrade(float amount)
    {
        GameManager.instance.defence += amount;
    }
    public void healthMultUpgrade(float amount)
    {
        GameManager.instance.healthMult += amount;
        GameManager.instance.maxPlayerHealth = GameManager.instance.playerMaxHealthPre * GameManager.instance.healthMult;
    }
}