using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarCB : MonoBehaviour
{
    public Image healthBar;

    public void SetHealth(float _health)
    {
        healthBar.fillAmount = _health;
    }
}
