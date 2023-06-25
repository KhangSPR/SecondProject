using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletImpart : BulletAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected CircleCollider2D circleCollider2D;
    [SerializeField] protected Rigidbody2D _rigidbody;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigibody();
    }

    protected virtual void LoadCollider()
    {
        if (this.circleCollider2D != null) return;
        this.circleCollider2D = GetComponent<CircleCollider2D>();
        this.circleCollider2D.isTrigger = true;
        this.circleCollider2D.radius = 0.2f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent == this.bulletCtrl.Shooter) return;

        this.bulletCtrl.DamageSender.Send(other.transform);
        //this.CreateImpactFX(other);
    }


    /*
    protected virtual void CreateImpactFX(Collider other)
    {
        string fxName = this.GetImpactFX();

        Vector3 hitPos = transform.position;
        Quaternion hitRot = transform.rotation;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
        fxImpact.gameObject.SetActive(true);

        //fxImpact.parent = other.transform.parent;
        //Debug.LogError("stop");

        //Trung Nghia Nguyen
        //Vector3 dir = Vector3.Normalize(hitPos);
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion rotate = Quaternion.Euler(0, 0, angle + 90f);
        //fxImpact.rotation = rotate;
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.impact1;
    }*/
}