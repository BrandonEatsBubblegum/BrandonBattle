using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int numToSpawn = 1;
    public KeyCode key;
    public string team;
    public GameObject basicUnit;
    public GameObject rareUnit;
    public GameObject legendaryUnit;
    public GameObject mythicalUnit;
    public GameObject secretWeaponUnit;
    public GameObject turretUnit;
    public Transform enemyBase;
    float nextSpawnTime = 0f;
    float spawnInterval = 3f;
    public float health = 500f;
    public float points = 0f;
    public MeshRenderer spawnRenderer;
    public TextMeshPro healthText;
    public TextMeshPro pointsText;
    private AudioSource spawnAudio;

    // Start is called before the first frame update
    void Start()
    {
        spawnAudio = GetComponent<AudioSource>();
        TakeDamage(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnInterval;
            Spawn(UnitType.normal);
        }
        /*if(Input.GetKeyDown(key))
        {
            Spawn();
        }*/
    }
    public void Spawn(UnitType type)
    {
        GameObject typeToSpawn = null;
        if (type == UnitType.normal)
        {
            typeToSpawn = basicUnit;
        }
        if (type == UnitType.rare)
        {
            typeToSpawn = rareUnit;
        }
        if (type == UnitType.legendary)
        {
            typeToSpawn = legendaryUnit;
        }
        if (type == UnitType.mythical)
        {
            typeToSpawn = mythicalUnit;
        }
        if (type == UnitType.secretweapon)
        {
            typeToSpawn = secretWeaponUnit;
        }
        if (type == UnitType.turret)
        {
            typeToSpawn = turretUnit;
        }
        for (int i = 0; i < numToSpawn; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.1f;
            if(type == UnitType.turret)
            {
                spawnPos.z += Random.Range(-50f, 50f);
            }
            GameObject instance = Instantiate(typeToSpawn, spawnPos, transform.rotation);
            instance.GetComponent<BattleBall>().enemyBase = enemyBase;
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), instance.GetComponent<Collider>());
        }
        spawnAudio.Play();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + ((int)health);
        if (health <= 0)
        {
            healthText.text = "DEAD";
            Destroy(spawnRenderer.gameObject);
            Destroy(gameObject);
        }
    }
    public void GivePoints(float p)
    {
        points += p;
        pointsText.text = "Points: " + points;
    }
    public bool TakePoints(float p)
    {
        if(p > points)
        {
            return false;
        }
        points -= p;
        pointsText.text = "Points: " + points;
        return true;
    }
}

public enum UnitType
{
    normal,
    rare,
    legendary,
    mythical,
    secretweapon,
    turret
}
