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

    // rollType body�� ������ Ž��
    // 3���� body�� ��� ���� rollName���� Ž��
    // rollType soul�� ������ Ž��
    public void CreateRollOutput()
    {
        // pan�� �ö�� �ִ� body�� � body type���� ��ġ�ϴ���
        _rollNameOnPan = inventory.InputSlots[0].GetRoll().rollSo.rollType;
        
        // body ����, Ÿ��
        // recipe���� �˻��ؼ� output roll ã�Ƴ���
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
        // pan�� �ö�� �ִ� flavor�� � flavor type���� ��ġ�ϴ���
        _flavorNameOnPan = inventory.InputSlots[0].GetFlavor().flavorSo.flavorType;

        // flavor ����, Ÿ�� 
        // recipe���� �˻��ؼ� output flavor ã�Ƴ���
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
