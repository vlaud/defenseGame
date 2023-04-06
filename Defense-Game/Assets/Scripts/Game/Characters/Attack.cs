using UnityEngine;
public class Attack : ChracterState 
{
    public Attack(Character character) : base(character)
    {
        this.character = character;
    }
    public override void Update()
    {
        // Move the player forward with a faster speed

        // Switch to the walking state if the player stops running
        if (!character.isDetected) character.SetState(new Move(character));
    }

    public override void OnEnter()
    {
        // Set the player's animation to running
        character.animator.SetTrigger("Attack");

        character.OnAttack();
    }

    public override void OnExit()
    {
        // Reset the player's animation
        Debug.Log("StopAttack");
        character.OffAttack();
    }
}
