using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterStatMenu : MonoBehaviour
{
    GameObject panelObj; 
    public TextMeshProUGUI[] traitTexts = new TextMeshProUGUI[3];
    public TextMeshProUGUI successChance;
    public TextMeshProUGUI showNum;
    public TextMeshProUGUI genreTitle;
    public TextMeshProUGUI livesAmount;
    public TextMeshProUGUI charactersAmount;

    EventCore eventCore; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetReferences();
        
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        eventCore.revealTraitEV.AddListener(RevealTrait);
        eventCore.updateSuccessChanceEV.AddListener(UpdateSuccessChance);
        eventCore.updateShowNumEV.AddListener(UpdateShowNum);
        eventCore.updateGenreEV.AddListener(UpdateGenreTitle);
        eventCore.updateLivesAmountEV.AddListener(UpdateLivesAmount);
        eventCore.updateCharactersAmountEV.AddListener(UpdateCharactersAmount);
    }

    void SetReferences()
    {
        print("setting references");
        panelObj = transform.GetChild(0).gameObject;
        for (int i = 0; i < traitTexts.Length; i++)
        {
            if (traitTexts[i] == null)
            {
                traitTexts[i] = panelObj.transform.Find($"Trait{i+1}").GetComponent<TextMeshProUGUI>();

                if (traitTexts[i] == null)
                {
                    Debug.LogError("Could not find trait text in character stats menu.");
                }
            }
        }

        if (successChance == null)
        {
            successChance = panelObj.transform.Find("SuccessChance").GetComponent<TextMeshProUGUI>();

            if (successChance == null)
                Debug.LogError("Could not find success chance text in character stats menu.");
        }

        if (showNum == null)
        {
            showNum = panelObj.transform.Find("ShowNum").GetComponent<TextMeshProUGUI>();

            if (showNum == null)
                Debug.LogError("Could not find show num text in character stats menu.");
        }

        if (genreTitle == null)
        {
            genreTitle = panelObj.transform.Find("GenreTitle").GetComponent<TextMeshProUGUI>();

            if (genreTitle == null)
                Debug.LogError("Could not find genre title text in character stats menu.");
        }

        if (livesAmount == null)
        {
            livesAmount = panelObj.transform.Find("LivesAmount").GetComponent<TextMeshProUGUI>();

            if (livesAmount == null)
                Debug.LogError("Could not find lives amount text in character stats menu.");
        }

        if (charactersAmount == null)
        {
            charactersAmount = panelObj.transform.Find("CharactersAmount").GetComponent<TextMeshProUGUI>();

            if (charactersAmount == null)
                Debug.LogError("Could not find characters amount text in character stats menu.");
        }
    }

    void UpdateShowNum(int showNumber)
    {
        showNum.text = $"Show: {showNumber + 1}";
    }

    void UpdateGenreTitle(string tempGenreTitle)
    {
        genreTitle.text = tempGenreTitle;
    }

    void UpdateSuccessChance(float displayedChance)
    {
        if (!CheckIfAllTraitsRevealed())
        {
            successChance.text = $"Chance of success: {displayedChance}% + ?";
        }
        else
        {
            successChance.text = $"Chance of success: {displayedChance}%";
        }
    }

    void UpdateLivesAmount(int lives)
    {
        livesAmount.text = $"Lives: {lives}";
    }

    void UpdateCharactersAmount(int charactersLeft)
    {
        charactersAmount.text = $"Characters: {charactersLeft}";
    }

    void RevealTrait(Trait selectedTrait)
    {
        for (int i = 0; i < traitTexts.Length; i++)
        {
            if (traitTexts[i].text != "?")
                continue;

            bool isTraitRevealed = CheckIfTraitIsRevealed(selectedTrait);
            if (isTraitRevealed)
                break;

            traitTexts[i].text = selectedTrait.traitName;
        }
    }
    public bool CheckIfTraitIsRevealed(Trait selectedTrait)
    {
        for (int i = 0; i < traitTexts.Length; i++)
        {
            if (traitTexts[i].text == selectedTrait.traitName)
                return true;
        }

        return false;
    }


    bool CheckIfAllTraitsRevealed()
    {
        for (int i = 0; i < traitTexts.Length; i++)
        {
            if (traitTexts[i].text == "?")
                return false;
        }

        return true;
    }
}
