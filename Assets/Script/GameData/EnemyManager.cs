using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject Enemy;
    float ReseponEnemy;
    // Start is called before the first frame update
    void Start()
    {
        ReseponEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ReseponEnemy > 1f)
        {
            GameObject A =Instantiate(Enemy, new Vector3(10.72f, 2.72f, 0), Quaternion.identity);
            A.name = "Enemy";
            ReseponEnemy = 0;

        }
        else
        {
            ReseponEnemy += Time.deltaTime;
        }
    }
}
