using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    [SerializeField]
    Vector3 PlayerPos;
    Vector3 MovePos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector2.Distance(transform.position, GameObject.Find("Player").transform.GetChild(0).position) < 5)
        {
            transform.eulerAngles += new Vector3(0, 0, 360) * Time.deltaTime;
            PlayerPos = GameObject.Find("PlayerImage").transform.position;
            MovePos = Vector3.Normalize(PlayerPos - transform.position);
            transform.position += new Vector3(MovePos.x * Time.deltaTime * transform.GetComponentInParent<CharacterParent>().GetGameData().Speed, 0);
        }
    }
          

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent.GetComponent<CharacterParent>().Damage(transform.parent.GetComponent<CharacterParent>().GetGameData().SkilDamage, DefList.Physics);
            transform.parent.GetComponent<CharacterParent>().Damage(transform.GetComponentInParent<CharacterParent>().GetGameData().SkilDamage, DefList.Physics);
        }
    }
}
