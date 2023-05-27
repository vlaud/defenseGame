using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatusData", menuName = "Resource Data/Unit Data")]
public class UnitStatus : ScriptableObject
{
    [SerializeField]
    private Sprite unitSprite;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float ap;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float dp;
    [SerializeField]
    private float attackRange;

}
