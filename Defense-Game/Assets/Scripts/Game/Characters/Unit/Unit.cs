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
    }

    public override void Move()
    {
        animator.SetBool("IsWalking", true);
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }
    public void InflictDamage()
    {
        if (myTarget != null)
            myTarget.GetComponent<LoseHealthAndDie>().LoseHealth(attackPower);
    }

    //Lose Health
    public override void LoseHealth(int amount)
    {
        //health = health - amount
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    //Die
    public override void Die()
    {
        myTarget.GetComponent<LoseHealthAndDie>().DeadMessage();
        Destroy(gameObject);
    }
    public override void DeadMessage()
    {
        myTarget = null;
        isDetected = false;
        SetState(new Move(this));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDetected)
            return;

        if ((enemyMask & 1 << collision.gameObject.layer) != 0)
        {
            myTarget = collision.transform;
            isDetected = true;
        }
    }
}
