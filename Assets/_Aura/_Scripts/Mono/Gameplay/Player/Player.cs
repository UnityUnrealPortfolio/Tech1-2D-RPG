using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
    public PlayerStatsSO Stats => playerStats;

    private PlayerAnimations playerAnimations;

    #region Mono Callbacks
    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    private void OnEnable()
    {
        //playerStats.OnPlayerReset += HandleReset;
    }
    private void OnDisable()
    {
        //playerStats.OnPlayerReset -= HandleReset;
    } 
    #endregion
    public void ResetPlayer()
    {
        Stats.ResetPlayer();
        playerAnimations.ResetPlayer(Vector2.down);
   }
    private void HandleReset()
    {
        GetComponent<PlayerMovement>().enabled = true;
    }
}
