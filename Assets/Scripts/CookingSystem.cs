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

    private bool[] isToSkip;
    private int numberOfMatching;

    public LayerMask rollsOnPan;
    private Inventory _inventory;

    private void Awake()
    {
        instance = this;
        InitRecipe();
    }

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
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
        recipe[0] = Rolls.rollType.ProjectileF; recipe[1] = Rolls.rollType.None;
        recipeDictionary[Rolls.rollType.ProjectileF] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF; recipe[1] = Rolls.rollType.ProjectileF;
        recipeDictionary[Rolls.rollType.BombF01] = recipe;

        recipe = new Rolls.rollType[2];
        recipe[0] = Rolls.rollType.GoulF; recipe[1] = Rolls.rollType.GoulF;
        recipeDictionary[Rolls.rollType.GoulF02] = recipe;
    }

    public Rolls.rollType GetRecipeOutput()
    {
        isToSkip = new bool[PlayerPanAttack.instance.capturableAmount];
        numberOfMatching = 0;

        foreach (Rolls.rollType _recipeRollType in recipeDictionary.Keys)
        {
            Rolls.rollType[] _recipeCheck = recipeDictionary[_recipeRollType];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (_inventory.slots[i].GetRollType() == _recipeCheck[i] && isToSkip[j] != true)
                    {
                        isToSkip[j] = true;
                        numberOfMatching++;
                        if (numberOfMatching == 2)
                        {
                            Inventory.instance.ClearInventory();

                            return _recipeRollType;
                        }
                        break;
                    }
                }
            }
        }
        return Rolls.rollType.None;
    }

    public void CreateOutput()
    {
        Rolls.rollType recipeOutput = GetRecipeOutput();
        if (recipeOutput == Rolls.rollType.None)
        {
            //레시피에 없는 조합이라면 null
            _outputRoll = null;
        }
        else
        {
            //SO를 생성하고 RollManager에서 rollType에 해당하는 roll을 불러와서 연결해준다
            _outputRoll = ScriptableObject.CreateInstance<Rolls>();
            _outputRoll.theRollType = recipeOutput;

            //프라이팬 위의 다른 roll들을 제거한다
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

            //roll 프리펩을 생성하고 UI roll 이미지도 인벤토리에 배치한다
            Instantiate(RollManager.instance.GetRoll(recipeOutput).rollPrefab,
                PlayerPanAttack.instance.panPoint.position, PlayerPanAttack.instance.panPoint.rotation);
            _inventory.AcquireRolls(RollManager.instance.GetRoll(recipeOutput));
            _inventory.AcquireOutPutUI(RollManager.instance.GetRoll(recipeOutput));

        }


    }
}
