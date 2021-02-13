using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DiceType
{
    Red,    //스킬 : 범위공격(레벨마다 범위 증가) , 효과 : 범위공격의 공격력 감소 낮춤 or 공격력 증가
    Blue,   //스킬 : 슬로우(슬로우 된 적 공격시 데미지 증가), 효과 : 슬로우 량 증가
    Green,  //스킬 : 5회 공격속도(레벨마다 공격속도 증가), 효과 : 일정 확률로 추가공격 (모일때 마다 확률 증가 및 타수 증가) or 공격속도 추가 횟수 증가
    Purple, //스킬 : 100% 치명타(레벨마다 치명타 데미지 증가), 효과 : 라운드 종료시 or 적 처치시 일정 금액 획득
    Yellow, //스킬 : 멈춤(멈춘 적 공격시 데미지 증가), 효과 : 멈춤 시간 증가
    None
}

public abstract class Dice : MonoBehaviour
{
    public const int MaxLv = 5;
    private const int MaxMP = 10;

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

    private Vector3 _mousePosition;
    private Vector3 _offset;

    public Vector2 pos;

    [SerializeField]
    private SpriteRenderer _diceColor;

    [SerializeField]
    private GameObject[] _diceLv;

    [SerializeField]
    private Image _image_MP;

    private IEnumerator _coroutine;

    #region MouseControl
    private void OnMouseDown()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - _mousePosition;
    }

    private void OnMouseDrag()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePosition + _offset;
    }

    private void OnMouseUp()
    {
        if (Vector2.Distance(pos, _mousePosition) < InGameManager.instance.InfoDistance)
        {
            //TODO 정보띄우기
            transform.position = pos;
        }
        else
        {
            Dice collaboDice = null;
            float distanceToDice = float.MaxValue;

            int cnt = InGameManager.instance.createdDice.Count;

            for(int i = 0; i < cnt; i++)
            {
                if (InGameManager.instance.createdDice[i] == this)
                    continue;

                var diff = Vector2.Distance(this.transform.position, InGameManager.instance.createdDice[i].transform.position);

                if (diff < distanceToDice)
                {
                    distanceToDice = diff;
                    collaboDice = InGameManager.instance.createdDice[i];
                }
            }

            if (collaboDice != null && distanceToDice < InGameManager.instance.CollaboDistance)
            {
                if(collaboDice.diceType == diceType && collaboDice.lv == lv)
                {
                    collaboDice.Collabo();
                    InGameManager.instance.createdDice.Remove(this);
                    InGameManager.instance.createPositionList.Add(pos);
                    gameObject.SetActive(false);
                }
                else
                {
                    Vector2 collaboDicePos = collaboDice.pos;
                    
                    collaboDice.transform.position = pos;
                    collaboDice.pos = pos;

                    transform.position = collaboDicePos;
                    pos = collaboDicePos;
                }
            }
            else
            {
                Vector2? targetPos = null;

                int positionCnt = InGameManager.instance.createPositionList.Count;
                float distanceToPos = float.MaxValue;

                for (int i = 0; i < positionCnt; i++)
                {
                    var diff = Vector2.Distance(this.transform.position, InGameManager.instance.createPositionList[i]);

                    if (diff < distanceToPos)
                    {
                        distanceToPos = diff;
                        targetPos = InGameManager.instance.createPositionList[i];
                    }
                }

                if(targetPos != null && distanceToPos < InGameManager.instance.CollaboDistance)
                {
                    Vector2 tp = (Vector2)targetPos;

                    InGameManager.instance.createPositionList.Add(pos);
                    InGameManager.instance.createPositionList.Remove(tp);
                    transform.position = tp;
                }
                else
                {
                    transform.position = pos;
                }
            }
        }
    }
    #endregion

    private void Start()
    {
        ResetDiceAbility();
    }

    private IEnumerator AttackCoroutine()
    {
        float soa = 1 / attackSpeed;

        while (true)
        {
            yield return new WaitForSeconds(soa);

            Enemy e = InGameManager.instance.GetNearEnemy(pos);

            if (e != null)
            {
                Bullet b = ObjectPoolManager.instance.GetBullet();

                Color c;
                if (ColorUtility.TryParseHtmlString(InGameManager.instance.DiceColorHexCode[(int)diceType], out c))
                {
                    b.SetBulletColor(c);
                }

                if (mp >= MaxMP)
                {
                    mp = 0;
                    b.AddAction(SkillAttack);
                }

                else
                {
                    mp += perMP;
                    b.AddAction(Attack);
                }

                _image_MP.fillAmount = mp / (float)MaxMP;
                b.transform.position = transform.position;
                b.targetEnemy = e;
                b.gameObject.SetActive(true);
            }
        }
    }

    private void ResetDiceAbility()
    {
        damage = _damage;
        mp = _mp;
        perMP = _perMP;
        critical = _critical;
        attackSpeed = _attackSpeed;
    }

    public abstract void Attack(Enemy e);
    public abstract void SkillAttack(Enemy e);

    //private void Attack(Enemy e)
    //{
    //    bool cri = false;

    //    int randomCritical = Random.Range(1, 101);
    //    if(randomCritical <= critical)
    //    {
    //        cri = true;
    //    }

    //    switch (diceType)
    //    {
    //        case DiceType.Red:
    //            if(cri)
    //                e.Hit((damage + UpgradeManager.instance.redDiceUpgrade) * 2);
    //            else
    //                e.Hit(damage + UpgradeManager.instance.redDiceUpgrade);
    //            break;
    //        case DiceType.Blue:
    //            if (cri)
    //                e.Hit((damage + UpgradeManager.instance.blueDiceUpgrade) * 2);
    //            else
    //                e.Hit(damage + UpgradeManager.instance.blueDiceUpgrade);
    //            break;
    //        case DiceType.Green:
    //            if (cri)
    //                e.Hit((damage + UpgradeManager.instance.greenDiceUpgrade) * 2);
    //            else
    //                e.Hit(damage + UpgradeManager.instance.greenDiceUpgrade);
    //            break;
    //        case DiceType.Purple:
    //            if (cri)
    //                e.Hit((damage + UpgradeManager.instance.purpleDiceUpgrade) * 2);
    //            else
    //                e.Hit(damage + UpgradeManager.instance.purpleDiceUpgrade);
    //            break;
    //        case DiceType.Yellow:
    //            if (cri)
    //                e.Hit((damage + UpgradeManager.instance.yellowDiceUpgrade) * 2);
    //            else
    //                e.Hit(damage + UpgradeManager.instance.yellowDiceUpgrade);
    //            break;
    //    }
    //}

    //private void SkillAttack(Enemy e)
    //{
    //    switch (diceType)
    //    {
    //        case DiceType.Red:
    //            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
    //            Collider2D[] enemys = Physics2D.OverlapCircleAll(e.transform.position, 1f, layerMask);

    //            int len = enemys.Length;

    //            for(int i = 0; i < len; i++)
    //            {
    //                enemys[i].GetComponent<Enemy>().Hit(damage + UpgradeManager.instance.redDiceUpgrade);
    //            }
    //            break;
    //        case DiceType.Blue:
    //            e.Slow(1f);
    //            break;
    //        case DiceType.Green:
    //            break;
    //        case DiceType.Purple:
    //            break;
    //        case DiceType.Yellow:
    //            break;
    //    }
    //}

    public void StopAttackCoroutine()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
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

        _diceColor.color = c;

        int len = _diceLv.Length;
        for (int i = 0; i < len; i++)
        {
            _diceLv[i].SetActive(false);
        }

        SpriteRenderer[] srs = _diceLv[lv].GetComponentsInChildren<SpriteRenderer>();
        int srLen = srs.Length;
        for (int i = 0; i < srLen; i++)
        {
            srs[i].color = c;
        }

        _diceLv[lv].SetActive(true);
    }

    public void ResetDiceOnRoundEnd()
    {
        StopAttackCoroutine();
        ResetDiceAbility();
    }

    public void StartAttackCoroutine()
    {
        _coroutine = AttackCoroutine();
        StartCoroutine(_coroutine);
    }
}