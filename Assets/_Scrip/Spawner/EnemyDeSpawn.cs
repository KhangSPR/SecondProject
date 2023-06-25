using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDeSpawn : DespawnByTime
{
    public override void deSpawnObj()
    {
        EnemySpawner.Instance.DeSpawn(transform.parent);
    }
}
