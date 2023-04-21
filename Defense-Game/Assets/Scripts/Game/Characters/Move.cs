using UnityEngine;

public class Move : ChracterState
{
    public Move(Character character) : base(character)
    {
        this.character = character;
    }
    public override void Update()
    {
        // Move the player forward with a faster speed
        character.Move();

        // Switch to the walking state if the player stops running
        if (character.isDetected) character.SetState(new Attack(character));
    }

    public override void OnEnter()
    {
        Debug.Log("OnMove");
        // Set the player's animation to running
        character.animator.SetBool("IsWalking", true);
    }

    public override void OnExit()
    {
        // Reset the player's animation
        character.animator.SetBool("IsWalking", false);
    }
}
