using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Health,AttackPower,MoveSpeed
    public int health, attackPower;
    public float moveSpeed;

    public Animator animator;
    public float attackInterval;
    Coroutine attackOrder;
    Tower detectedTower;

    private GameObject targetTile;

    private void Start()
    {
        initializeEnemy();
    }
    void Update()
    {
        checkPosition();
        moveEnemy();
        takeDamage(0);
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
    void Move()
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
        bool towerDied = detectedTower.LoseHealth(attackPower);

        if (towerDied)
        {
            detectedTower = null;
            StopCoroutine(attackOrder);
        }
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
        if (detectedTower)
            return;

        if (collision.tag == "Tower")
        {
            detectedTower = collision.GetComponent<Tower>();
            attackOrder = StartCoroutine(Attack());
        }
    }
}
