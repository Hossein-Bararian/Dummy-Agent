using System.Collections;
using RTLTMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrianManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private RTLTextMeshPro titleText;
    [SerializeField] private RTLTextMeshPro descriptionText;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Transform hand;
    private int _status;
    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(TrainingSectionJump());
    }
    
    private IEnumerator TrainingSectionJump()
    {
        MenuManager.IsOnGame = true;
        player.gameObject.GetComponent<Animator>().Play("Walk");
        player.runSpeed = 5f;
        yield return new WaitForSeconds(1.5f);
        _animator.Play("WelcomeTutorial");
        yield return new WaitForSeconds(2f);
        player.runSpeed = 6f;
        _animator.Play("SwapToJump");
        yield return new WaitForSeconds(5f);
        _animator.Play("SwapToSlide");
        yield return new WaitForSeconds(3f);
         MenuManager.IsOnGame = false;
         yield return new WaitForSeconds(1.5f);
        StartCoroutine(TrainingSectionShot()); 
    }
    private IEnumerator TrainingSectionShot()
    {
        _status = 0;
        hand.DORotateQuaternion(Quaternion.Euler(0, 0,  -90), .25f);
        yield return new WaitForSeconds(0.26f);
        _animator.Play("ClickToShotTutorial");
        SlowMotionMechanic.Instance.InputOnMobile();
        while (SlowMotionMechanic.Instance.slowTime>0.005)
        {
            SlowMotionMechanic.Instance.slowTime -=0.005f;
            yield return new WaitForSeconds(0.001f);
        }
    }
    private IEnumerator TrainingSectionHeadShot()
    {
        _status = 1;
        yield return new WaitForSeconds(2f);
        hand.DORotateQuaternion(Quaternion.Euler(0, 0,  -105), 0.24f);
        yield return new WaitForSeconds(0.25f);
        _animator.Play("ClickToHeadShotTutorial");
        SlowMotionMechanic.Instance.InputOnMobile();
        while (SlowMotionMechanic.Instance.slowTime>0.005)
        {
            SlowMotionMechanic.Instance.slowTime -=0.005f;
            yield return new WaitForSeconds(0.001f);
        }
    }
    private IEnumerator TrainingAdvice()
    {
        yield return new WaitForSeconds(3.2f);
        player.runSpeed = 0f;
        hand.DORotateQuaternion(Quaternion.Euler(0, 0,  -180), 0.3f);
        player.gameObject.GetComponent<Animator>().Play("Idle");
        _animator.Play("ImportantAdvice");
        _status = 2;
    }
    public void ClickButton()
    {
        if (_status==0)
        {
            _animator.Play("ClickToShotTutorialFadeOut");
            StartCoroutine(TrainingSectionHeadShot());
        }
        else if(_status==1)
        {
            _animator.Play("ClickToHeadShotTutorialFadeOut");
            StartCoroutine(TrainingAdvice());
        }
        else if (_status == 2)
        {
            SceneManager.LoadScene(0);
            return;
        }
        StartCoroutine(IncreaseSlowTime());
        SlowMotionMechanic.Instance.InputOnMobile();
        MenuManager.IsOnGame = true;
        Weapon.Instance.Pistol();
        MenuManager.IsOnGame = false;
    }

    private IEnumerator IncreaseSlowTime()
    {
        while (SlowMotionMechanic.Instance.slowTime < 1)
        {
            SlowMotionMechanic.Instance.slowTime += 0.05f;
            yield return new WaitForSeconds(0.005f);
        }
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
