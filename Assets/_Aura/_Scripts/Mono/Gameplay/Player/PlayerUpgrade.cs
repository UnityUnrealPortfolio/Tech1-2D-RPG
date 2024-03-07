using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    [Header("Stats Ref")]
    [SerializeField] private PlayerStatsSO stats;

    [Space(5)]
    [Header("Strength Upgrade Settings")]
    [SerializeField] private UpgradeSettings strengthUpgradeSettings;
    [Space(5)]
    [Header("Dexterity Upgrade Settings")]
    [SerializeField] private UpgradeSettings dexterityUpgradeSettings;
    [Space(5)]
    [Header("Intelligence Upgrade Settings")]
    [SerializeField] private UpgradeSettings intelligenceUpgradeSettings;

    public static event Action OnPlayerUpgradeEvent;
    private void OnEnable()
    {
        AttributeButton.OnAttributeSelected += HandleAttributeSelected;
    }


    private void OnDisable()
    {
        AttributeButton.OnAttributeSelected -= HandleAttributeSelected;

    }
    private void UpgradeAttribute(UpgradeSettings settings)
    {
        stats.BaseDamage += settings.DamageUpgrade;
        stats.TotalDamage += settings.DamageUpgrade;
        stats.MaxHealth += settings.HealthUpgrade;
        stats.Health = stats.MaxHealth;
        stats.MaxMana += settings.ManaUpgrade;
        stats.Mana = stats.MaxMana;
        stats.CriticalChance += settings.CChanceUpgrade;
        stats.CriticalDamage += settings.CDamageUpgrade;
    }

    private void HandleAttributeSelected(AttributeType _attributeType)
    {
        //do we have attribute points available for upgrade
        if (stats.attributePoints == 0) return;
       
        switch (_attributeType)
        {
            case AttributeType.Strength:
                UpgradeAttribute(strengthUpgradeSettings);
                stats.strength++;
                break;
            case AttributeType.Dexterity:
                UpgradeAttribute(dexterityUpgradeSettings);
                stats.dexterity++;
                break;
            case AttributeType.Intelligence:
                UpgradeAttribute(intelligenceUpgradeSettings);
                stats.intelligence++;
                break;
        }
        //We've used up our attribute points
        stats.attributePoints--;

        //notify interested listeners that we have upgraded (e.g UI)
        OnPlayerUpgradeEvent?.Invoke();
    }

}

[System.Serializable]
public class UpgradeSettings
{
    [Header("Setting name")]
    public string Name;

    [Header("Setting Values")]
    public float DamageUpgrade;
    public float HealthUpgrade;
    public float ManaUpgrade;
    public float CChanceUpgrade;
    public float CDamageUpgrade;
}
