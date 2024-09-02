using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //���� ���� ����
    [Header("���ݰ���")]
    public long att = 1000;
    public long creatt;
    public float dex = 1000;
    public float cri = 1000;
    [Header("��������")]
    public long hp = 100;
    public long maxHp = 100;
    public float def = 1;
    public Image hp_bar;

    //�˸� ǥ���� Text
    public Text noti;
    public Text attackTxt;//���ݷ�
    public Text hpTxt;//ü��
    public Text defTxt;//����
    public Text dexTxt;//��ø��
    public Text creTxt;//ũ��Ƽ��Ȯ��

    //private ����
    GameObject mob = null;
    Animator anim;
    float currentTime = 0;
    void Start()
    {
        anim = GetComponent<Animator>();


        maxHp = hp;

        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlay)
        {
            anim.SetBool("att", false);
        }else
        {
            //���� ����
            if(currentTime + (2 - (dex * 0.001f)) < Time.time)
            {
                currentTime = Time.time;
                anim.SetBool("att", true);
                int criRan = Random.Range(1, 101);
                if(criRan < (cri * 0.01f))
                {
                    Critical();
                }
                else
                {
                    mob.GetComponent<Monster>().Damage(att);
                }
            }
        }
        //��ø
        anim.speed = dex * 0.001f;
    }

    public void Critical()
    {
        int criRan = Random.Range(1, 10);

        if(criRan < (cri*0.001f))
        {
            creatt = att * criRan;
            mob.GetComponent<Monster>().CriDamage(creatt);
        }
    }


    public void Damage(long monAtt)
    {
        hp_bar.fillAmount = hp/maxHp;

        hp -= (monAtt - (long)(def / 1000));

        if (hp <= 0)
        {
            noti.text = "ü���� 0�� �Ǹ� ���ݼӵ��� ������ ���� �˴ϴ�.";
            dex = 500;
            hp = 0;
            Die();
        }else
        {
            noti.text = "";
        }
    }

    void Die()
    {
        GameManager.instance.isPlay = false;
        anim.SetTrigger("dead");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.isPlay = false;
            mob = collision.gameObject;
        }
    }

    public void AttackUp()
    {
        if(GameManager.instance.money < 1000)
        {
            Debug.Log("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            att += 1000;
            attackTxt.text = "���� ���ݷ� : " + att;
        }
    }

    public void DefenceUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            def += 100;
            defTxt.text = "���� ���� : " + def;
        }
    }

    public void HpUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            hp += 100;

            if(hp >= maxHp)
            {
                maxHp = hp;
            }
        }
        hpTxt.text = "���� ü�� : " + hp;
    }

    public void DexUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            dex += 1;

            if (hp >= maxHp)
            {
                maxHp = hp;
            }
        }
        dexTxt.text = "���� ��ø�� : " + dex;
    }

    public void CreUp()
    {
        if (GameManager.instance.money < 1000)
        {
            Debug.Log("�ݾ��� �����ϴ�");
        }
        else
        {
            GameManager.instance.SetMoney(-1000);
            creatt += 1;
            creTxt.text = "���� ũ��Ƽ�� : " + cri*0.001f;
        }
    }
}
