using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   public enum CharacterList { PlayerCharacter, EnemyCharacter };

    public CharacterList Character;
    public Vector3 PlayerTransform;
    public Vector3 Dir;
    public float Speed;
    public ParticleSystem BoomAnimation;
    bool Check=false;

    private void OnDisable()
    {

        Debug.Log(this.gameObject.name);
        //if(Character == CharacterList.EnemyCharacter)
        //{
        //   // BoomAnimation.Play();
        //}
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.GetChild(0).tag == "Bullet")
        {
            Check = true;
        }
        Speed = 5f;
        if (GameObject.FindWithTag("Player") != null)
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform.position;
        }
        else
        {
            PlayerTransform = new Vector2(-100,0);
            return;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if (Check)
        {
            return;
        }
        if (PlayerTransform.x != -100)
        {
            Dir = Vector3.Normalize(PlayerTransform - this.transform.position);
            switch (Character)
            {
                case CharacterList.PlayerCharacter:
                    this.gameObject.transform.position += this.transform.right * Time.deltaTime * Speed;
                    break;
                case CharacterList.EnemyCharacter:
                    this.gameObject.transform.position += new Vector3(Dir.x * Time.deltaTime * Speed, Dir.y * Time.deltaTime * Speed);
                    break;
            }
        }
        else
        {
            this.transform.position -= this.transform.up*Time.deltaTime * Speed;
        }
        if (this.gameObject.transform.position.x > 11f || this.gameObject.transform.position.y > 6.0f || this.gameObject.transform.position.x < -7f || this.transform.position.y < -3.5f)
        {
            this.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Character == CharacterList.EnemyCharacter)
        {
            this.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Enemy"&&Character == CharacterList.PlayerCharacter)
        {
            this.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
        if (collision.GetComponent<Bullet>() == null)
        {
            return;
        }
        else if(collision.tag == "Bullet" && collision.GetComponent<Bullet>().Character != Character)
        {  
            if(this.transform.childCount != 0)
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(false);
            }   
        }
       
    }

}
