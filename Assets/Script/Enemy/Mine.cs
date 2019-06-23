using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Data
{
        

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Player")
        {
            transform.parent.GetComponent<CharacterParent>().Damage(transform.parent.GetComponent<CharacterParent>().GetGameData().SkilDamage, DefList.Physics);
            collision.GetComponentInParent<CharacterParent>().Damage(transform.parent.GetComponent<CharacterParent>().GetGameData().SkilDamage,DefList.Energy);
        }
    }
}
