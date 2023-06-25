using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : EnemyAbstract
{
    public float moveSpeed;
    public Animator animator;
    //Moving forward
    protected virtual void Move()
    {
        transform.parent.Translate(-transform.right * moveSpeed * Time.deltaTime);

    }
}
