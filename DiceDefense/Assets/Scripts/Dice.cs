using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    Red,    //스킬 : 범위공격(레벨마다 범위 증가) , 효과 : 범위공격의 공격력 감소 낮춤 or 공격력 증가
    Blue,   //스킬 : 슬로우(슬로우 된 적 공격시 데미지 증가), 효과 : 슬로우 량 증가
    Green,  //스킬 : 5회 공격속도(레벨마다 공격속도 증가), 효과 : 일정 확률로 추가공격 (모일때 마다 확률 증가 및 타수 증가) or 공격속도 추가 횟수 증가
    Purple, //스킬 : 100% 치명타(레벨마다 치명타 데미지 증가), 효과 : 라운드 종료시 or 적 처치시 일정 금액 획득
    Yellow, //스킬 : 멈춤(멈춘 적 공격시 데미지 증가), 효과 : 멈춤 시간 증가
    None
}

public class Dice : MonoBehaviour
{
    public const int MaxLv = 5;

    public DiceType diceType = DiceType.None;

    private int _damage = 1;
    private int _mp = 0;
    private int _perMP = 4;
    private int _critical = 25;
    
    private float _attackSpeed = 0.7f;

    public int damage;
    public int mp;
    public int perMP;
    public int critical;
    public int addDamage;
    public int addMP;
    public int addPerMP;
    public int addCritical;

    private int lv = 0;

    public float attackSpeed;
    public float addAttackSpeed;

    private Vector3 mousePosition;
    private Vector3 offset;
    private Vector3 pos;

    [SerializeField]
    private SpriteRenderer diceColor;

    [SerializeField]
    private GameObject[] diceLv;

    private IEnumerator coroutine;

    #region MouseControl
    private void OnMouseDown()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(pos, mousePosition) < InGameManager.instance.InfoDistance)
        {
            //TODO 정보띄우기
        }
        else
        {
            Dice collaboDice = null;
            float distance = float.MaxValue;

            int cnt = InGameManager.instance._createdDice.Count;

            for(int i = 0; i < cnt; i++)
            {
                if (InGameManager.instance._createdDice[i] == this)
                    continue;

                var diff = Vector2.Distance(this.transform.position, InGameManager.instance._createdDice[i].transform.position);

                if (diff < distance)
                {
                    distance = diff;
                    collaboDice = InGameManager.instance._createdDice[i];
                }
            }

            if (collaboDice != null && distance < InGameManager.instance.CollaboDistance)
            {
                collaboDice.Collabo();
                InGameManager.instance._createdDice.Remove(this);
                gameObject.SetActive(false);
            }
        }
    }
    #endregion

    private void Start()
    {
        
    }

    private IEnumerator AttackCoroutine()
    {
        float soa = 1 / attackSpeed;

        while (true)
        {
            Enemy e = InGameManager.instance.GetNearEnemy(pos);

            if (e != null)
            {
                Bullet b = ObjectPoolManager.instance.GetBullet();

                if (mp >= 100)
                {
                    mp = 0;
                    b.AddAction(SkillAttack);
                }

                else
                {
                    mp += perMP;
                    b.AddAction(Attack);
                }

                b.transform.position = transform.position;
                b.targetEnemy = e;
                b.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(soa);
        }
    }


    private void Attack(Enemy e)
    {
        bool cri = false;

        int randomCritical = Random.Range(1, 101);
        if(randomCritical <= critical)
        {
            cri = true;
        }

        switch (diceType)
        {
            case DiceType.Red:
                if(cri)
                    e.Hit((damage + UpgradeManager.instance.redDiceUpgrade) * 2);
                else
                    e.Hit(damage + UpgradeManager.instance.redDiceUpgrade);
                break;
            case DiceType.Blue:
                if (cri)
                    e.Hit((damage + UpgradeManager.instance.blueDiceUpgrade) * 2);
                else
                    e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);
                break;
            case DiceType.Green:
                if (cri)
                    e.Hit((damage + UpgradeManager.instance.greenDiceUpgrade) * 2);
                else
                    e.Hit(damage + UpgradeManager.instance.greenDiceUpgrade);
                break;
            case DiceType.Purple:
                if (cri)
                    e.Hit((damage + UpgradeManager.instance.purpleDiceUpgrade) * 2);
                else
                    e.Hit(damage + UpgradeManager.instance.purpleDiceUpgrade);
                break;
            case DiceType.Yellow:
                if (cri)
                    e.Hit((damage + UpgradeManager.instance.yellowDiceUpgrade) * 2);
                else
                    e.Hit(damage + UpgradeManager.instance.yellowDiceUpgrade);
                break;
        }
    }

    private void SkillAttack(Enemy e)
    {
        switch (diceType)
        {
            case DiceType.Red:
                break;
            case DiceType.Blue:
                break;
            case DiceType.Green:
                break;
            case DiceType.Purple:
                break;
            case DiceType.Yellow:
                break;
        }
    }

    public void StopAttackCoroutine()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void Collabo()
    {
        StopAttackCoroutine();

        lv++;

        damage = (int)((Mathf.Pow(lv, 2) * 2) + 1);
        attackSpeed = _attackSpeed - lv * 0.1f;

        int randomType = Random.Range(0, (int)DiceType.None);

        diceType = (DiceType)randomType;

        Color c;

        if (ColorUtility.TryParseHtmlString(InGameManager.instance.DiceColorHexCode[randomType], out c) == false)
        {
            return;
        }

        diceColor.color = c;

        int len = diceLv.Length;
        for (int i = 0; i < len; i++)
        {
            diceLv[i].SetActive(false);
        }

        SpriteRenderer[] srs = diceLv[lv].GetComponentsInChildren<SpriteRenderer>();
        int srLen = srs.Length;
        for (int i = 0; i < srLen; i++)
        {
            srs[i].color = c;
        }

        diceLv[lv].SetActive(true);
    }
}