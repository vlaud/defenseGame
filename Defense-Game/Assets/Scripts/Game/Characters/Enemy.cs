using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    Coroutine attackOrder;

    private GameObject targetTile;

    private void Start()
    {
        //initializeEnemy();
    }
    void Update()
    {
        //checkPosition();
        //moveEnemy();
        //takeDamage(0);
        if (!isDetected) Move();
    }
    private void initializeEnemy()
    {
        targetTile = MapGenerator.Inst.startTile;
    }
    public void takeDamage(int amount)
    {
        health -= amount;
        if (health <= Mathf.Epsilon)
        {
            enemyDie();
        }
    }
    private void enemyDie()
    {
        Destroy(gameObject);
    }
    IEnumerator Attack()
    {
        animator.Play("Attack", 0, 0);
        //Wait attackInterval 
        yield return new WaitForSeconds(attackInterval);
        //Attack Again
        attackOrder = StartCoroutine(Attack());
    }

    //Moving forward
    protected override void Move()
    {
        animator.Play("Move");
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }
    private void moveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            targetTile.transform.position, moveSpeed * Time.deltaTime);
    }
    private void checkPosition()
    {
        if (targetTile != null && targetTile != MapGenerator.Inst.endTile)
        {
            float distance = (transform.position - targetTile.transform.position).magnitude;

            if (distance < Mathf.Epsilon)
            {
                int currentIndex = MapGenerator.Inst.pathTiles.IndexOf(targetTile);

                targetTile = MapGenerator.Inst.pathTiles[currentIndex + 1];
            }
        }
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
            attackOrder = StartCoroutine(Attack());
            isDetected = true;
        }
    }
}
