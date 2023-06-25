using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : ParentFly
{
    protected override void loadValue()
    {
        base.loadValue();
        this.moveSpeed = 8f;
    }
}
