using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{

    private static BulletSpawner instance;
    public static string bulletOne = "Bullet_1"; // bullet 1
    public static string bulletTwo = "Bullet_2"; // bullet 2
    public static BulletSpawner Instance { get => instance; }
    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance != null) Debug.LogError("Onlly 1 BulletSpawner Warning");
        BulletSpawner.instance = this;
    }
}
