using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    bool Trigger;
    bool ShootingStart;
    public GameObject[] Enemy;
    Vector3 MinDir;
    public int Num;
    public float Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Trigger = false;
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemy.Length != 0)
        {
            MinDir = new Vector3();
            MinDir.Set(100000, 1000000, 100000);
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (Vector3.Distance(Enemy[i].transform.position, transform.position) < Vector3.Distance(MinDir, transform.position) && Enemy[i].GetComponent<EnemyController>().Check == false)
                {
                    MinDir.x = Enemy[i].transform.position.x;
                    MinDir.y = Enemy[i].transform.position.y;
                    Num = i;
                }
            }
            ShootingStart = true;
            Enemy[Num].GetComponent<EnemyController>().Check = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootingStart)
        {
            if (Enemy[Num] == null)
            {
                Enemy = GameObject.FindGameObjectsWithTag("Enemy");
                if (Enemy.Length != 0)
                {
                    MinDir = new Vector3();
                    MinDir.Set(100000, 1000000, 100000);
                    for (int i = 0; i < Enemy.Length; i++)
                    {
                        if (Vector3.Distance(Enemy[i].transform.position, transform.position) < Vector3.Distance(MinDir, transform.position) && Enemy[i].GetComponent<EnemyController>().Check == false)
                        {
                            MinDir.x = Enemy[i].transform.position.x;
                            MinDir.y = Enemy[i].transform.position.y;
                            Num = i;
                        }
                    }
                }
                else
                {
                    Destroy(this.gameObject);
                }

            }

            Vector3 ToDir = Vector3.Normalize(Enemy[Num].transform.position - transform.position);
            transform.position += new Vector3(ToDir.x * Time.deltaTime * Speed, ToDir.y * Time.deltaTime * Speed);
            transform.Rotate(0, 0, Quaternion.Dot(Enemy[Num].transform.rotation, transform.rotation), Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && Trigger == false)
        {
            Trigger = true;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
