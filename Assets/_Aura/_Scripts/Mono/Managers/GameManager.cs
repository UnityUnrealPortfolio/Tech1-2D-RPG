using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//ToDo:will refactor to scriptable object based architecture
{
    [SerializeField]private Player playerReferences;

    #region Singleton Setup
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//ToDo:Here for testing
        {
            playerReferences.ResetPlayer();
        }
    }

    public void AddPlayerExp(float _exp)
    {
        playerReferences.GetComponent<PlayerExp>().AddExp( _exp );
    }
}
