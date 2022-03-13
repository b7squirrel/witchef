using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : MonoBehaviour
{
    public static CookingSystem instance;

    private Dictionary<Rolls.rollType, Rolls.rollType[]> recipeDictionary;

    private Rolls[] rollArray;
    private Rolls _outputRoll;

    public Rolls defaultRoll;
    public GameObject defaultRollPrefab;

    private bool[] isToSkiInputp;
    private bool[] isToSkipRecipeSlot;
    private int numberOfMatching;

    public LayerMask rollsOnPan;
    private Inventory _inventory;

    private void Awake()
    {
        instance = this;
        InitRecipe();
        isToSkiInputp = new bool[2];
        isToSkipRecipeSlot = new bool[2];
        InitIsToSkip();
    }

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            foreach (Rolls.rollType _recipeRollType in recipeDictionary.Keys)
            {
                Debug.Log("================= Recipe key = " + _recipeRollType);
                Rolls.rollType[] _recipeCheck = recipeDictionary[_recipeRollType];
                for (int i = 0; i < 2; i++)
                {
                    Debug.Log(_recipeCheck[i]);
                }
            }
        }
    }


    public void Cook()
    {
        CreateOutput();
    }
    public void InitRecipe()
    {
        recipeDictionary = new Dictionary<Rolls.rollType, Rolls.rollType[]>();

        Rolls.rollType[] recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF; recipe[1] = Rolls.rollType.None;
        recipeDictionary[Rolls.rollType.GoulF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.BombF01] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.BombF01; recipe[1] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.BombF02] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.BombF02; recipe[1] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.BombF03] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF; recipe[1] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.GoulF02] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF02; recipe[1] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.GoulF03] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockF; recipe[1] = Rolls.rollType.None;
        recipeDictionary[Rolls.rollType.WarlockF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF02; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.BombF02T] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF03; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.BombF03T] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockF; recipe[1] = Rolls.rollType.WarlockF;
        recipeDictionary[Rolls.rollType.WarlockF02] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockF; recipe[1] = Rolls.rollType.WarlockFF;
        recipeDictionary[Rolls.rollType.WarlockF03] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockF; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.WarlockFF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockFF; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.WarlockFFF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.WarlockFFF; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.WarlockFFFF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.ProjectileF; recipe[1] = Rolls.rollType.None;
        recipeDictionary[Rolls.rollType.None] = recipe;
    }

    public Rolls.rollType GetRecipeOutput()
    {
        foreach (Rolls.rollType _recipeRollType in recipeDictionary.Keys)
        {
            for (int i = 0; i < 2; i++)
            {
                isToSkiInputp[i] = false;
                isToSkipRecipeSlot[i] = false;
            }
            numberOfMatching = 0;
            Debug.Log(">>>>>>>>>>>>>>>>>>�����Ǵ� " + _recipeRollType);

            Rolls.rollType[] _recipeCheck = recipeDictionary[_recipeRollType];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (isToSkipRecipeSlot[i] == false)
                    {
                        if (isToSkiInputp[j] == false)
                        {
                            Debug.Log("������ ���� " + i + " " + _recipeCheck[i]
                                + " �� input ���� " + j + " " + _inventory.InputSlots[j].GetRollType() + " �񱳰�� : ");

                            if (_inventory.InputSlots[j].GetRollType() == _recipeCheck[i])
                            {
                                Debug.Log("��ġ��");
                                isToSkiInputp[j] = true;
                                isToSkipRecipeSlot[i] = true;
                                Debug.Log("is to skip input [" + j + "]" + isToSkiInputp[j]);
                                Debug.Log("is to skip recipe slot [" + i + "]" + isToSkiInputp[i]);
                                numberOfMatching++;
                                if (numberOfMatching == 2)
                                {
                                    return _recipeRollType;
                                }
                            }
                            else
                            {
                                Debug.Log("��ġ���� ����");
                            }
                        }
                    }
                }
            }
        }
        return Rolls.rollType.None;
    }

    public void CreateOutput()
    {
        Rolls.rollType recipeOutput = GetRecipeOutput();
        recipeOutput = ReviseOverlap(recipeOutput);
        Debug.Log("�׷��Ƿ� recipe output �� Ű ���� " + recipeOutput + "�Դϴ�.");

        if (recipeOutput == Rolls.rollType.None)
        {
            //�����ǿ� ���� �����̶�� null
            Debug.Log("���� �����Դϴ�.");
            _outputRoll = null;
        }
        else
        {
            //SO�� �����ϰ� RollManager���� rollType�� �ش��ϴ� roll�� �ҷ��ͼ� �������ش�
            _outputRoll = ScriptableObject.CreateInstance<Rolls>();
            Rolls clone = RollManager.instance.GetRoll(recipeOutput);
            

            _outputRoll = clone;
            _outputRoll.theRollType = clone.theRollType;
            _outputRoll.rollSprite = clone.rollSprite;
            _outputRoll.rollPrefab = clone.rollPrefab;
            Debug.Log("==============================================");
            Debug.Log("==============================================");
            Debug.Log("==============================================");
            Debug.Log("������ output Roll Ÿ���� " + _outputRoll);

            //�������� ���� �ٸ� roll���� �����Ѵ�
            Collider2D[] _rollsOnPan = Physics2D.OverlapCircleAll(PlayerPanAttack.instance.panPoint.position,
                3f, rollsOnPan);
            foreach (var _roll in _rollsOnPan)
            {
                if(_roll != null)
                {
                    //_roll.gameObject.SetActive(false);
                    _roll.GetComponent<EnemyRolling>().DestroyPrefab();
                }
            }

            //roll �������� �����ϰ� UI roll �̹����� �κ��丮�� ��ġ�Ѵ�
            Instantiate(_outputRoll.rollPrefab,
                PlayerPanAttack.instance.panPoint.position, PlayerPanAttack.instance.panPoint.rotation);
            _inventory.GetOutputRoll(_outputRoll);
        }
    }
    
    private void InitIsToSkip()
    {
        for (int i = 0; i < 2; i++)
        {
            isToSkiInputp[i] = false;
            isToSkipRecipeSlot[i] = false;
        }
    }

    private Rolls.rollType ReviseOverlap(Rolls.rollType _key)
    {
        if(_key == Rolls.rollType.BombF02T)
        {
            _key = Rolls.rollType.BombF02;
        }
        if(_key == Rolls.rollType.BombF03T)
        {
            _key = Rolls.rollType.BombF03;
        }
        Debug.Log("Ű ���� " + _key + "�� �ٲ�����ϴ�.");
        return _key;
    }
}
