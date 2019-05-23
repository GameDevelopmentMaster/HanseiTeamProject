using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nulclaer : MonoBehaviour
{
    public ParticleSystem Effter;
    GameObject[] Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemy.Length != 0)
        {
            for(int i=0; i<Enemy.Length; i++)
            {
                Destroy(Enemy[i]);
            }
            Destroy(this.gameObject);
            Instantiate(Effter, transform.position, Quaternion.identity);
        }
        if(Effter.time > 3.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
