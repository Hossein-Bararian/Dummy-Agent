using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool IsDead;
    public PlayerTemplate player;

    [SerializeField] private GameObject head,
        body,
        rightUpHand,
        rightLowHand,
        rightHand,
        leftUpHand,
        leftLowHand,
        leftHand,
        rightUpLeg,
        rightLowLeg,
        rightLeg,
        leftUpLeg,
        leftLowLeg,
        leftLeg;

    [SerializeField] private GameObject[] eyBrow;
    public GameObject[] eyes;
    public GameObject mouth;

    private void Awake()
    {
        IsDead = false;
    }

    private void Start()
    {
        SetUpPlayer();
    }

    public void DeActiveScripts()
    {
        var playerScripts = GetComponents<MonoBehaviour>();
        var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemyObjects)
        {
            var enemyScripts = enemy.GetComponents<MonoBehaviour>();
            foreach (var script in enemyScripts)
            {
                script.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                if (script is EnemyTakeDamage || script is EnemyManager)
                    continue;
                if (script.gameObject.GetComponent<Animator>())
                {
                    script.gameObject.GetComponent<Animator>().enabled = false;
                }

                script.enabled = false;
            }
        }

        foreach (var script in playerScripts)
        {
            if (script is PlayerTakeDamage)
                continue;
            script.enabled = false;
        }
    }

    public void SetUpPlayer()
    {
        gameObject.name = player.name;
        head.GetComponent<SpriteRenderer>().sprite = player.head;
        body.GetComponent<SpriteRenderer>().sprite = player.body;

        rightUpHand.GetComponent<SpriteRenderer>().sprite = player.upHand;
        rightLowHand.GetComponent<SpriteRenderer>().sprite = player.lowHand;
        rightHand.GetComponent<SpriteRenderer>().sprite = player.hand;

        leftUpHand.GetComponent<SpriteRenderer>().sprite = player.upHand;
        leftLowHand.GetComponent<SpriteRenderer>().sprite = player.lowHand;
        leftHand.GetComponent<SpriteRenderer>().sprite = player.hand;

        rightUpLeg.GetComponent<SpriteRenderer>().sprite = player.upLeg;
        rightLowLeg.GetComponent<SpriteRenderer>().sprite = player.lowLeg;
        rightLeg.GetComponent<SpriteRenderer>().sprite = player.leg;

        leftUpLeg.GetComponent<SpriteRenderer>().sprite = player.upLeg;
        leftLowLeg.GetComponent<SpriteRenderer>().sprite = player.lowLeg;
        leftLeg.GetComponent<SpriteRenderer>().sprite = player.leg;

        eyBrow[0].GetComponent<SpriteRenderer>().sprite = player.eyBrow;
        eyBrow[1].GetComponent<SpriteRenderer>().sprite = player.eyBrow;
        eyes[0].GetComponent<SpriteRenderer>().sprite = player.eye;
        eyes[1].GetComponent<SpriteRenderer>().sprite = player.eye;
        mouth.GetComponent<SpriteRenderer>().sprite = player.mouth;
    }
}