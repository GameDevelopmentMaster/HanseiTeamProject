using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class Player : MonoBehaviour
{ 
    public int CheckImage;
    public GameObject HomingMissile;
    public GameObject CircleBullet;
    public float Speed;
    public Transform Pos;
    public GameObject LaserObject;
    public AudioSource Gun;
    CameraShake camaraShake;
    PlayerSkil skil;
    PlayerImage playerImage;
    PlayerBust bust;
    BackGroundMove groundMove;
    float BulletTiming;
    float LaserWidth = 0;
    //플레이어 수치 관련 수치

    int HomingMissileCount;
    public int JumpCount;
    bool EneryBoom = false;
    int BulletCount = 50;

    private void Awake()
    {
        skil = transform.parent.GetComponent<PlayerSkil>();
        camaraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        playerImage = GameObject.Find("PlayerManager").GetComponent<PlayerImage>();
        bust = transform.GetComponent<PlayerBust>();
        HomingMissileCount = 3;
        groundMove = GameObject.Find("GroundManager").GetComponent<BackGroundMove>();
    }
    // Start is called before the first frame update
    void Start()
    {
        BulletTiming = 0;
        Speed = transform.parent.GetComponent<CharacterParent>().GetGameData().Speed;
        JumpCount = 1;
    }

   
    // Update is called once per frame
    void Update()
    {
        if (skil.GetSkil() == false)
        {
            //총알 자동 발사
            if (Input.GetKey(KeyCode.A) && BulletCount >0)
            {

                if (BulletTiming > 0.125f && EneryBoom == false)
                {
                    for (int i = CheckImage + 3; i < this.transform.parent.childCount; i++)
                    {
                        if (this.transform.parent.GetChild(i).gameObject.activeSelf == false)
                        {
                            this.transform.parent.GetChild(i).position = transform.position;
                            this.transform.parent.GetChild(i).gameObject.SetActive(true);
                            break;
                        }
                    }
                    Gun.Play();
                    Debug.Log("Play");
                    BulletTiming = 0;
                    BulletCount--;
                    playerImage.BulletText(BulletCount.ToString());
                }
            }
            else if(Input.GetKey(KeyCode.A) && BulletCount <= 1)
            {
               playerImage.BulletRed(ref BulletCount);
            }
            else if(Input.GetKeyUp(KeyCode.A))
            {
                Gun.Stop();
            }

            //플레이어 움직임
            if (Input.GetKey(KeyCode.RightArrow) && EneryBoom == false)
            {
                transform.position += transform.right * Time.deltaTime * Speed;
                transform.GetComponent<SpriteRenderer>().flipX = false;
                groundMove.RightMove();
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && EneryBoom == false)
            {
                transform.position -= transform.right * Time.deltaTime * Speed;
                transform.GetComponent<SpriteRenderer>().flipX = true;
                groundMove.LeftMove();
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                for(int i=CheckImage+3; i<transform.parent.childCount; i++)
                {
                    if (this.transform.parent.GetChild(i).gameObject.activeSelf == false)
                    {
                        this.transform.parent.GetChild(i).position = Pos.position;
                        if (transform.GetComponent<SpriteRenderer>().flipX)
                        {
                            transform.parent.GetChild(i).rotation = Quaternion.Euler(0, 0, -90);
                        }
                        else
                        {
                            transform.parent.GetChild(i).rotation = Quaternion.Euler(0, 0, 90);
                        }
                        
                        break;
                    }
                }
            }
            else
            {
                for (int i = CheckImage + 3; i < transform.parent.childCount; i++)
                {
                    if (this.transform.parent.GetChild(i).gameObject.activeSelf == false)
                    {
                        this.transform.parent.GetChild(i).position = Pos.position;
                        transform.parent.GetChild(i).rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0&& EneryBoom == false) 
            {
                Rigidbody2D Check = GetComponent<Rigidbody2D>();
                Check.AddForce(new Vector2(0, 300));
                JumpCount -= 1;
            }


            //플레이어 스킬
            if (Input.GetKeyDown(KeyCode.Q) && playerImage.ReturnSkil(0).fillAmount >= 1)
            {
                CircleBullet.transform.position = Pos.position;
                CircleBullet.SetActive(true);
                playerImage.ReturnSkil(0).fillAmount = 0;
            }

            if (Input.GetKeyDown(KeyCode.W) && playerImage.ReturnSkil(1).fillAmount >= 1)
            {
                for (int i = 1; i < 4; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetChild(i).gameObject.transform.position = Pos.position;
                    transform.GetChild(i).GetComponentInChildren<HomingMissile>().VsalueList = i-1;
                }
                playerImage.ReturnSkil(1).fillAmount = 0;
            }

            if (Input.GetKeyDown(KeyCode.E) && playerImage.ReturnSkil(3).fillAmount >= 1)
            {
                bust.enabled = true;
                playerImage.ReturnSkil(3).fillAmount = 0;
            }
            if (playerImage.ReturnSkil(4).fillAmount >= 1)
            { 
                if (Input.GetKey(KeyCode.R))
                {
                    LineRenderer Line = transform.parent.GetComponent<LineRenderer>();
                    Vector3[] Linepos = new Vector3[4];
                    if (transform.GetComponent<SpriteRenderer>().flipX)
                    {
                        Linepos[0] = new Vector3(transform.position.x, transform.position.y + LaserWidth, 0) - transform.right; 
                        Linepos[1] = new Vector3(transform.position.x - 30,transform.position.y + LaserWidth, 0) - transform.right; 
                        Linepos[2] = new Vector3(transform.position.x - 30, transform.position.y - LaserWidth, 0) - transform.right; 
                        Linepos[3] = new Vector3(transform.position.x, transform.position.y - LaserWidth, 0) - transform.right; 
                    }
                    else
                    {
                        Linepos[0] = new Vector3(transform.position.x, transform.position.y + LaserWidth, 0)+transform.right;
                        Linepos[1] = new Vector3(transform.position.x + 30, transform.position.y + LaserWidth, 0)  +transform.right;
                        Linepos[2] = new Vector3(transform.position.x + 30, transform.position.y - LaserWidth, 0) + transform.right; 
                        Linepos[3] = new Vector3(transform.position.x, transform.position.y - LaserWidth, 0) + transform.right; 
                    }
                   
                    Line.SetPositions(Linepos);
                    Line.enabled = true;

                    if (LaserWidth < 0.5f)
                        LaserWidth += 0.1f * Time.deltaTime;
                    EneryBoom = true;

                }
                else if (Input.GetKeyUp(KeyCode.R) && playerImage.ReturnSkil(4).fillAmount >= 1)
                {
                    camaraShake.ShakeStart = true;
                    PhysicsLaser(transform.parent.GetComponent<LineRenderer>());
                    GameObject.Find("Main Camera").GetComponent<CameraShake>().SetAmount(LaserWidth / 2.5f);
                    LaserObject.SetActive(true);
                    playerImage.ReturnSkil(4).fillAmount = 0;
                }
               
            }
            else
            {
                LineRenderer Line = transform.parent.GetComponent<LineRenderer>();
                Line.enabled = false;
                LaserWidth = 0;
                EneryBoom = false;
            }

            if (Input.GetKeyDown(KeyCode.U) && playerImage.ReturnSkil(2).fillAmount >=1)
            {
                playerImage.ReturnSkil(4).fillAmount = 0;
                StartCoroutine(skil.ZoomInit());
                transform.GetComponentInParent<CharacterParent>().SetShield(true);
            }

            
            BulletTiming += Time.deltaTime;
        }
    }
    private void OnDestroy()
    {
        FindObjectOfType<StageManager>().GameOver();
        if (transform.parent.GetChild(1).tag != "Player")
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "DownGround")
        {
            JumpCount = 1;
        }
    }

   public void SpeedSet(float Value)
    {
            Speed = Value;
            Debug.Log(Speed);
    }

    public void SpeedReturn()
    {
       Speed /=1.3f;
    }
    

    Vector3 Dir(Vector3 A, Vector3 B)
    {
        Vector3 C = new Vector3(Mathf.Abs(A.x) + Mathf.Abs(B.x), Mathf.Abs(A.y) + Mathf.Abs(B.y));
        return C;
    }

    void PhysicsLaser(LineRenderer PhysicsLaser)
    {
        Vector2 Size = Dir(transform.parent.GetComponent<LineRenderer>().GetPosition(0), transform.parent.GetComponent<LineRenderer>().GetPosition(2));
        GameObject Laser = Instantiate(LaserObject);
        Laser.GetComponent<TrailRenderer>().startWidth = Laser.GetComponent<TrailRenderer>().endWidth = Size.y;
        RaycastHit2D[] hit = Physics2D.BoxCastAll(PhysicsLaser.GetPosition(0), Size, 0, PhysicsLaser.GetPosition(2));
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i])
            {
                switch (hit[i].transform.gameObject.tag)
                {
                    case "Bullet":
                        if (hit[i].transform.parent.GetChild(0).name != "PlayerImage"&&hit[i].transform.parent.GetComponent<CharacterParent>().GetCharacter(hit[i].transform.gameObject) != CharacterList.PlayerCharacter)
                        {
                            hit[i].transform.gameObject.SetActive(false);
                        }
                        break;
                    case "Enemy":
                        hit[i].transform.parent.GetComponent<CharacterParent>().Damage(450 + (LaserWidth * 240 * 10), DefList.Energy);
                        break;
                    default:
                        break;
                }
            }
        }

    }

    
  

   
}



