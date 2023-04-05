using UnityEngine;

public class Unit : Character, LoseHealthAndDie
{
    public int cost;

    private void Start()
    {
        SetState(new Move(this));
    }
    // Update is called once per frame
    void Update()
    {
        currentState.Update();
        //if (!isDetected) Move();
    }

    protected override void Move()
    {
        animator.SetBool("IsWalking", true);
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }
    //Lose Health
    public void LoseHealth(int amount)
    {
        //health = health - amount
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    //Die
    public void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDetected)
            return;

        if ((enemyMask & 1 << collision.gameObject.layer) != 0)
        {
            myTarget = collision.transform;
            animator.SetBool("IsWalking", false);
            isDetected = true;
        }
    }
}
