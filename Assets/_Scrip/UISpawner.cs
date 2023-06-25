using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UISpawner: SaiMonoBehaviour
{
    //list of towers (prefabs) that will instantiate
    public List<GameObject> towersPrefabs;
    //Transform of the spawning towers (Root Object)
    public Transform spawnTowerRoot;
    //list of towers (UI)
    public List<Image> towersUI;
    //id of tower to spawn
    int spawnID = -1;
    //Tilemap to spawn the tower on
    public Tilemap tilemap;

    public static UISpawner instance;
    public static UISpawner Instance => instance;
    public List<Transform> enemies = new List<Transform>();
    protected override void Awake()
    {
        base.Awake();
        if (UISpawner.instance != null) Debug.LogError("Onlly 1 UISpawner Warning");
        UISpawner.instance = this;
    }

    protected override void Update()
    {
        base.Update();
        if (CanSpawn())
            DetectSpawnPoint();
    }

    bool CanSpawn()
    {
        if (spawnID == -1)
            return false;
        else
            return true;
    }
    [SerializeField] private float enemySpacing = 1f;
    void DetectSpawnPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePos = tilemap.WorldToCell(mouseWorldPos);

            if (tilemap.GetTile(tilePos) != null)
            {
                Vector3 spawnPosition = tilemap.GetCellCenterWorld(tilePos);

                // Sinh ra enemy tại vị trí spawnPosition
                GameObject tower = Instantiate(towersPrefabs[spawnID], spawnPosition, Quaternion.identity, spawnTowerRoot);

                Transform newTower = tower.transform;
                // Deselect all towers
                enemies.Add(newTower);
                DeselectTowers();
            }
        }
    }
    public virtual bool Check()
    {
        List<Vector2> positions = new List<Vector2>(); // danh sách chứa vị trí các enemy

        // Tạo danh sách chứa vị trí hiện tại của các enemy
        foreach (var enemy in enemies)
        {
            positions.Add(enemy.transform.position);
        }

        // Di chuyển các enemy đến vị trí của enemy tiếp theo trong list cách nó 1 đơn vị
        for (int i = 1; i < positions.Count; i++)
        {
            float distance = Vector2.Distance(positions[i], positions[i - 1]);
            if (distance > enemySpacing)
            {
                return true;
            }

        }

        int lastIndex = enemies.Count - 1;
        bool isLastEnemyDead = false;
        PlayerImpart lastEnemy = enemies[lastIndex].GetComponent<PlayerImpart>();
        if (lastEnemy != null && lastEnemy.PlayerCtrl.ShootAbleObjectDamageReceiver.IsDead())
        {
            isLastEnemyDead = true;
        }

        if (lastIndex > 0 && (!isLastEnemyDead || isLastEnemyDead && (lastIndex > 1 && !enemies[lastIndex - 1].GetComponent<PlayerImpart>().PlayerCtrl.ShootAbleObjectDamageReceiver.IsDead())))
        {
            float distance = Vector2.Distance(enemies[lastIndex].transform.position, enemies[lastIndex - 1].transform.position);
            if (distance >= enemySpacing && lastIndex > 0)
            {
                enemies[lastIndex].GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Đứng yên nếu khoảng cách enemy phía sau thỏa mãn
                return true;
            }
            else
            {
                return false;
            }
        }

        return false; // Trả về false nếu không có kẻ đứng sau hoặc khoảng cách chưa thỏa mãn
    }
    public void RevertCellState(Vector3Int pos)
    {
        //Not needed anymore
    }

    public void SelectTower(int id)
    {
        DeselectTowers();
        //Set the spawnID
        spawnID = id;


        //Highlight the tower
        towersUI[spawnID].color = Color.white;

        //SpawnEnemy();
        // Activate the game object

        towersPrefabs[spawnID].SetActive(true);
    }

    public void DeselectTowers()
    {
        spawnID = -1;
        foreach (var t in towersUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

}