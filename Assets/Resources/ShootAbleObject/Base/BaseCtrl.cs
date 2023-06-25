using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCtrl : AbilityObjectCtrl
{
    protected override string GetObjectTypeString()
    {
        return ObjectType.Base.ToString();
    }
}