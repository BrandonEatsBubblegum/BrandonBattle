using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGiver : MonoBehaviour
{
    public Spawner blueSpawn;
    public Spawner redSpawn;
    public static PointGiver main;
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        blueSpawn.GivePoints(0);
        redSpawn.GivePoints(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GivePoints(string team, float amount)
    {
        if(team == "blue")
        {
            blueSpawn.GivePoints(amount);
        }
        if(team == "red")
        {
            redSpawn.GivePoints(amount);
        }
    }
    public bool TakePoints(string team, float amount)
    {
        if (team == "blue")
        {
            return blueSpawn.TakePoints(amount);
        }
        if (team == "red")
        {
            return redSpawn.TakePoints(amount);
        }
        return false;
    }
}
