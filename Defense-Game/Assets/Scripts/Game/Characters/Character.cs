using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;

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
public interface LoseHealthAndDie : CharacterDie, LoseHealth, DeadAction
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
public class Character : MonoBehaviour
{
    //Health,AttackPower,MoveSpeed
    public int health, attackPower;
    public float moveSpeed;
    public float attackInterval;

    public bool isDetected;
    public LayerMask enemyMask;
 
    public Transform myTarget;
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
}