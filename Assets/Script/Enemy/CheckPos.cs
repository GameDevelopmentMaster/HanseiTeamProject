using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPos : MonoBehaviour
{
    float Timer;

    private void OnEnable()
    {
        Timer = 0;

    }
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<BoxCollider2D>().enabled)
        Timer += Time.deltaTime;
        if(Timer > 1.5f&& transform.GetComponent<BoxCollider2D>().enabled)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && transform.GetComponentInParent<CharacterParent>().GetCharacter(transform.parent.gameObject) != CharacterList.PlayerCharacter)
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(transform.GetComponentInParent<CharacterParent>().GetGameData().SkilDamage*0.7f, DefList.Energy);
            if (collision.transform.position.x < transform.position.x)
            {
                collision.transform.parent.GetComponent<CharacterParent>().Move(-1.5f);

            }
            else
            {
                collision.transform.parent.GetComponent<CharacterParent>().Move(1.5f);
            }
            transform.parent.gameObject.SetActive(false);
        }

        if (collision.tag == "Enemy" && transform.GetComponentInParent<CharacterParent>().GetCharacter(transform.parent.gameObject) == CharacterList.PlayerCharacter)
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(200 * 0.7f, DefList.Physics);
            if (collision.transform.position.x < transform.position.x)
            {
                collision.transform.parent.GetComponent<CharacterParent>().Move(-1.5f);

            }
            else
            {
                collision.transform.parent.GetComponent<CharacterParent>().Move(1.5f);
            }
            //transform.parent.gameObject.SetActive(false);
        }

    }


   

}
