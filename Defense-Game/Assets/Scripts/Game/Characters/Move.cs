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
        character.transform.Translate(character.transform.right * character.moveSpeed * Time.deltaTime);
        //character.transform.position += character.transform.forward * character.moveSpeed * Time.deltaTime;

        // Switch to the walking state if the player stops running
      
    }

    public override void OnEnter()
    {
        // Set the player's animation to running
        character.animator.SetBool("IsWalking", true);
    }

    public override void OnExit()
    {
        // Reset the player's animation
        character.animator.SetBool("IsWalking", false);
    }
}
