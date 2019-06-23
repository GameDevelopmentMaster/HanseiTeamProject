using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : MonoBehaviour
{
    float Speed;
    bool Stop;
    private void OnEnable()
    {
        Stop = false;
        Speed = 1;
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.GetChild(21).gameObject.transform.position = transform.parent.GetChild(0).position + new Vector3(0.25f, -1.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Stop == false)
        {
            transform.position += Vector3.down * Time.deltaTime * Speed;
            Speed += Time.deltaTime * 3;
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            Stop = true;
            transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            transform.GetComponent<SpriteRenderer>().enabled = false;

        }
        if(collision.tag == "Player")
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(transform.parent.GetComponent<CharacterParent>().GetGameData().SkilDamage, DefList.Energy);
            transform.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            transform.gameObject.SetActive(false);
        }
    }

}
