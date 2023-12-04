using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class turret0 : TurretManager
{
    private EnemyManager currentEnemy;
    float fireRate;
    public float rateTime;
    public int damage;


    private void Start()
    {
        areaRender.color = areaColor;
        areaRender.transform.localScale = new Vector3(range,range,1);
    }
    private void Update()
    {
        if (active)
        { 
            if (currentEnemy == null)
            {
                EnemyDetect();
            }
            else
            {
                AttackEnemy();
            }
        }
    }
    private void OnMouseEnter()
    {
        areaRender.enabled = true;
    }
    private void OnMouseExit()
    {
        areaRender.enabled = false;
    }
    private void OnMouseDown()
    {
        if (evolve!=null)
        {
            if (evolve.GetComponent<TurretManager>().cost<=PlayerValues.coins)
            {
                manager.GetTurretEvolve(this);
            }
        }
    }
    void EnemyDetect()
    {
        Collider2D[] detect= Physics2D.OverlapCircleAll(transform.position,range, detectMask);
        if (detect.Length>0)
        {
            float distance = Mathf.Infinity;
            EnemyManager tempEnemy = null;
            for (int i = 0; i < detect.Length; i++)
            {
                if (Vector2.Distance(transform.position, detect[i].transform.position) < distance)
                {
                    distance = Vector2.Distance(transform.position, detect[i].transform.position);
                    tempEnemy = detect[i].GetComponent<EnemyManager>();
                }
            }
            currentEnemy = tempEnemy;
        }
    }
    void AttackEnemy()
    {
        Vector2 dir = currentEnemy.transform.localPosition - transform.localPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 900 * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentEnemy.transform.position) > range)
        {
            currentEnemy = null;
            return;
        }
        fireRate += Time.deltaTime;

        if(fireRate>=rateTime)
        {
            fireRate = 0;
            currentEnemy.GetDamage(damage);
        }
    }
}

