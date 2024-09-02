using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    //오브젝트 풀링
    public List<GameObject> mobPool = new List<GameObject>();
    //담을 오브젝트 배열
    public GameObject[] mobs;

    public int objCnt = 1;

    private void Awake()
    {
        for(int i = 0; i < mobs.Length; i++)
        {
            for(int j = 0; j<objCnt; j++)
            {
                mobPool.Add(CreateObj(mobs[i], transform));
            }
        }
    }

    GameObject CreateObj(GameObject obj, Transform parent)
    {
        GameObject copy = Instantiate(obj);
        copy.transform.SetParent(parent);

        copy.SetActive(false);

        return copy;
    }

    void Start()
    {
        //랜덤 오브젝트풀 코루틴
        StartCoroutine(CreateMob());
    }

    //오브젝트 풀링에서 인덱스가 뽑힌애를 켜줌
    IEnumerator CreateMob()
    {
        while (true)
        {
            mobPool[DeactiveMob()].SetActive(true);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }


    //비활성화된 오브젝트 랜덤 인덱스 뽑아냄
    int DeactiveMob()
    {
        List<int> num = new List<int>();

        for(int i = 0; i < mobPool.Count; i++)
        {
            //activeSelf를 모른다면
            //만약 리스트에 비활성화된 프리팹이 있다면
            if (!mobPool[i].activeSelf)
            {
                num.Add(i);
            }
        }

        int x = 0;

        if(num.Count > 0)
        {
            x = num[Random.Range(0, num.Count)];
        }
        return x;
    }


    void Update()
    {

    }

}
