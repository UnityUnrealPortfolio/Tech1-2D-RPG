using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents any action Enemy can perform
/// e.g patrol, chase etc
/// </summary>
public abstract class FSMAction : MonoBehaviour
{
    public abstract void Act();//perform specific action
}
