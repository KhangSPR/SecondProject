using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender_CloseRange : SaiMonoBehaviour
{
    public Animator animator;
    Coroutine attackOrder;
    ShootAbleObjectDamageReceiver detectedTower;

    [SerializeField] protected float attackInterval = 3;
    [SerializeField] protected int attackPower =  2;

    IEnumerator Attack()
    {
        animator.Play("Attack", 0, 0);
        //Wait attackInterval 
        yield return new WaitForSeconds(attackInterval);
        //Attack Again
        attackOrder = StartCoroutine(Attack());
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (detectedTower)
            return;

        if (other.tag == "Tower")
        {
            detectedTower = other.GetComponent<ShootAbleObjectDamageReceiver>();
            attackOrder = StartCoroutine(Attack());
        }
    }
}
