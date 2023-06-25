using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootAbleObjectDamageReceiver : DamageReceiver
{
    [Header("ShootAbleObject")]
    [SerializeField] protected ShootAbleObjectCtrl shootAbleObjectCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadjunkctrl();
    }
    protected virtual void loadjunkctrl()
    {
        if (this.shootAbleObjectCtrl != null) return;
        this.shootAbleObjectCtrl = transform.parent.GetComponent<ShootAbleObjectCtrl>();
        Debug.Log(gameObject.name + ": loadjunkctrl" + gameObject);
    }
    public override void onDead()
    {

        //this.ondeadfx();//
        this.shootAbleObjectCtrl.DeSpawn.deSpawnObj();
        //this.ondeaddrop();//

    }
    public override bool IsDead()
    {
        return base.IsDead();
    }
    //protected virtual void ondeaddrop()
    //{
    //    Vector3 pos = transform.position;
    //    Quaternion rot = transform.rotation;
    //    ItemDropSpawner.Instance.Drop(this.shootAbleObjectCtrl.ShootAbleObjectSO.dropList, pos, rot);
    //}

    //protected virtual void ondeadfx()
    //{
    //    string fxname = this.getondeadfxname();
    //    Transform fxondead = FXSpawner.Instance.Spawn(fxname, transform.position, transform.rotation); //ham smoke bang ten
    //    fxondead.gameObject.SetActive(true);
    //}
    //protected virtual string getondeadfxname()
    //{
    //    return FXSpawner.SmokeOne;
    //}
    public override void ReBorn()
    {
        this.isMaxHP = this.shootAbleObjectCtrl.ShootAbleObjectSO.hpMax;
        base.ReBorn();
    }
}
