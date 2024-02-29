using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
    public PlayerStatsSO Stats => playerStats;

    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    public void ResetPlayer()
    {
        Stats.ResetPlayer();
        playerAnimations.ResetPlayer(Vector2.down);
    }
}
