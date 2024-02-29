using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
    public PlayerStatsSO Stats => playerStats;
}
