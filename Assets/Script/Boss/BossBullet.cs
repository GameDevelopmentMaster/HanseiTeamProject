using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public BossSkil skilList;
    private void OnEnable()
    {
        switch (skilList)
        {
            case BossSkil.LeftDirBullet:
            case BossSkil.CrossDirBullet:
                transform.position = transform.parent.position;
                if (transform.position.x < GameObject.Find("PlayerImage").transform.position.x && transform.rotation.z < 2270)
                {
                    transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + (270f - transform.eulerAngles.z) * 2);
                }
                else if (transform.position.x > GameObject.Find("PlayerImage").transform.position.x && transform.rotation.z > 270)
                {
                    transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - (270f - transform.eulerAngles.z) * 2);
                }
                break;
            case BossSkil.DownDirBullet:
                transform.position = transform.parent.position + Vector3.up * 3;
                break;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (skilList)
        {
            case BossSkil.LeftDirBullet:
                transform.position -= transform.right * Time.deltaTime * 10f;
                break;
            case BossSkil.CrossDirBullet:
                if(transform.rotation.z > 0)
                {
                    transform.position += ( transform.right * Time.deltaTime) * 10f;
                }
                if (transform.rotation.z < 0)
                {
                    transform.position -= (transform.right * Time.deltaTime +transform.up * Time.deltaTime) * 10f;
                }
                break;
            case BossSkil.DownDirBullet:
                transform.position -= transform.up * Time.deltaTime;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInParent<CharacterParent>().Damage(75, DefList.Energy);
            transform.gameObject.SetActive(false);
        }
        if(collision.tag == "Ground")
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            transform.gameObject.SetActive(false);
        }
        Debug.Log(collision.name);
    }
}
