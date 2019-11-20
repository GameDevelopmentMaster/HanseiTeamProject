using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

   public  Dir dir;
    //public CharacterList Character;
    public Vector3 PlayerTransform;
    public Vector3 Dir;
    public float Speed;
    public ParticleSystem BoomAnimation;
    bool Check = false;
    float Damage;

    private void OnEnable()
    {
        Damage = transform.parent.GetComponent<CharacterParent>().GetGameData().Damage;
        if (transform.parent.GetChild(0).tag == "Bullet" || GameObject.FindWithTag("Player") == null)
        {
            Check = true;
        }
        else if (GameObject.FindWithTag("Player") != null)
        {
            PlayerTransform = GameObject.FindWithTag("Player").transform.position;
            Dir = Vector3.Normalize(PlayerTransform - this.transform.position);
        }
        Speed = 3f;

        if (transform.parent.GetChild(0).GetComponent<SpriteRenderer>().flipX == true)
        {
            dir = global::Dir.Left;
            transform.position -= transform.right;
        }
        else
        {
            dir = global::Dir.Right;
            transform.position += transform.right;
        }

    }
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Check)
        {
            return;
        }

        switch (transform.parent.GetChild(0).name)
        {
            case "PlayerImage":
                Speed = 7f;
                if(dir == global::Dir.Right)
                this.gameObject.transform.position += this.transform.right * Time.deltaTime * Speed;
                if(dir == global::Dir.Left)
                    this.gameObject.transform.position += -this.transform.right * Time.deltaTime * Speed;
                break;
            case "EnemyImage":
                if(dir == global::Dir.Stop)
                {
                    transform.position -= transform.up;
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(Dir.x * Time.deltaTime * Speed, Dir.y * Time.deltaTime * Speed);
                }
                
                break;
        }
        //else
        //{
        //    this.transform.position -= this.transform.up * Time.deltaTime * Speed;
        //}
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            return;
        }
        if (collision.tag == "Ground")
        {
            gameObject.SetActive(false);
            return;
        }
        if (collision.transform.GetComponentsInParent<CharacterParent>() != null)
        {
            if (collision.transform.parent.GetComponent<CharacterParent>() != null && transform.parent.GetChild(0).tag != collision.transform.parent.GetChild(0).tag)
            {
                this.gameObject.SetActive(false);
                collision.transform.parent.GetComponent<CharacterParent>().Damage(Damage, DefList.Physics);
            }
        }
        
         if (collision.tag == "Bullet" && collision.transform.parent.GetChild(0).tag != transform.parent.GetChild(0).tag)
        {
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            gameObject.SetActive(false);
        }
    }
}
