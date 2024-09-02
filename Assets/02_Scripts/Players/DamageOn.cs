using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOn : MonoBehaviour
{
    public GameObject prefabDamage;//canvas프리팹 일반 데미지
    public GameObject criDamage;//크리티컬 데미지 canvas

    public void DamageTxt()
    {
        //생성, 스크립트는 몬스터에 적용
        GameObject inst = Instantiate(prefabDamage, transform);//이렇게만 해도 자식으로 생성 가능

        //inst.transform.SetParent(transform); << 이렇게도 가능
    }
    public void CriDamageTxt()
    {
        GameObject inst = Instantiate(criDamage, transform);
        inst.transform.SetParent(transform);
    }
}
