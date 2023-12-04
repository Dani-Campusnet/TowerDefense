using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : EnemyManager
{
    public override void Init(List<Vector2> corners, GameManager manager)
    {
        base.Init(corners, manager);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, corners[currentCorner], speed * Time.deltaTime);

        Vector2 dir = corners[currentCorner] - (Vector2)transform.localPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 900 * Time.deltaTime);

        if ((Vector2)transform.localPosition == corners[currentCorner])
        {
            currentCorner++;
            if (currentCorner >= corners.Count)
            {
                manager.DestroyEnemy(gameObject);
                PlayerValues.RemoveLifes(damage);
            }
        }
    }
}
