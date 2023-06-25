using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : SaiMonoBehaviour
{
    [Header("Damage Sender")]
    public int dame = 1;


    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
        //this.createImpactFX();

    }
    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.deDuct(this.dame);
        //
    }
    //protected virtual void createImpactFX()
    //{
    //    string fxName = this.getImpactFX();
    //    Vector3 hitPos = transform.position;
    //    Quaternion hitRot = transform.rotation;
    //    Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
    //    fxImpact.gameObject.SetActive(true);
    //}
    //protected virtual string getImpactFX()
    //{
    //    return FXSpawner.ImpactOne;
    //}

}
