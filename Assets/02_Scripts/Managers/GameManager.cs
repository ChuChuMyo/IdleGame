using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float gameSpeed = 3f;
    public bool isPlay = true;//적과 마주칠경우 멈춤

    public float money = 1000;
    public Text moneyText;

    public static GameManager instance;
    void Start()
    {
        instance = this;//싱글톤
    }


    void Update()
    {
        if (isPlay)
        {
            gameSpeed = 3f;
        }
        else//적과 마주쳣을 경우
        {
            gameSpeed = 0f;
        }
    }

    //숫자 카운팅 애니메이션
    IEnumerator Count(float target, float current)
    {
        float duration = 1f;//카운팅에 걸리는 시간
        float offset = (target - current) / duration;//1000/0.5 = 2000

        while(current < target)
        {
            current += offset * Time.deltaTime;

            moneyText.text = string.Format("{0:n0}", (int)current);
            yield return null;
        }

        current = target;

        moneyText.text = string.Format("{0:n0}", (int)current);

    }


    public void SetMoney(float Money)
    {
        money += Money;
        StartCoroutine(Count(money, money - Money));
    }
}
