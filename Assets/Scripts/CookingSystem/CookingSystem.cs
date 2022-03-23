using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실질적인 Roll, Flavor 생성
/// </summary>
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

    [Header("Frying Pan")]
    public GameObject roll_Slot;
    public GameObject flavor_Slot;
    SpriteRenderer _rollSprite;
    SpriteRenderer _flavorSprite;
    Color _color;

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
        
        _rollSprite = roll_Slot.GetComponent<SpriteRenderer>();
        _color = _rollSprite.color;
        _color.a = 0;
        _rollSprite.color = _color;

        _flavorSprite = flavor_Slot.GetComponent<SpriteRenderer>();
        _color = _flavorSprite.color;
        _color.a = 0;
        _rollSprite.color = _color;
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
        }
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

                // 후라이팬의 Roll Slot의 Sprite를 output sprite로 교체
                // roll아웃풋이 none이 아니라면 outputSO에서 sprite를 꺼내오고 ROll Slot의 알파값은 1로
                _rollSprite.sprite = outputRoll.rollSprite[inventory.numberOfRolls - 1];
                _color.a = 1;
                _rollSprite.color = _color;
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

                // 후라이팬의 Flavor Slot의 Sprite를 output sprite로 교체
                _flavorSprite.sprite = outputFlavor.flavorSprite[inventory.numberOfFlavors - 1];
                _color.a = 1;
                _rollSprite.color = _color;
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
