using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private TMP_Text damageText;

    public void ShowDamageText(float _value)
    {
        damageText.text = _value.ToString();
    }
    public void DestroyDamageText()
    {
        Destroy(gameObject);
    }
}
