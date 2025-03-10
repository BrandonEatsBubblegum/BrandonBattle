using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public float cost;
    public Spawner spawner;
    public UnitType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Purchase()
    {
        bool success = PointGiver.main.TakePoints(spawner.team, cost);
        if (success && spawner != null)
        {
            spawner.Spawn(type);
        }
    }
}
