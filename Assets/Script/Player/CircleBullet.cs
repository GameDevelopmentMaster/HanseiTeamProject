using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    Vector3 StartPos;
    Vector3 EndPos;
    public float Timer = 1f;
    float StartTime;
    public Bullet.CharacterList characterList;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartTime = Time.time;
        StartPos = transform.position;
        EndPos = new Vector3(StartPos.x + 6.4f, StartPos.y - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Center = (StartPos + EndPos) * 0.5f;
        Center -= Vector3.up;
        Vector3 StartCenter = StartPos  - Center;
        Vector3 EndCenter = EndPos - Center;
        float frame = (Time.time - StartTime) / Timer;
        transform.position = Vector3.Slerp(StartCenter, EndCenter, frame);
        transform.position += Center;
        
        if (frame > Timer)
        {
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            transform.gameObject.SetActive(false);
        }
        if (collision.tag == "Bullet" && collision.GetComponent<Bullet>().Character != characterList)
        {
            collision.gameObject.SetActive(false);
           // this.gameObject.SetActive(false);
        }
    }
}
