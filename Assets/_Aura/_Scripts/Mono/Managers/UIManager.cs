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

    [Header("UI Elements - TMP_Text")]
    public TMP_Text levelText;
    public TMP_Text healthText;
    public TMP_Text manaText;
    public TMP_Text expText;

    private void Update()
    {
        UpdateUI();//ToDo:consider refactor to an event based architecture rather than update based
    }
    public void UpdateUI()
    {
        //Update image bars
        UpdateStatBars();

        //update texts
        UpdateStatTexts();
    }

    #region Update Utility

    private void UpdateStatTexts()
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
    #endregion
}
