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
public interface DeadAction
{
    public void DeadMessage();
}
public interface AddAttacker
{
    public void AddAttackers(LoseHealthAndDie target);
}
public interface LoseHealthAndDie : CharacterDie, LoseHealth, DeadAction, AddAttacker
{

}
public abstract class ChracterState
{
    protected Character character;

    public ChracterState(Character character)
    {
        this.character = character;
    }

    public virtual void Update()
    {
        // Default implementation of the Update method
    }

    public virtual void OnEnter()
    {
        // Default implementation of the OnEnter method
    }
    
    public virtual void OnExit()
    {
        // Default implementation of the OnExit method
    }
}
public class Character : MonoBehaviour, LoseHealthAndDie
{
    //Health,AttackPower,MoveSpeed
    public int health, attackPower;
    public float moveSpeed;
    public float attackInterval;

    public bool isDetected;
    public LayerMask enemyMask;

    protected List<LoseHealthAndDie> myAttackers = new List<LoseHealthAndDie>();

    Transform _target = null;
    public Transform myTarget
    {
        get => _target;
        set
        {
            _target = value;
            if (_target != null) _target.GetComponent<LoseHealthAndDie>()?.AddAttackers(this);
        }
    }
    public Animator animator;

    protected ChracterState currentState;
    protected Coroutine attackOrder;

    public virtual void Move() { }

    private void Start()
    {
        SetState(new Move(this));
    }
    private void Update()
    {
        // Update the current state
        currentState.Update();
    }
    public void SetState(ChracterState newState)
    {
        // Exit the current state
        if (currentState != null)
        {
            currentState.OnExit();
        }

        // Set the new state
        currentState = newState;

        // Enter the new state
        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }
    protected IEnumerator Attack()
    {
        while(myTarget != null)
        {
            // Set the player's animation to running
            animator.SetTrigger("Attack");

            Debug.Log("OnAttack");

            //Wait attackInterval 
            if (attackInterval <= Mathf.Epsilon) yield return new WaitForSeconds(1.0f);
            else yield return new WaitForSeconds(attackInterval);
            //Attack Again
        }
    }
    public void OnAttack()
    {
        StartCoroutine(Attack());
    }
    public void OffAttack()
    {
        StopCoroutine(Attack());
    }
    public virtual void LoseHealth(int amount) { }
    public virtual void Die() { }


    public virtual void DeadMessage() { }

    public virtual void AddAttackers(LoseHealthAndDie target) { }
}