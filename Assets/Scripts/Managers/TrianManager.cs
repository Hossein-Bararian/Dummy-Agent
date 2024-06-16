using System.Collections;
using RTLTMPro;
using UnityEngine;

public class TrianManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private RTLTextMeshPro titleText;
    [SerializeField] private RTLTextMeshPro descriptionText;
    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Training());
    }
    private IEnumerator Training()
    {
        yield return new WaitForSeconds(1.5f);
        _animator.Play("WelcomeTutorial");
     
    }

    public void SetTitleText(string title)
    {
        titleText.text = title;
    }
    public void SetDescriptionText(string description)
    {
        descriptionText.text = description;
     
    }
}
