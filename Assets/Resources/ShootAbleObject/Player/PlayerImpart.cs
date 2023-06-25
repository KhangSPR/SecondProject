using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerImpart : PlayerAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected BoxCollider2D boxCollider2D;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected float stackingDistanceLimit = 3f;
    //Detect detect;   Vector2 min = tilemap.CellToWorld(pos);

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCircleCollider2D();
        this.LoadRigibody();
    }
    protected virtual void LoadCircleCollider2D()
    {
        if (this.boxCollider2D != null) return;
        this.boxCollider2D = GetComponent<BoxCollider2D>();
        this.boxCollider2D.isTrigger = true;
        this.boxCollider2D.offset = new Vector2(-0.2f, 0f);
        this.boxCollider2D.size = new Vector2(0.2f, 0.6f);
        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._rigidbody.bodyType = RigidbodyType2D.
            Kinematic;
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
            // Nếu khoảng cách giữa enemy đủ lớn, di chuyển
            if (!playerCtrl.Detect.stopMoving)
                Move(); 
            else
                Idle();
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

    //protected virtual bool CheckEnemiesSpacing()
    //{
    //    if (UISpawner.instance == null || UISpawner.instance.enemies.Count == 0)
    //    {
    //        Debug.Log("WARNING!!!");
    //        return false;
    //    }

    //    for (int i = 0; i < UISpawner.instance.enemies.Count - 1; i++)
    //    {
    //        float distanceX = Mathf.Abs(UISpawner.instance.enemies[i].transform.position.x - UISpawner.instance.enemies[i + 1].transform.position.x);
    //        if (distanceX < enemySpacing)
    //        {
    //            return true; // nếu khoảng cách giữa hai enemy liền kề đủ nhỏ, trả lại true
    //        }
    //    }

    //    return false;  // trong tất cả các trường hợp khác, trả về false
    //}
    void Idle()
    {
        animator.Play("Idle");
    }    
    void Move()
    {
        animator.Play("Move");
        transform.parent.Translate(transform.right * moveSpeed * Time.deltaTime);

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

        if (collision.transform.parent.tag == "Enemy")
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