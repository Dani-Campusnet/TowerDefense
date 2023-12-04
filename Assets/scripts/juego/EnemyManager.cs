using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float life;
    public int damage;
    public int coins;
    
    protected List<Vector2> corners;
    protected GameManager manager;
    protected int currentCorner;
    public virtual void Init(List<Vector2> corners,GameManager manager)
    {
        this.corners = corners;
        this.manager = manager; 
        currentCorner =0;
        transform.localPosition = corners[0];
    }
    public virtual void GetDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            PlayerValues.AddCoins(coins);
            manager.DestroyEnemy(gameObject); 
            
        }
    }
}
