using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSKill : MonoBehaviour
{
    public AudioSource nas;
    public AudioSource ang;
    GameObject Enemy10;
    float SkilCoolTiming;
    float StartTiming;
    int PreSkil;
    bool Check;
    int NowSkil;
    // Start is called before the first frame update
    void Start()
    {
        SkilCoolTiming = 3f;
        PreSkil = Random.Range(1, 4);
        switch (PreSkil)
        {
            case 1:
                transform.GetComponent<CharacterParent>().SetShield(true);
                StartCoroutine(BossShieldStart());
                break;
            case 2:
            case 3:
            case 4:
                StartCoroutine(LeftHand());
                break;
            case 5:
                StartCoroutine(RightHand());
                break;
            case 6:
                Respen();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if(StartTiming> SkilCoolTiming)
        {  NowSkil = Random.Range(1,4);
            while (PreSkil == NowSkil)
            {
                NowSkil = Random.Range(1, 6);
            }

            switch (NowSkil)
            {
                case 1:
                    transform.GetComponent<CharacterParent>().SetShield(true);
                    StartCoroutine(BossShieldStart());
                    break;
                case 2:
                case 3:
                case 4:
                    StartCoroutine(LeftHand());
                    break;
                case 5:
                    StartCoroutine(RightHand());
                    break;
                case 6:
                    Respen();
                    break;
            }
            PreSkil = NowSkil;
            StartTiming = 0;
        }
        StartTiming += 1 * Time.deltaTime;
    }

   public void Respen()
    {
        for(int i=0; i<4; i++)
        {
            Instantiate(Enemy10,new Vector3(10*(i+1),1),Quaternion.identity);
        }
    }

    IEnumerator BossShieldStart()
    {
        SpriteRenderer BossSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        Color color = new Color(0.1f, 0.1f, 0.1f,0);
        for(int i=0; i<4; i++)
        {
            BossSprite.color -= color;
            yield return null;
        }
    }

   public IEnumerator BossShieldEnd()
    {
        SpriteRenderer BossSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        Color color = new Color(0.1f, 0.1f, 0.1f,0);
        for (int i = 0; i < 4; i++)
        {
            BossSprite.color += color;
            yield return null;
        }
    }

    IEnumerator CrossSkil()
    {
        for(int i=3; i<transform.childCount-1; i++)
        {
            if(transform.GetChild(i).GetComponent<BossBullet>().skilList == BossSkil.CrossDirBullet)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i + 5).gameObject.SetActive(true);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
    IEnumerator RightHand()
    {
        for(int i=0; i<50; i++)
        {
            transform.GetChild(1).position += transform.up * Time.deltaTime * 10;
            yield return null;
        }
      
        for (int i = 0; i < 50; i++)
        {
            transform.GetChild(1).position -= transform.up * Time.deltaTime * 10;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        GameObject.Find("Main Camera").GetComponent<CameraShake>().ShakeStart = true;
        transform.GetComponent<BoxCollider2D>();
        nas.Play();
    }
    IEnumerator LeftHand()
    {
        for(int i=0; i<50; i++)
        {
            transform.GetChild(0).position += transform.up * Time.deltaTime * 10;
            yield return null;
        }
        for(int i=0; i<50; i++)
        {
            transform.GetChild(0).position -= transform.up * Time.deltaTime * 10;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        switch (NowSkil)
        {
            case 2:
                for (int i = 3; i < transform.childCount - 3; i++)
                {
                    if (transform.GetChild(i).GetComponent<BossBullet>().skilList == BossSkil.LeftDirBullet)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                        transform.GetChild(i +7).gameObject.SetActive(true);
                    }

                }
                break;
            case 3:
                for (int i = 3; i < transform.childCount - 3; i++)
                {
                    if (transform.GetChild(i).GetComponent<BossBullet>().skilList == BossSkil.DownDirBullet)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }

                }
                break;
            case 4:
                StartCoroutine(CrossSkil());
                break;
        }
        ang.Play();
    }
}
