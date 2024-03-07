using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Placed as component of attribute upgrade button
/// exposes static event that alerts listeners of 
/// which AttributeType has been selected for upgrade
/// </summary>
public class AttributeButton : MonoBehaviour
{
  //static event that fires when attribtue is selected for upgrade
  public static event Action<AttributeType> OnAttributeSelected;

    [Header("Attribute Config")]
    [Tooltip("The attribute type this button represents")]
    [SerializeField]
    private AttributeType attributeType;

    #region Button Callback
    /// <summary>
    /// Called when button is clicked
    /// to upgrade attribute unique to this button
    /// </summary>
    public void UpgradeAttribute()
    {
        Debug.Log($"Inside attribute button: {attributeType.ToString()}");
        OnAttributeSelected?.Invoke(attributeType);
    }
    #endregion
}
