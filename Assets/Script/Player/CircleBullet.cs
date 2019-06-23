using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 EndPos;
    public float Timer = 0.1f;
    float StartTime;
    float Damage = 200;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = true;

        StartTime = Time.time;
        StartPos = transform.position;
        switch (transform.GetComponentInParent<CharacterParent>().Code)
        {
            case 1:
            case 4:
                if(transform.position.x > GameObject.Find("PlayerImage").transform.position.x)
                {
                    EndPos = new Vector3(StartPos.x - 6.4f, StartPos.y - 2);

                }
                else if (transform.position.x < GameObject.Find("PlayerImage").transform.position.x)
                {
                    EndPos = new Vector3(StartPos.x + 6.4f, StartPos.y - 2);
                }
                   
                break;
            case 0:
                if (GameObject.Find("PlayerImage").GetComponent<SpriteRenderer>().flipX)
                {
                    EndPos = new Vector3(StartPos.x - 6.4f, StartPos.y - 2);
                }
                else
                {
                    EndPos = new Vector3(StartPos.x + 6.4f, StartPos.y - 2);
                }
               
                break;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<SpriteRenderer>().enabled)
        {
            Vector3 Center = (StartPos + EndPos) * 0.5f;
            Center -= Vector3.up;
            Vector3 StartCenter = new Vector3();
            Vector3 EndCenter = new Vector3();
            StartCenter = StartPos - Center;
            EndCenter = EndPos - Center;

            float frame = (Time.time - StartTime) / Timer;
            transform.position = Vector3.Slerp(StartCenter, EndCenter, frame);
            transform.position += Center;
            if(frame > Timer)
            {
                gameObject.SetActive(false);
            }
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            //transform.gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(collision.tag == "DownGround" && transform.GetComponentInParent<CharacterParent>().Code == 0)
        {
            collision.GetComponent<DownGround>().SetCheck();
            transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            transform.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (collision.tag == "Bullet" && collision.transform.parent.GetComponent<CharacterParent>().GetCharacter(collision.gameObject) != CharacterList.PlayerCharacter)
        {
            collision.gameObject.SetActive(false);
           // this.gameObject.SetActive(false);
        }
        if(collision.tag == "Enemy" && transform.GetComponentInParent<CharacterParent>().GetCharacter(transform.parent.gameObject) == CharacterList.PlayerCharacter)
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(Damage, DefList.Physics);
            transform.gameObject.SetActive(false);
        }

        if(collision.tag == "Player" && transform.GetComponentInParent<CharacterParent>().GetCharacter(collision.transform.parent.gameObject) != CharacterList.PlayerCharacter)
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(transform.GetComponentInParent<CharacterParent>().GetGameData().SkilDamage, DefList.Energy);
            transform.gameObject.SetActive(false);
        }
    }
}
