using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buff : MonoBehaviour
{
    public string type;
    public float percentage;
    public float duration;
    public float currentTime;
    public Image icon;

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Init(string type, float per, float du)
    {
        this.type = type;
        percentage = per;
        duration = du;
        currentTime = duration;
        icon.fillAmount = 1;

        Execute();
    }
    // �ڷ�ƾ���� ����� ���ð�
    WaitForSeconds seconds = new WaitForSeconds(0.1f);

    public void Execute()
    {
        StartCoroutine(Activation());
    }


    IEnumerator Activation()
    {
        // 0.1�ʿ� 0.1��ŭ icon.fillAmount�� ����
        while(currentTime > 0)
        {
            currentTime -= 0.1f;
            icon.fillAmount -= currentTime;
            yield return seconds;
        }
        icon.fillAmount = 0;
        currentTime = 0;

        DeActivation();
    }

    void DeActivation()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
