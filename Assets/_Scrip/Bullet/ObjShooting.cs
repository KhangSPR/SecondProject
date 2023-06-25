using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class ObjShooting : SaiMonoBehaviour
{
    [SerializeField] protected bool isShooting = false;
    [SerializeField] protected float shootDelay = 0.2f;
    [SerializeField] protected float shootTimer = 0f;
    public float moveSpeed;
    public Animator animator;
    protected override void Update()
    {
        base.Update();
        this.IsShooting();
        Move();
    }

    private void FixedUpdate()
    {
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        this.shootTimer += Time.fixedDeltaTime;

        if (!this.isShooting)
        {
            return;
        }
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0;

        animator.SetBool("Attack", true);
        // Get the player's position
        GameObject player = GameObject.FindGameObjectWithTag("Tower");
        if (player == null)
        {
            this.isShooting = false;
            return;
        }
        Vector3 playerPos = player.transform.position;
        // Set the transformation to shoot towards the player
        Vector3 shootDirection = playerPos - transform.position;
        float angle = Vector3.SignedAngle(Vector3.right, shootDirection, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle );

        // Spawn and activate the new bullet
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, transform.position, rotation);
        if (newBullet == null) return;
        newBullet.gameObject.SetActive(true);

        // Set the shooter of the new bullet
        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        bulletCtrl.SetShotter(transform.parent);
    }
    protected virtual void Move()
    {
        if (!this.isShooting)
        {
            animator.SetBool("Attack", false);
            transform.parent.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }

    }
    public abstract bool IsShooting();
}
