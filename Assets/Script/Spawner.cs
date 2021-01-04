using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int ObsToPlace = 10;
    public GameObject[] Obstacles = new GameObject[0];
    GameObject Obstacle;

    public float obstacleCheckRadius = 3f;
    public int maxSpawnAttemptsPerObstacle = 10;

    public float maxX1, maxY1 = 0;
    public float maxX2, maxY2 = 0;

    void Awake()
    {

        for (int i = 0; i < ObsToPlace; i++)
        {
            Obstacle = Obstacles[Random.Range(0, Obstacles.Length)];
            Vector3 position = Vector3.zero;
            bool validPosition = false;
            int spawnAttempts = 0;

            while (!validPosition && spawnAttempts < maxSpawnAttemptsPerObstacle)
            {
                spawnAttempts++;
                position = new Vector3(Random.Range(maxX1, maxX2), 0, Random.Range(maxY1, maxY2));
                validPosition = true;
                Collider[] colliders = Physics.OverlapSphere(position, obstacleCheckRadius);
                foreach (Collider col in colliders)
                {
                    if (col.tag == "Obstacle")
                    {
                        validPosition = false;
                    }
                }

                if (spawnAttempts > 7)
                {
                    maxX1 -= 25;
                    maxX2 += 25;
                    maxY1 += 25;
                    maxY2 -= 25;
                }
            }

            if (validPosition)
            {
                Instantiate(Obstacle, position + Obstacle.transform.position, Quaternion.identity);
            }
        }
    }
}