using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeOne : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    public Button upgradeButton;
    
    [Header("Tree Connections")]
    [SerializeField] private List<GameObject> nextUpgrades = new List<GameObject>();
    [SerializeField] private GameObject upgradeLines;

    [Header("Upgrade Settings")]
    [SerializeField] public string upgradeID; // Unique ID (e.g., "Dmg_Node_1")
    [SerializeField] private int maxLevel = 5;
    [SerializeField] private float baseCost = 100;
    [SerializeField] private float costMultiplier = 1.5f; // Cost scales up per level
    [SerializeField] private UnityEvent upgradeEffect;

    [Header("Tooltip Info")]
    [SerializeField] private string title;
    [TextArea] [SerializeField] private string description; 

    private int currentLevel = 0;
    private float currentCost;

    void Awake()
    {
        if (string.IsNullOrEmpty(upgradeID)) upgradeID = gameObject.name;
        upgradeButton = gameObject.transform.Find("Button").GetComponent<Button>();
    }

    public void clicked()
    {
        if (currentLevel >= maxLevel) return;

        if (GameManager.instance.playerCoins >= currentCost)
        {
            GameManager.instance.playerCoins -= currentCost;
            currentLevel++;

            GameManager.instance.SetUpgradeLevel(upgradeID, currentLevel); 
            upgradeEffect.Invoke();

            CalculateCurrentCost();
            UpdateVisuals();
            if (tooltipManager._instance.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                OnPointerEnter(null);
            }
        }
    }

    private void CalculateCurrentCost()
    {
        currentCost = baseCost * Mathf.Pow(costMultiplier, currentLevel);
    }

    private void UpdateVisuals()
    {
        if (currentLevel >= 1)
        {
            if (nextUpgrades != null)
            {
                foreach (GameObject nextUpgrade in nextUpgrades)
                {
                    Debug.Log("Unlocking next upgrade: " + nextUpgrade.GetComponent<UpgradeOne>().upgradeID);   
                    nextUpgrade.transform.Find("Button").gameObject.SetActive(true);
                }
            }

            if (upgradeLines != null)
                upgradeLines.SetActive(true);
        }

        if (currentLevel >= maxLevel)
        {
            upgradeButton.interactable = false;
            if (upgradeButton.image != null) upgradeButton.image.color = Color.green; 
        }
    }

    private string GetTooltipText()
    {
        return $"{description}\nLevel: {currentLevel}/{maxLevel}";
    }
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        tooltipManager._instance.SetandShow(title, GetTooltipText(), currentCost, GetComponent<RectTransform>());
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        tooltipManager._instance.hideTool();
    }
    public void SetupUpgradeState(int loadedLevel)
    {
        currentLevel = loadedLevel;
        CalculateCurrentCost();
        UpdateVisuals();
    }
}