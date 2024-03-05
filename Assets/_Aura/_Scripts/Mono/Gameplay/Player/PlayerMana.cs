using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;

    public float CurrentMana
    {
        get
        {
            return playerStats.Mana;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))//ToDo:here for testing only
        {
            UseMana(2f);
        }
    }
    public void UseMana(float _amount)
    {
        if (playerStats.Mana >= _amount)
        {
            playerStats.Mana = Mathf.Max(playerStats.Mana -= _amount, 0f);
        }
    
    }
}
