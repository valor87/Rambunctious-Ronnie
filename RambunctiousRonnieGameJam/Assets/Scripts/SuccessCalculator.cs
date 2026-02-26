using UnityEngine;
using static Trait;
using System.Collections.Generic;

public class SuccessCalculator : MonoBehaviour
{
    GameManager gameManager;
    EventCore eventCore;

    [Header("References")]
    public GameObject characterObj;
    public CharacterStatMenu characterStatMenu;
    [Space(20)]
    [Tooltip("The base chance of success. Should be 20%.")]
    public float baseChance = 0.2f;

    [Header("Chance Conversions")]
    [Tooltip("How much the trait chance should convert over to the actual success chance. " +
        "\n(For example, if set to 40%, 100% trait chance is 40% actual chance. 50% trait chance is 20% actual chance.) " +
        "\nWill not go over its value specified at 100%. (so if trait chance was 200%, it will still be 40% actual chance, not 80%.) " +
        "\n\nShould be 40%.")]
    public float traitChanceConversion = 0.4f;
    [Tooltip("How much the body part chance should convert over to the actual success chance. " +
        "\nShould be 40%." +
        "\n\n(Look at traitChanceConversion's tooltip for a more in-depth explanation)")]
    public float bodyPartChanceConversion = 0.4f;

    [Header("Chance Steps")]
    [Tooltip("How much each positive trait should increase the trait chance. Should be 40%.")]
    public float traitChanceUpStep = 0.4f;
    [Tooltip("How much each negative trait should decrease the trait chance. Should be 20%.")]
    public float traitChanceDownStep = 0.2f;
    [Tooltip("How much each positive body part should increase the body part chance. Should be 1f/6f% (16.67~%).")]
    public float bodyPartChanceUpStep = 1f/6f;

    float traitChance;
    float bodyPartChance;
    float actualChance;

    List<LimbCharacter> correspondingGenresToLimbTypes = new List<LimbCharacter>() {
        LimbCharacter.None, //genre: none
        LimbCharacter.Spiky, //genre: LateNightTalkShow
        LimbCharacter.Spiky, //genre: Improv
        LimbCharacter.Thin, //genre: MysteryThriller
        LimbCharacter.Buff, //genre: PoliceShow
        LimbCharacter.Buff, //genre: ActionShow
        LimbCharacter.Buff, //genre: RealitySurvivalShow
        LimbCharacter.Curvy, //genre: DatingCompetition
        LimbCharacter.Thin, //genre: Horror
        LimbCharacter.Curvy, //genre: Romcom
        LimbCharacter.Curvy, //genre: Drama
        LimbCharacter.Thin, //genre: Sitcom
        LimbCharacter.Spiky //genre: MonsterFeature

    };
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();

        eventCore.calculateSuccessChanceEV.AddListener(CalculateActualChance);
        eventCore.setNewCharacterEV.AddListener(SetNewCharacter);
        eventCore.followApproveCharacterEV.AddListener(DetermineSuccess);

        GameObject canvas = GameObject.Find("Canvas");
        if (characterStatMenu == null)
        {
            characterStatMenu = canvas.transform.Find("CharacterStatsMenu").GetComponent<CharacterStatMenu>();
            if (characterStatMenu == null)
            {
                Debug.LogError("Could not find the character stat menu in the canvas.");
            }
        }
    }

    void SetNewCharacter(GameObject newCharacterObj)
    {
        characterObj = newCharacterObj;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterObj != null)
            CalculateActualChance();
    }

    void CalculateActualChance()
    {
        CharacterValues characterObjValues = characterObj.GetComponent<CharacterValues>();
        Character characterData = characterObjValues.CharactersValues;
        Genres currentGenre = gameManager.currentShowGenre;
        traitChance = 0;
        float displayedTraitChance = 0;
        bodyPartChance = 0;
        actualChance = 0;

        foreach (Trait trait in characterData.traitList)
        {
            if (trait.positiveGenres.Contains(currentGenre))
            {
                traitChance += traitChanceUpStep;
                if (characterStatMenu.CheckIfTraitIsRevealed(trait))
                {
                    displayedTraitChance += traitChanceUpStep;
                    print($"Trait {trait} is revealed! displayed trait chance is now {displayedTraitChance}");
                }

                continue;
            }

            if (trait.negativeGenres.Contains(currentGenre))
            {
                traitChance -= traitChanceDownStep;
                if (characterStatMenu.CheckIfTraitIsRevealed(trait))
                {
                    displayedTraitChance -= traitChanceDownStep;
                    print($"Trait {trait} is revealed! displayed trait chance is now {displayedTraitChance}");
                }
            }
        }

        foreach (GameObject bodyPart in characterObjValues.ChildObjectsBodyParts)
        {
            LimbClassification limb = bodyPart.GetComponent<LimbClassification>();
            int currentGenreId = (int)currentGenre;

            if (correspondingGenresToLimbTypes[currentGenreId] == limb.LimbType)
            {
                bodyPartChance += bodyPartChanceUpStep;
            }
        }

        if (traitChance > 1)
            traitChance = 1;
        traitChance *= traitChanceConversion;

        if (displayedTraitChance > 1)
            displayedTraitChance = 1;
        displayedTraitChance *= traitChanceConversion;

        if (bodyPartChance > 1)
            bodyPartChance = 1;
        bodyPartChance *= bodyPartChanceConversion;

        actualChance = baseChance + traitChance + bodyPartChance;
        print($"Trait Chance: {traitChance / traitChanceConversion * 100}\nBodypart Chance: {bodyPartChance / bodyPartChanceConversion * 100}\n Actual Chance: {actualChance * 100}");

        float displayedChance = Mathf.Round((baseChance + displayedTraitChance + bodyPartChance) * 100);
        print($"Displayed Chance: {displayedChance}");

        eventCore.updateSuccessChanceEV.Invoke(displayedChance);
    }

    void DetermineSuccess()
    {
        CalculateActualChance();

        float randomNum = Random.Range(0f, 101f) / 100f;
        print($"Num: {randomNum * 100f}");
        if (randomNum <= actualChance)
        {
            eventCore.successfulShowEV.Invoke();
            print("show is success");
        }
        else
        {
            eventCore.failureShowEV.Invoke();
            print("show is failure");
        }
    }
}
