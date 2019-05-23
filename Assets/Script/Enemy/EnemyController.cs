using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum Dir {Left,Right};
    
    Dir EnemyDIr;
    public float BulletTiming;
    public bool Check = false;
    float Speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
     switch (EnemyDIr)
        {
            case Dir.Left:
                transform.position -= transform.right * Time.deltaTime * 10;
                break;
            case Dir.Right:
                transform.position += transform.right * Time.deltaTime * 10;
                break;
        }
        
        if(transform.position.x < -6.76f)
        {
            EnemyDIr = Dir.Right;
        }
        if (transform.position.x > 6.86f)
        {
            EnemyDIr = Dir.Left;
        }

        if (BulletTiming > 0.3f)
        {
           int count = 0;
           for(int i=1; i<this.transform.parent.childCount; i++)
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
        BulletTiming += Time.deltaTime;
    }
}
