using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BulletDespawn : DespawnByTime
{
    public override void deSpawnObj()
    {
        BulletSpawner.Instance.DeSpawn(transform.parent);
    }
}
