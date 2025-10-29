using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class tooltipManager : MonoBehaviour
{
    public TextMeshProUGUI textComp;

    public static tooltipManager _instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }


    public void SetandShow(string name, string description, float cost)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        textComp.text = name + "\n" + description + "\n" + "Cost: " +cost.ToString();
    }
    
    public void hideTool ()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        textComp.text = string.Empty;
    }
}
