using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        List<string> upgrades = GameManager.instance.upgrades;

        foreach (string upgrade in upgrades)
        {
            GameObject upgradeToToggle = GameObject.Find(upgrade);
            UpgradeOne script = upgradeToToggle.GetComponent<UpgradeOne>();
            script.obtained = true;

        }


    }
    
    public void dmgUpgrade(float amount)
    {
        print(amount);
        GameManager.instance.playerDamage += amount;
    }

    public void hpUpgrade(float amount)
    {
        print(amount);
        GameManager.instance.playerHealth += amount;
    }

    public void coinAddUpgrade(float amount)
    {
        print(amount);
        GameManager.instance.moreCoins += amount;
    }

    public void atkSpdUpgrade(float amount)
    {
        print(amount);

        //need to implement elsewhere
        GameManager.instance.playerHealth += amount;
    }

    public void atkVeloUpgrade(float amount)
    {
        print(amount);

        //need to implement elsewhere
        GameManager.instance.playerHealth += amount;
    }
    // Update is called once per frame
    void Update()
    {

    }
    

}
