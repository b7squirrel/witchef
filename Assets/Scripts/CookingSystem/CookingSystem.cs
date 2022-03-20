using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : MonoBehaviour
{
    public static CookingSystem instance;

    Inventory inventory;
    RecipeRoll myRecipeRoll;
    RecipeFlavor myRecipeFlavor;
    RollSO outputRoll;
    FlavorSo outputFlavor;

    Roll.rollType _rollNameOnPan;
    Flavor.flavorType _flavorNameOnPan;

    public Transform panPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        myRecipeRoll = GetComponent<RecipeRoll>();
        myRecipeFlavor = GetComponent<RecipeFlavor>();
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
                Instantiate(outputRoll.rollPrefab[inventory.numberOfRolls - 1], panPoint.position, Quaternion.identity);
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
                var _flavor = Instantiate(outputFlavor.flavorPrefab[inventory.numberOfFlavors - 1], panPoint.position, Quaternion.identity);
                
                return;
            }
        }
    }

}
