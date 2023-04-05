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
        character.transform.position += character.transform.forward * character.moveSpeed * Time.deltaTime;

        // Switch to the walking state if the player stops running

    }

    public override void OnEnter()
    {
        // Set the player's animation to running
        character.animator.SetTrigger("Attack");
    }

    public override void OnExit()
    {
        // Reset the player's animation
        character.animator.ResetTrigger("Attack");
    }
}
