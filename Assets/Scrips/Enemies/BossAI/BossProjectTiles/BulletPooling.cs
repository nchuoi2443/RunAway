using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling Instance;

    public GameObject bulletPrefab;
    public int poolSize = 30;
    

    private List<GameObject> pool = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        InitPool();
    }

    void InitPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in pool)
        {
            if (!bullet.activeInHierarchy)
                return bullet;
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnProjectTile(GameObject projectTile)
    {
        projectTile.SetActive(false);
    }
}
