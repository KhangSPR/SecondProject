using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : AbilityObjectCtrl
{
    private Vector3Int cellPosition;
    protected override string GetObjectTypeString()
    {
        return ObjectType.Player.ToString();
    }
    public virtual void Init(Vector3Int cellPos)
    {
        cellPosition = cellPos;
    }
}
