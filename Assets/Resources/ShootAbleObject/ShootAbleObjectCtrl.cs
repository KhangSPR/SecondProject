using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShootAbleObjectCtrl : SaiMonoBehaviour
{

    [SerializeField] protected Transform modle;
    public Transform Modle { get => modle; }
    [SerializeField] protected Despawn deSpawn;
    public Despawn DeSpawn => deSpawn;
    [SerializeField] protected ShootAbleObjectSO shootAbleObjectSO;
    public ShootAbleObjectSO ShootAbleObjectSO => shootAbleObjectSO;
    public DamageSender DamageSender => damageSender;
    [SerializeField] protected DamageSender damageSender;
    [SerializeField] protected EnemyImpart enemyImpart;
    public EnemyImpart EnemyImpart => enemyImpart;
    [SerializeField] protected ShootAbleObjectDamageReceiver Receiver;
    public ShootAbleObjectDamageReceiver ShootAbleObjectDamageReceiver => Receiver;
    [SerializeField] protected ShipShootByDistance colider;
    public ShipShootByDistance ShipShootByDistance => colider;
    [SerializeField] protected Detect detect;
    public Detect Detect => detect;
    //[SerializeField] protected ObjShooting objShooting;
    //public ObjShooting ObjShooting => objShooting;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadShootAbleObjectSO();
        this.loadDamageSender();
        this.loadModle();
        this.loadReceiver();
        this.loadEnemyImpart();
        this.loadDespawn();
        this.loadColider();
        this.loadDetect();
    }
    protected virtual void loadDetect()
    {
        if (this.detect != null) return;
        this.detect = transform.GetComponentInChildren<Detect>();
        Debug.Log(gameObject.name + ": loadDetect" + gameObject);
    }
    protected virtual void loadColider()
    {
        if (this.colider != null) return;
        this.colider = transform.GetComponentInChildren<ShipShootByDistance>();
        Debug.Log(gameObject.name + ": loadObjShooting" + gameObject);
    }
    protected virtual void loadEnemyImpart()
    {
        if (this.enemyImpart != null) return;
        this.enemyImpart = transform.GetComponentInChildren<EnemyImpart>();
        Debug.Log(gameObject.name + ": loadEnemyImpart" + gameObject);
    }
    protected virtual void loadDespawn()
    {
        if (this.deSpawn != null) return;
        this.deSpawn = transform.GetComponentInChildren<Despawn>();
        Debug.Log(gameObject.name + ": loadDespawn" + gameObject);
    }
    protected virtual void loadReceiver()
    {
        if (this.Receiver != null) return;
        this.Receiver = transform.GetComponentInChildren<ShootAbleObjectDamageReceiver>();
        Debug.Log(gameObject.name + ": loadloadReceiver" + gameObject);
    }
    protected virtual void loadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = transform.GetComponentInChildren<DamageSender>();
        Debug.Log(gameObject.name + ": loadDamageSender" + gameObject);
    }
    protected virtual void loadModle()
    {
        if (this.modle != null) return;
        this.modle = transform.Find("Modle");
        Debug.Log(gameObject.name + ": loadModle" + gameObject);
    }
    protected virtual void loadShootAbleObjectSO() // ScriptableObject
    {
        if (this.shootAbleObjectSO != null) return;
        string resPath = "ShootAbleObject/" + this.GetObjectTypeString() + "/" + transform.name;
        this.shootAbleObjectSO = Resources.Load<ShootAbleObjectSO>(resPath); //Ph?i t?o Folder là Resources
        Debug.LogWarning(transform.name + ": LoadShootAbleObjectSO" + resPath, gameObject);
    }
    protected abstract string GetObjectTypeString();
}
