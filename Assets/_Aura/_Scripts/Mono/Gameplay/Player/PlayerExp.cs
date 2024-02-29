using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] PlayerStatsSO playerStats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(300f);//ToDo:Here for testing
        }
    }
    public void AddExp(float _amount)
    {
        playerStats.CurrentExp += _amount;

        //exp will add up until eventually it's enough to match what's needed to go to next Level
        while(playerStats.CurrentExp >= playerStats.NextLevelExp)
        {
            //use up the current experience to upgrade
            playerStats.CurrentExp -= playerStats.NextLevelExp;

            //update what's needed to get to new next level
            UpdateToNextLevel();
        }
    }

    private void UpdateToNextLevel()
    {
        //Player now graduates to the next level
        playerStats.Level++;

        //calculate what it will take to get to new next level
        float newNextLevel = Mathf.Round(playerStats.NextLevelExp + 
            playerStats.NextLevelExp * (playerStats.ExpMultiplier / 100f));

        playerStats.NextLevelExp = newNextLevel;
    }
}
