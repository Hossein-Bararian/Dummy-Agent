using UnityEngine;

[CreateAssetMenu(menuName = "PlayerTemplate", fileName = "New Player")]
public class PlayerTemplate : ScriptableObject
{
    public int characterID;
    
    public string englishName;
    public new string name;
    public string description;
    public int moneyCost;

    public Sprite head;
    public Sprite body;
    public Sprite eye;
    public Sprite mouth;
    public Sprite eyBrow;
    public Sprite upHand;
    public Sprite lowHand;
    public Sprite hand;
    public Sprite upLeg;
    public Sprite lowLeg;
    public Sprite leg;
}