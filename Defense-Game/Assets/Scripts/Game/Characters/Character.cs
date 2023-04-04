using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LoseHealth
{
    public void LoseHealth(int amount);
}
public interface CharacterDie
{
    public void Die();
}
public interface LoseHealthAndDie : CharacterDie, LoseHealth
{

}
public class Character : MonoBehaviour
{
    //Health,AttackPower,MoveSpeed
    public int health, attackPower;
    public float moveSpeed;

    public Animator animator;
    public float attackInterval;
    public LayerMask enemyMask;
    public bool isDetected;

    public Transform myTarget;
    public State state;

    public Character()
    {
    }
    public Character(State state)
    {
        this.state = state;
    }
    public void setState(State state) // setter 상태를 set
    {
        this.state = state;
    }
    public void act()  // getter 상대를 get. ⭐상태에 따른 행동을 알아서 한다.⭐
    {
        state.Action();
    }
    protected virtual void Move() { }
    public void ChangeState()
    {

    }
    public void StateProcess()
    {

    }
}
