using UnityEngine;
using RTLTMPro;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private RTLTextMeshPro characterName;
    [SerializeField] private RTLTextMeshPro characterDescription;
    [SerializeField] private RTLTextMeshPro characterCost;

    public PlayerTemplate[] characters;
    [SerializeField] private PlayerManager playerManager;
    private int _characterIndex = 0;
    private int _selectedCharactersIndex = 0;

    [SerializeField] private Animator btnBuyAnim;

    private List<int> _ownedCharacterIds;
    
    [SerializeField] private RTLTextMeshPro txtCoin;
    [SerializeField] private Animator coinAnimator;

    private void Start()
    { 
        if (!PlayerPrefs.HasKey("OwnCharacters"))
        {
            PlayerPrefs.SetString("OwnCharacters", "0");
        }
        LoadOwnedCharacters();

        if (!PlayerPrefs.HasKey("_selectedCharactersIndex"))
        {
            PlayerPrefs.SetInt("_selectedCharactersIndex", 0);
        }

        _selectedCharactersIndex = PlayerPrefs.GetInt("_selectedCharactersIndex");
        _characterIndex = GetCharacterIndexById(_selectedCharactersIndex);

        SetCharacter(_characterIndex);
        UpdateButtonState();
    }

    private void Update()
    {
        txtCoin.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    private int GetCharacterIndexById(int characterId)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].characterID == characterId)
            {
                return i;
            }
        }
        return 0;
    }

    private void LoadOwnedCharacters()
    {
        _ownedCharacterIds = new List<int>();
        string[] characterIds = PlayerPrefs.GetString("OwnCharacters").Split(',');
        foreach (string id in characterIds)
        {
            if (int.TryParse(id, out int characterId))
            {
                _ownedCharacterIds.Add(characterId);
            }
        }
    }

    public void NextButton()
    {
        _characterIndex++;
        if (_characterIndex >= characters.Length)
            _characterIndex = 0;
        SetCharacter(_characterIndex);
        UpdateButtonState();
    }

    public void PreviousButton()
    {
        _characterIndex--;
        if (_characterIndex < 0)
            _characterIndex = characters.Length - 1;
        SetCharacter(_characterIndex);
        UpdateButtonState();
    }

    private void SetCharacter(int index)
    {
        _characterIndex = index;
        playerManager.player = characters[_characterIndex];
        playerManager.SetUpPlayer();
        SetUpPlayerAttribute();
    }

    public void BuyOrSelectCharacters()
    {
        if (CheckCharacterIsOwn())
        {
            _selectedCharactersIndex = characters[_characterIndex].characterID;
            PlayerPrefs.SetInt("_selectedCharactersIndex", _selectedCharactersIndex);
            UpdateButtonState();
        }
        else
        {
            if (GameManager.Coin >= characters[_characterIndex].moneyCost)
            {
                GameManager.Coin -= characters[_characterIndex].moneyCost;
                PlayerPrefs.SetInt("Coin",GameManager.Coin);
                _ownedCharacterIds.Add(characters[_characterIndex].characterID);
                SaveOwnedCharacters();
                _selectedCharactersIndex = characters[_characterIndex].characterID;
                PlayerPrefs.SetInt("_selectedCharactersIndex", _selectedCharactersIndex);
                SetCharacter(_characterIndex);
                UpdateButtonState();
            }
            else
            {
                coinAnimator.SetTrigger("NoMoney");
            }
        }
    }

    public void SetSelectedCharacter()
    {
        _characterIndex = GetCharacterIndexById(_selectedCharactersIndex);
        playerManager.player = characters[_characterIndex];
        playerManager.SetUpPlayer();
        SetUpPlayerAttribute();
        UpdateButtonState();
    }

    private void SaveOwnedCharacters()
    {
        string ownedCharactersString = string.Join(",", _ownedCharacterIds);
        PlayerPrefs.SetString("OwnCharacters", ownedCharactersString);
        PlayerPrefs.Save();
    }

    private void UpdateButtonState()
    {
        if (CheckCharacterIsOwn())
        {
            if (PlayerPrefs.GetInt("_selectedCharactersIndex") == characters[_characterIndex].characterID)
            {
                btnBuyAnim.Play("ButtonSelectedStatus"); 
                return;
            }
            btnBuyAnim.Play("ButtonUnSelectStatus");
        }
        else
            btnBuyAnim.Play("ButtonBuyStatus"); 
    }

    private void SetUpPlayerAttribute()
    {
        characterName.text = characters[_characterIndex].name;
        characterDescription.text = characters[_characterIndex].description;
        characterCost.text = characters[_characterIndex].moneyCost.ToString();
    }

    private bool CheckCharacterIsOwn()
    {
        return _ownedCharacterIds.Contains(characters[_characterIndex].characterID);
    }
}
