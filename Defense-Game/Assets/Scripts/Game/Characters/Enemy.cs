using System.Collections;
using UnityEngine;

public class Enemy : Character, LoseHealthAndDie
{
    private void Start()
    {
        SetState(new Move(this));
    }
    void Update()
    {
        currentState.Update();
    }
    //Moving forward
    public override void Move()
    {
        animator.Play("Move");
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    public override void DeadMessage()
    {
        myTarget = null;
        isDetected = false;
        SetState(new Move(this));
    }
   
    public void InflictDamage()
    {
        if(myTarget != null)
            myTarget.GetComponent<LoseHealthAndDie>().LoseHealth(attackPower);
    }

    //Lose health
    public void LoseHealth()
    {
        //Decrease health value
        health--;
        //Blink Red animation
        StartCoroutine(BlinkRed());
        //Check if health is zero => destroy enemy
        if (health <= 0)
            Destroy(gameObject);
    }
    public override void LoseHealth(int amount)
    {
        //health = health - amount
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        myTarget.GetComponent<LoseHealthAndDie>().RemoveAttacker(this);
        foreach (LoseHealthAndDie ib in myAttackers)
        {
            ib.DeadMessage();
        }
        Destroy(gameObject);
    }
    IEnumerator BlinkRed()
    {
        //Change the spriterendere color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //Wait for really small amount of time 
        yield return new WaitForSeconds(0.2f);
        //Revert to default color
        GetComponent<SpriteRenderer>().color = Color.white;
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
