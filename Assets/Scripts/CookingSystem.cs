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
    private Slot[] slots;

    private bool[] isToSkip;
    private int numberOfMatching;

    public LayerMask rollsOnPan;

    private void Awake()
    {
        instance = this;
        InitRecipe();
    }

    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }

    public void Cook()
    {
        CreateOutput();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(slots[0].roll.theRollType);
        }
    }

    public void InitRecipe()
    {
        recipeDictionary = new Dictionary<Rolls.rollType, Rolls.rollType[]>();

        Rolls.rollType[] recipe = new Rolls.rollType[2];
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
            for (int i = 0; i < _recipeCheck.Length; i++)
            {
                Debug.Log(slots[i].GetRollType());

                for (int j = 0; j < _recipeCheck.Length; j++)
                {
                    if (slots[j].GetRollType() == _recipeCheck[i] && isToSkip[j] != true)
                    {
                        isToSkip[j] = true;
                        numberOfMatching++;
                        if (numberOfMatching == _recipeCheck.Length)
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
            _outputRoll = null;
            Debug.Log("Output is Null");
        }
        else
        {
            _outputRoll = ScriptableObject.CreateInstance<Rolls>();
            _outputRoll.theRollType = recipeOutput;
            Debug.Log("Output is " + _outputRoll.theRollType);

            Collider2D[] _rollsOnPan = Physics2D.OverlapCircleAll(PlayerPanAttack.instance.panPoint.position,
                3f, rollsOnPan);
            foreach (var _roll in _rollsOnPan)
            {
                Debug.Log("HERE to Deactivate the roll");
                _roll.gameObject.SetActive(false);
            }

            Instantiate(RollManager.instance.GetRoll(recipeOutput).rollPrefab,
                PlayerPanAttack.instance.panPoint.position, PlayerPanAttack.instance.panPoint.rotation);

            
        }


    }
}
