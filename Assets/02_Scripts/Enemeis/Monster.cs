using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public long att = 10;
    public long hp = 100;//몬스터 HP
    long oriHp;

    public Vector2 startPos;

    Animator anim;
    public float curTime;


    GameObject Player;
    //동전 프리팹
    //나올 위치
    public GameObject money;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        //시작하자 마자 hp값을 담아야함
        //몬스터의 체력은 플레이어의 공격 등으로 이후 변할 예정이라
        //담는 이유는 나중에 리스폰 될 때 
        oriHp = hp;
        target = GameObject.Find("Gold").transform;
        //Debug.Log(target.position);

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            transform.Translate(Vector2.left * Time.deltaTime * GameManager.instance.gameSpeed);

            if(transform.position.x < -16)
            {
                gameObject.SetActive(false);//오브젝트 풀 용SetActive

                transform.position = startPos;//처음 위치로 초기화
            }
        }else
        {
            if (curTime >= 1)
            {
                float dis = Vector3.Distance(Player.transform.position, transform.position);

                if (dis <= 3)
                {
                    anim.SetBool("att", true);
                    Player.GetComponent<PlayerController>().Damage(att);
                    curTime = 0;
                }
            }
        }
    }
    
    public void Damage(long pAtt)
    {
        hp -= pAtt;//매개변수att(Player 공격력)를 빼줌

        if (hp <= 0)//몬스터의 체력이 0보다 작거나 같을경우
        {
            int randCount = Random.Range(5, 10);
            for (int i = 0; i < randCount; i++)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
                GameObject itemFx = Instantiate(money, screenPos, Quaternion.identity);
                itemFx.transform.SetParent(GameObject.Find("Canvas").transform);
                itemFx.GetComponent<ItemFx>().Explosion(screenPos, target.position, 150f);
            }

            gameObject.SetActive(false);//죽음 표시 꺼주는걸로
            transform.position = startPos;//다시 처음 시작 지점으로 이동
            hp = oriHp;//체력 다시 처음 HP로 
            GameManager.instance.isPlay = true;//플레이를 다시 트루로 해줘서 플레이 속행
            GameManager.instance.SetMoney(Random.Range(50,100));
        }
        else
        {
            //데미지 텍스트 처리
            DamageOn damageTxt = GetComponent<DamageOn>();
            damageTxt.DamageTxt();
        }
    }
    public void CriDamage(long criAtt)
    {
        hp -= criAtt;

        //크리티컬 데미지 처리
        DamageOn damgeTxt = GetComponent<DamageOn>();
        damgeTxt.CriDamageTxt();
    }

}
