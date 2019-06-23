using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemy : MonoBehaviour
{
    float BulletTiming;
    public bool Check = false;
    float Speed;
    //[Range(0.1f, 10)]
    float SkilTIming =0;
    float SkilCoolTime;
    Dir dir;
    Vector2 LeftPos;
    Vector2 RightPos;
    // Start is called before the first frame update
    void Start()
    {
        LeftPos = new Vector2(transform.position.x - 3f, transform.position.y);
        RightPos = new Vector2(transform.position.x + 3f, transform.position.y);
        switch (transform.parent.GetComponent<CharacterParent>().Code)
        {
            case 3:
                SkilCoolTime = 3.5f;
                dir = Dir.Stop;
                break;
            case 6:
                dir = Dir.Left;
                SkilCoolTime = 3f;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("HomingMissile") == null)
        {
            Check = false;
        }
        if((transform.parent.GetComponent<CharacterParent>().Code == 3 || transform.parent.GetComponent<CharacterParent>().Code == 6) && SkilTIming > SkilCoolTime)
        {
            transform.parent.GetChild(21).gameObject.SetActive(true);
            SkilTIming = 0;
        }
     switch (dir)
        {
            case Dir.Left:
                transform.position -= transform.right * Time.deltaTime * 10;
                break;
            case Dir.Right:
                transform.position += transform.right * Time.deltaTime * 10;
                break;
        }

        if(transform.position.x < LeftPos.x)
        {
            dir = Dir.Right;
        }
        else if (transform.position.x > RightPos.x)
        {
            dir = Dir.Left;
        }
        if(transform.position.x > GameObject.Find("Player").transform.GetChild(0).position.x)
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (BulletTiming > transform.parent.GetComponent<CharacterParent>().GetGameData().AtkSpeed)
        {
           int count = 0;
           for(int i=1; i<this.transform.parent.childCount-1; i++)
            {
                if(this.transform.parent.GetChild(i).gameObject.activeSelf == false)
                {
                    this.transform.parent.GetChild(i).position = new Vector2(this.transform.position.x - 1.5f, this.transform.position.y - 0.6f);
                    count = i;
                    break;  
                }
            }
            this.transform.parent.GetChild(count).gameObject.SetActive(true);
            BulletTiming = 0;
        }
        BulletTiming  += Time.deltaTime;
        if(transform.parent.GetChild(21).gameObject.activeSelf == false)
        {
            SkilTIming += Time.deltaTime;
        }
        
    }

   

    
}
