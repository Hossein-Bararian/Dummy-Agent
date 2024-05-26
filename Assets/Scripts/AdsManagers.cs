using UnityEngine;
using AdiveryUnity;
using UnityEngine.SceneManagement;

public class AdsManagers : MonoBehaviour
{
    [SerializeField] private string app_id ;
    [SerializeField] private string rewarded_id ;
  
    AdiveryListener listener;

    public void Start()
    {
        Adivery.Configure(app_id);
        Adivery.PrepareRewardedAd(rewarded_id);

        listener = new AdiveryListener();

        listener.OnError += OnError;
        listener.OnRewardedAdLoaded += OnRewardedLoaded;
        listener.OnRewardedAdClosed += OnRewardedClosed;

        Adivery.AddListener(listener);
    }

    public void OnRewardedLoaded(object caller, string placementId)
    {
        
    }

    public void OnRewardedClosed(object caller, AdiveryReward reward)
    {
        if (reward.IsRewarded)
        {
            SceneManager.LoadScene(0);
            //Double headCoin
        }
        else
        {
            Debug.Log("Please Watch to Ads Fully for Double HeadCoin");
        }
    }

    public void LoadRequestedAds( )
    {
        if (Adivery.IsLoaded(rewarded_id))
        {
            Adivery.Show(rewarded_id);
        }
    }
    public void OnError(object caller, AdiveryError error)
    {
        Debug.Log("placement: " + error.PlacementId + " error: " + error.Reason);
    }
   
}
