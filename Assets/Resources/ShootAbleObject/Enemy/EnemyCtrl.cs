using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : AbilityObjectCtrl
{
    private Vector3Int cellPosition;
    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }
    public virtual void Init(Vector3Int cellPos)
    {
        cellPosition = cellPos;
    }
}
