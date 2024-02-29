using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//ToDo:will refactor to scriptable object based architecture
{
    [SerializeField]Player playerReferences;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))//ToDo:Here for testing
        {
            playerReferences.ResetPlayer();
        }
    }
}
