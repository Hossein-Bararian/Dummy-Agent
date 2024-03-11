using System;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemHolder;
    [SerializeField] private bool isPick;
    private Shooting _shooting;

    private void Awake()
    {
        IsPickItem();
    }

    private void Start()
    {
        _shooting = GetComponent<Shooting>();
    }


    void LateUpdate()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }

    private void Pick(GameObject item)
    {
        if (IsPickItem())
        {
            itemHolder.transform.GetChild(0).SetParent(null);
            item.transform.SetParent(itemHolder.transform);
            item.transform.position = itemHolder.transform.position;
        }
        else
        {
            item.transform.SetParent(itemHolder.transform);
            item.transform.position = itemHolder.transform.position;
        }
        IsPickItem();
    }

    private void Drop()
    {
        if (IsPickItem())
        {
            GameObject item = itemHolder.transform.GetChild(0).gameObject;
            Transform firePoint = item.transform.GetChild(0).transform;
            Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();
            Animator anim = item.GetComponent<Animator>();
            _shooting.enabled = false;
            item.transform.SetParent(null);
            itemRb.simulated = true;
            itemRb.AddForce(30 * firePoint.right, ForceMode2D.Impulse);
            anim.Play("GunRotate");
        }
        IsPickItem();
    }

    private bool IsPickItem()
    {
        if (itemHolder.transform.childCount > 0)
            isPick = itemHolder.transform.GetChild(0).gameObject != null;
        return isPick;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Pick(other.gameObject);
            }
        }
    }
}