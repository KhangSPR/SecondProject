using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : SaiMonoBehaviour
{

    private void FixedUpdate()
    {
        this.Despawning();
    }
    protected virtual void Despawning()
    {
        if (!this.canDespawn()) return;
        this.deSpawnObj();
    }
    public virtual void deSpawnObj()
    {
        Destroy(transform.parent.gameObject);
    }
    protected abstract bool canDespawn();

}
