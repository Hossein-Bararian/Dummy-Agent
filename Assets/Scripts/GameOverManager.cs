using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private Animator _anim;
  
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public IEnumerator CrossFadeRestartLevel()
    {
        SlowMotionMechanic.Instance.StopSlowMotion();
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }
}
