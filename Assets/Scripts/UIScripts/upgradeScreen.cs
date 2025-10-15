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

        foreach(string upgrade in upgrades)
        {
            GameObject upgradeToToggle = GameObject.Find(upgrade);
            UpgradeOne script = upgradeToToggle.GetComponent<UpgradeOne>();
            script.obtained = true;

        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    

}
