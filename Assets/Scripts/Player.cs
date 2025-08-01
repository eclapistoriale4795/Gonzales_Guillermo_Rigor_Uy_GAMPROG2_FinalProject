using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
[Serializable]
public class Player : ScriptableObject
{
    [Header("Player Settings")]
    public float walkingSpeed;
    public float runningSpeed;
    public float jumpSpeed;
    public float gravity;
    [Header("Camera Settings")]
    public float lookSpeed;
    public float lookXLimit;
    [Header("Player Data")]
    public float health;
    public float stamina;
    //public float invisibility;

    [HideInInspector]
    public bool canMove;
    public bool isMoving;
}
