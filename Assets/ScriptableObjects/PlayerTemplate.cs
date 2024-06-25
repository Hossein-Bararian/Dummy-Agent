using UnityEngine;

[CreateAssetMenu(menuName = "PlayerTemplate", fileName = "New Player")]
public class PlayerTemplate : ScriptableObject
{
    [SerializeField] public string englishName;
    [SerializeField] public new string name;
    [SerializeField] public string description;
    [SerializeField] public int moneyCost;

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