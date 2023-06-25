using UnityEngine;

public class ShipShootByDistance : ObjShooting
{
    [SerializeField] protected string targetTag = "Tower";
    [SerializeField] protected float distance = Mathf.Infinity;
    [SerializeField] protected float shootDistance = 5f;
    [SerializeField] protected Transform target;

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    public override bool IsShooting()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            if (!SetTarget())
            {
                this.isShooting = false;
                return false;
            }
        }

        this.distance = Vector3.Distance(transform.position, this.target.position);

        if (this.distance > this.shootDistance)
        {
            this.isShooting = false;
            Move();
            return false;
        }

        this.isShooting = true;
        return true;
    }

    protected bool SetTarget()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, shootDistance, LayerMask.GetMask(targetTag));
        if (col != null)
        {
            Transform newTarget = col.transform;
            if (newTarget == null)
            {
                return false;
            }
            SetTarget(newTarget);
            return true;
        }
        return false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.parent.CompareTag(targetTag))
        {
            Transform newTarget = col.transform;
            if (newTarget == null)
            {
                return;
            }
            if (newTarget == target)
            {
                OnTowerDestroyed();
            }
            SetTarget(newTarget);
        }
    }

    protected void OnTowerDestroyed()
    {
        if (target != null && target.parent != null && target.parent.CompareTag(targetTag))
        {
            SetTarget(target.parent);
        }
        else
        {
            target = null;
        }
    }

    // Lưu ý: bạn có thể ghi đè phương thức OnDestroy() của Tower để gọi hàm OnTowerDestroyed() ở đây nếu cần thiết.
}