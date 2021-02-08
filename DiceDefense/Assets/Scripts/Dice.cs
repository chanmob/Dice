using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiceType
{
    Red,
    Blue,
    Green,
    Purple,
    Yellow,
    None
}

public class Dice : MonoBehaviour
{
    public const int MaxLv = 5;

    public DiceType diceType = DiceType.None;

    private int _damage;
    private int _mp;
    private int _perMP;
    private int _critical;
    
    private float _attackSpeed;

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
                //TODO 합체
                collaboDice.Collabo();
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

            }

            yield return new WaitForSeconds(soa);
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