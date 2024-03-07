using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStatsSO stats;

    [Header("UI properties")]
    [SerializeField] private float valueUpdateRate = 10f;


    [Header("UI Elements - Images")]
    public Image healthBar;
    public Image manaBar;
    public Image expBar;

    [Header("UI Elements - HUD")]
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text expText;

    [Header("UI Elements - Stats")]
    public GameObject statsMenu;
    public TMP_Text statLevelText;
    public TMP_Text statDamageText;
    public TMP_Text statCChanceText;
    public TMP_Text statCDamageText;
    public TMP_Text statTotalExpText;
    public TMP_Text statExpText;
    public TMP_Text reqExpText;

    #region Mono Callbacks
    private void Awake()
    {
        statsMenu.SetActive(false);
    }
    private void Update()
    {
        UpdateUI();//ToDo:consider refactor to an event based architecture rather than update based
    } 
    #endregion

    #region Button Callbacks
    public void ToggleStatsMenu()
    {
        Debug.Log("Toggle stats menu");
        statsMenu.SetActive(!statsMenu.activeSelf);
        if(statsMenu.activeSelf )
        {
            UpdateStatMenuTexts();  
        }
    }
    #endregion

    #region Update Utility
    public void UpdateUI()
    {
        //Update image bars
        UpdateStatBars();

        //update texts
        UpdateHUDTexts();
    }
    private void UpdateHUDTexts()
    {
        levelText.text = $"Level {stats.Level}";
        healthText.text = $"{stats.Health}/{stats.MaxHealth}";
        manaText.text = $"{stats.Mana}/{stats.MaxMana}";
        expText.text = $"{stats.CurrentExp} / {stats.NextLevelExp}";
    }

    private void UpdateStatBars()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,
            stats.Health / stats.MaxHealth, valueUpdateRate * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.Mana / stats.MaxMana,
            valueUpdateRate * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp / stats.NextLevelExp,
            valueUpdateRate * Time.deltaTime);
    } 

    private void UpdateStatMenuTexts()
    {
        statLevelText.text = stats.Level.ToString();
        statDamageText.text = stats.TotalDamage.ToString();
        statCChanceText.text = stats.CriticalChance.ToString();
        statCDamageText.text = stats.CriticalDamage.ToString();
        statTotalExpText.text = stats.TotalExp.ToString();
        statExpText.text = stats.CurrentExp.ToString();
        reqExpText.text=stats.NextLevelExp.ToString();
    }
    #endregion
}
