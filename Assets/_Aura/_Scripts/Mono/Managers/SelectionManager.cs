using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    #region Selection Events
    public static Action<EnemyBrain> OnEnemySelected;
    public static Action OnNotEnemySelected;
    #endregion

    #region World Refs
    Camera mainCam;
    #endregion

    #region Mono Callbacks
    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        HandleSelectionAction();
    }
    #endregion

    #region Selection Utility
    private void HandleSelectionAction()
    {
        //Detect mouse left click
        if (Input.GetMouseButtonDown(0))
        {
            var worldMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            //cast ray into world from world pos of mouse pos
            //check for enemy layer
            var hitInfo = Physics2D.Raycast(worldMousePos, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemy"));

            if (hitInfo.collider != null)
            {
                //get enemy brain component
                var enemyBrain = hitInfo.collider.gameObject.GetComponent<EnemyBrain>();
                if (enemyBrain != null)
                {
                    //invoke OnEnemySelected event and pass the enemy component
                    OnEnemySelected?.Invoke(enemyBrain);
                }
            }
            //if no enemy hit invoke OnNotEnemySelected
            else
            {
                OnNotEnemySelected?.Invoke();
            }
        }




    } 
    #endregion
}
