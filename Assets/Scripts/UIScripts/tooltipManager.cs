using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class tooltipManager : MonoBehaviour
{
    public TextMeshProUGUI textComp;
    public TextMeshProUGUI NameComp;
    public Vector3 offset = new Vector3(0, 50, 0);

    private string lastName;
    private string lastDesc;
    private float lastCost;
    private RectTransform lastRect;

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
        NameComp.text = lastName;
        textComp.text = lastDesc + "\n" + "Cost: " + lastCost.ToString();
        if(lastRect != null)
        transform.position = lastRect.position + offset;
    }


    public void SetandShow(string name, string description, float cost,RectTransform hoveredNode)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        NameComp.text = name;
        textComp.text = description + "\n" + "Cost: " +cost.ToString();
        transform.position = hoveredNode.position + offset;

        lastCost = cost;
        lastDesc = description;
        lastName = name;
        lastRect = hoveredNode;
    }
    
    public void hideTool ()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        textComp.text = string.Empty;
    }
}
