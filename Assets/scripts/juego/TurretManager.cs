using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public int cost;
    public float range;
    public LayerMask detectMask;
    public SpriteRenderer areaRender;
    public Color areaColor;
    public GameObject evolve;
    protected GameManager manager;
    protected bool active;
    public virtual void InitTurret(GameManager manager)
    {
        this.manager = manager;
        active = true;
        areaRender.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
