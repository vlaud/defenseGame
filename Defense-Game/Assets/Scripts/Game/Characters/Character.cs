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

    protected virtual void Move() { }

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
}