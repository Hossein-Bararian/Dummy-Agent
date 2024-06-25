using System;
using UnityEngine;
using RTLTMPro;
using Unity.VisualScripting;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private RTLTextMeshPro characterName; 
    [SerializeField] private RTLTextMeshPro characterDescription; 
    [SerializeField] private RTLTextMeshPro characterCost; 
    
    [SerializeField] private PlayerTemplate[] characters;
    [SerializeField] private PlayerManager playerManager;
    private int _characterIndex=0;

    private void Start()
    {
        playerManager.SetUpPlayer();
        SetUpPlayerAttribute();
    }

    public void NextButton()
    {
        _characterIndex++;
        if (_characterIndex >= characters.Length)
            _characterIndex = 0;
        playerManager.player = characters[_characterIndex];
        playerManager.SetUpPlayer();
        SetUpPlayerAttribute();
    }
    public void PreviousButton()
    {
        _characterIndex--;
        if (_characterIndex < 0)
            _characterIndex = characters.Length-1;
        playerManager.player = characters[_characterIndex];
        playerManager.SetUpPlayer();
        SetUpPlayerAttribute();
    }

    private void SetUpPlayerAttribute()
    {
        characterName.text=characters[_characterIndex].name;
        characterDescription.text= characters[_characterIndex].description;
        characterCost.text= characters[_characterIndex].moneyCost.ToString();
    }
}
