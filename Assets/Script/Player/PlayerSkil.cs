using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkil : MonoBehaviour
{

   public  SpriteRenderer BackGround;       //스킬 백그라운드
    public SpriteRenderer Zoom;             //조준점
    public SpriteMask mask;                 //스프라이트 마스크

    [SerializeField]
    float Speed;
    [SerializeField]
    int GunCount = 3;
    [SerializeField]
    int Check = 0;
    [SerializeField]
    float SkilTime;
    [SerializeField]
    float SkilStayTime;
    [SerializeField]
    bool SkilStart;                         //스킬 시작
    [SerializeField]
    Color color;                            //컬러 색
    // Start is called before the first frame update
    void Start()
    {
        SkilTime = 0;
        color = new Color(0, 0, 0, 0.1f);

        
    }

    // Update is called once per frame
    void Update()
    {
        BackGround = GameObject.Find("BackGround").GetComponent<SpriteRenderer>();
        Zoom = GameObject.Find("Zoom").GetComponent<SpriteRenderer>();
        mask = GameObject.Find("Mask").GetComponent<SpriteMask>();
        if (SkilStart)
        {
            if (SkilStayTime <= 0)
            {
                StartCoroutine(ZoomRelease());
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                mask.transform.position += Vector3.left * Time.deltaTime * Speed;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                mask.transform.position += Vector3.right * Time.deltaTime * Speed;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                mask.transform.position += Vector3.up * Time.deltaTime *Speed;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                mask.transform.position += Vector3.down * Time.deltaTime * Speed;
            }

            if (Input.GetKeyDown(KeyCode.A) && GunCount >0)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(mask.transform.position, mask.transform.forward);
                StartCoroutine(Shooting());
                for(int i=0; i<hit.Length; i++)
                {
                    if (hit[i].transform.tag == "Enemy")
                    {
                        hit[i].transform.GetComponentInParent<CharacterParent>().Damage(950, DefList.Physics);
                    }
                }
               
                
                GunCount--;
            }

            SkilStayTime -= Time.deltaTime;
        }
        else
        {
            if (SkilTime > 0)
            {
                SkilTime -= Time.deltaTime * 1;
            }
        }
    }

    public void CountUp()
    {
        GunCount++;
    }

    public IEnumerator ZoomInit()
    {
        GunCount = 3;
        Check = 0;
        if (SkilTime <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                BackGround.color += color;
                Zoom.color += color;
                yield return null;
            }
            SkilTime = 2;
            SkilStayTime = 5;
            SkilStart = true;
        }
    }

    IEnumerator ZoomRelease()
    {
        if (Check > 0)
        {
            yield break;
        }
        Check++;
        for (int i = 0; i < 10; i++)
        {
            BackGround.color -= color;
            Zoom.color -= color;
            yield return null;
        }
        SkilStart = false;
    }

    IEnumerator Shooting()
    {
        mask.transform.position += Vector3.up * 2;
        for(int i=0; i<20; i++)
        {
            mask.transform.position += Vector3.down*0.1f;
            yield return null;
        }
    }
    public bool GetSkil()
    {
        return SkilStart;
    }
}
