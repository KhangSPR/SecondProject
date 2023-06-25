using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyImpart : EnemyAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected CircleCollider2D circleCollider;
    [SerializeField] protected Rigidbody2D _rigidbody;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCircleCollider2D();
        this.LoadRigibody();
    }

    protected virtual void LoadCircleCollider2D()
    {
        if (this.circleCollider != null) return;
        this.circleCollider = GetComponent<CircleCollider2D>();
        this.circleCollider.isTrigger = true;
        this.circleCollider.offset = new Vector2(-0.65f, -0.4f);
        this.circleCollider.radius = 0.6f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._rigidbody.bodyType = RigidbodyType2D.Kinematic;
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }
    public int attackPower;
    public float moveSpeed;

    public Animator animator;
    public float attackInterval;
    Coroutine attackOrder;
    ShootAbleObjectDamageReceiver detectedTower;

    protected override void Update()
    {
        base.Update();
        if (!detectedTower)
        {
            Move();
        }
    }

    IEnumerator Attack()
    {
        animator.Play("Attack", 0, 0);
        //Wait attackInterval 
        yield return new WaitForSeconds(attackInterval);
        //Attack Again
        attackOrder = StartCoroutine(Attack());
    }

    //Moving forward
    void Move()
    {
        animator.Play("Move");
        transform.parent.Translate(-transform.right * moveSpeed * Time.deltaTime);
    }

    public void InflictDamage()
    {
        bool towerDied = detectedTower.LoseHealth(attackPower);

        if (towerDied)
        {
            detectedTower = null;
            StopCoroutine(attackOrder);
        }
    }

    ////Lose health
    //public void LoseHealth()
    //{
    //    //Decrease health value
    //    health--;
    //    //Blink Red animation
    //    StartCoroutine(BlinkRed());
    //    //Check if health is zero => destroy enemy
    //    if (health <= 0)
    //        Destroy(gameObject);
    //}

    IEnumerator BlinkRed()
    {
        //Change the spriterendere color to red
        GetComponent<SpriteRenderer>().color = Color.red;
        //Wait for really small amount of time 
        yield return new WaitForSeconds(0.2f);
        //Revert to default color
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (detectedTower)
            return;

        if (collision.transform.parent.tag == "Tower")
        {
            detectedTower = collision.GetComponent<ShootAbleObjectDamageReceiver>();
            attackOrder = StartCoroutine(Attack());
        }
    }
    /*
    protected virtual void CreateImpactFX(Collider other)
    {
        string fxName = this.GetImpactFX();

        Vector3 hitPos = transform.position;
        Quaternion hitRot = transform.rotation;
        Transform fxImpact = FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
        fxImpact.gameObject.SetActive(true);

        //fxImpact.parent = other.transform.parent;
        //Debug.LogError("stop");

        //Trung Nghia Nguyen
        //Vector3 dir = Vector3.Normalize(hitPos);
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Quaternion rotate = Quaternion.Euler(0, 0, angle + 90f);
        //fxImpact.rotation = rotate;
    }

    protected virtual string GetImpactFX()
    {
        return FXSpawner.impact1;
    }*/
}