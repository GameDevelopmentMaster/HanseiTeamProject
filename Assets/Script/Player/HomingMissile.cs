using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public bool upgrade = false;
    public bool Trigger;
    public bool ShootingStart;
    public GameObject[] Enemy;
    public Vector3 MinDir;
    public int Num;
    public float Speed = 1.0f;
    public int VsalueList;

    private void OnEnable()
    {
        ShootingStart = false;
        Trigger = false;
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (Enemy.Length > VsalueList)
        {
            MinDir = new Vector3();
            MinDir.Set(100000, 1000000, 100000);
            for (int i = 0; i < Enemy.Length; i++)
            {
                if (upgrade == false)
                {
                    if (Enemy[i].GetComponentInParent<CharacterParent>().GetCharacter(Enemy[i].transform.parent.gameObject) == CharacterList.AirCharacter)
                    {
                        if (Vector3.Distance(Enemy[i].transform.position, transform.position) < Vector3.Distance(MinDir, transform.position) && Enemy[i].GetComponentInParent<CharacterParent>().Check == false)
                        {
                            MinDir.x = Enemy[i].transform.position.x;
                            MinDir.y = Enemy[i].transform.position.y;
                            Num = i;
                        }

                    }
                }
                else 
                {
                    if (Vector3.Distance(Enemy[i].transform.position, transform.position) < Vector3.Distance(MinDir, transform.position) && Enemy[i].GetComponentInParent<CharacterParent>().Check == false)
                    {
                        MinDir.x = Enemy[i].transform.position.x;
                        MinDir.y = Enemy[i].transform.position.y;
                        Num = i;
                    }

                }

            }
                    ShootingStart = true;
            Enemy[Num].GetComponentInParent<CharacterParent>().Check = true;
        }
        else
        {
            gameObject.SetActive(false);
        }


    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (upgrade == false && Enemy[Num].transform.parent.gameObject.name != "AirEnemy")
        {
               transform.gameObject.SetActive(false);
        }
        if (ShootingStart)
        {
            Vector3 ToDir = Vector3.Normalize(Enemy[Num].transform.position - transform.position);
            transform.position += ToDir * Time.deltaTime * Speed;
            transform.rotation = Quaternion.Euler(0, 0, Vector3.Dot(Enemy[Num].transform.position, transform.position));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && Trigger == false)
        {
            Trigger = true;
            collision.transform.parent.GetComponent<CharacterParent>().Damage(300, DefList.Energy);
            collision.GetComponentInParent<CharacterParent>().Check = false;
            transform.gameObject.SetActive(false);
        }
        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
