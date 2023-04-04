using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, LoseHealthAndDie
{
    public int health;
    public int cost;
    private Vector3Int cellPosition;


    protected virtual void Start()
    {
        Debug.Log("BASE TOWER");
    }

    public virtual void Init(Vector3Int cellPos)
    {
        cellPosition = cellPos;
    }

    //Lose Health
    public void LoseHealth(int amount)
    {
        //health = health - amount
        health-= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    //Die
    public void Die()
    {
        Debug.Log("Tower is dead");
        FindObjectOfType<Spawner>().RevertCellState(cellPosition);
        Destroy(gameObject);
    }
}
