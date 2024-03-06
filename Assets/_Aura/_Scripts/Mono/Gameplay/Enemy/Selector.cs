using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("Selection Graphic")]
    [Tooltip("Graphic to show when selected")]
    [SerializeField] private GameObject selectionGraphic;

    #region Private fields
    private Animator selectionAnimator;
    private EnemyBrain thisEnemyBrain;
    #endregion

    #region Mono Callbacks
    private void Awake()
    {
        thisEnemyBrain = GetComponent<EnemyBrain>();
        selectionAnimator = selectionGraphic.GetComponent<Animator>();
        selectionGraphic.SetActive(false);
    }
    private void OnEnable()
    {
        //subscribe to SelectionManager events
        SelectionManager.OnEnemySelected += HandleEnemySelected;
        SelectionManager.OnNotEnemySelected += HandleNotEnemySelected;
    }

    private void OnDisable()
    {
        //Unsubscribe to SelectionManager events
        SelectionManager.OnEnemySelected -= HandleEnemySelected;
        SelectionManager.OnNotEnemySelected -= HandleNotEnemySelected;
    }
    #endregion

    #region Selection Callbacks
    private void HandleEnemySelected(EnemyBrain brain)
    {
       
        if (brain == thisEnemyBrain)
        {
            
            selectionGraphic.SetActive(true);
        }
        else
        {
            selectionGraphic.SetActive(false);
        }

    }

    private void HandleNotEnemySelected()
    {
        selectionGraphic.SetActive(false);
    }

    internal void DisableSelectionGraphic()
    {
        HandleNotEnemySelected();
    }
    #endregion

}
