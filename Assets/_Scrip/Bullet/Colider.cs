using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colider : EnemyAbstract
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.tag == "Tower")
        {
            Debug.Log("VACHAM");
            Transform towerTransform = collision.transform.parent.transform;
            this.enemyCtrl.ShipShootByDistance.SetTarget(towerTransform);
        }
    }
}
