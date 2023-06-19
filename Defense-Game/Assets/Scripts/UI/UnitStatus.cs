using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitPosition
{
    Deal,
    Tank,
    Heal
}
[CreateAssetMenu(fileName = "UnitStatusData", menuName = "Resource Data/Unit Data")]
public class UnitStatus : ScriptableObject
{
    [SerializeField]
    private Sprite unitSprite;
    [SerializeField]
    private string unitName;
    [SerializeField]
    private string unitDescription;
    [SerializeField]
    private UnitPosition unitPosition;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float dp;
    [SerializeField]
    private float ap;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float attackRange;
}
