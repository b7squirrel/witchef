using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : MonoBehaviour
{
    public static CookingSystem instance;

    Inventory inventory;
    RecipeRoll myRecipeRoll;
    RecipeFlavor myRecipeFlavor;
    public RollSO outputRoll;
    public FlavorSo outputFlavor;

    public RollSO defaultRollSo;
    public FlavorSo defaultFlavorSo;

    Roll.rollType _rollNameOnPan;
    Flavor.flavorType _flavorNameOnPan;

    public Transform panPoint;
    public LayerMask rollLayer;
    public LayerMask flavorLayer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        myRecipeRoll = GetComponent<RecipeRoll>();
        myRecipeFlavor = GetComponent<RecipeFlavor>();
        panPoint = FindObjectOfType<PlayerPanAttack>().GetComponent<PlayerPanAttack>().panPoint;
        rollLayer = FindObjectOfType<PlayerPanAttack>().GetComponent<PlayerPanAttack>().rollLayers;
    }

    private void Update()
    {
        Collider2D[] checkRoll = Physics2D.OverlapCircleAll(panPoint.position, .3f, rollLayer);
        if(checkRoll != null)
        {
            int _numberOfRollOnPan = 0;
            foreach (var item in checkRoll)
            {
                _numberOfRollOnPan++;
            }
            Debug.Log("Roll Game Object on the pan = " + _numberOfRollOnPan);
        }
    }

    public void Roll()
    {
        CreateRollOutput();
    }

    public void Flavor()
    {
        CreateFlavorOutput();
    }

    // rollType body의 갯수를 탐색
    // 3개의 body가 모두 같은 rollName인지 탐색
    // rollType soul의 갯수를 탐색
    public void CreateRollOutput()
    {
        // pan에 올라와 있는 body가 어떤 body type으로 일치하는지
        _rollNameOnPan = inventory.InputSlots[0].GetRoll().rollSo.rollType;
        
        // body 갯수, 타입
        // recipe에서 검색해서 output roll 찾아내기
        for (int i = 0; i < myRecipeRoll.recipeRoll.Length; i++)
        {
            if(myRecipeRoll.recipeRoll[i].rollType == _rollNameOnPan)
            {
                outputRoll = myRecipeRoll.recipeRoll[i];
                outputRoll.rollType = myRecipeRoll.recipeRoll[i].rollType;
                Collider2D[] _checkRoll = Physics2D.OverlapCircleAll(panPoint.position, .3f, rollLayer);
                if(_checkRoll != null)
                {
                    foreach (var item in _checkRoll)
                    {
                        Destroy(item.gameObject);
                    }
                }

                GameObject _roll = Instantiate(outputRoll.rollPrefab[inventory.numberOfRolls - 1], panPoint.position, Quaternion.identity);
                return;
            }
        }
    }

    public void CreateFlavorOutput()
    {
        // pan에 올라와 있는 flavor가 어떤 flavor type으로 일치하는지
        _flavorNameOnPan = inventory.InputSlots[0].GetFlavor().flavorSo.flavorType;

        // flavor 갯수, 타입 
        // recipe에서 검색해서 output flavor 찾아내기
        for (int i = 0; i < myRecipeFlavor.recipeFlavor.Length; i++)
        {
            if (myRecipeFlavor.recipeFlavor[i].flavorType == _flavorNameOnPan)
            {
                outputFlavor = myRecipeFlavor.recipeFlavor[i];
                outputFlavor.flavorType = myRecipeFlavor.recipeFlavor[i].flavorType;

                Collider2D[] _checkFlavor = Physics2D.OverlapCircleAll(panPoint.position, .3f, flavorLayer);
                if (_checkFlavor != null)
                {
                    foreach (var item in _checkFlavor)
                    {
                        Destroy(item.gameObject);
                    }
                }

                GameObject _flavor = Instantiate(outputFlavor.flavorPrefab[inventory.numberOfFlavors - 1], panPoint.position, Quaternion.identity);
                return;
            }
        }
    }

    public void ResetOutputs()
    {
        inventory.numberOfFlavors = 0;
        inventory.numberOfRolls = 0;
    }
}
