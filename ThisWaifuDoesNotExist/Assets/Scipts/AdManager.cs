using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    string App_ID = "ca-app-pub-xxx~xxx";
    string interstitialAd_ID = "ca-app-pub-xxx/xxx";
    string bannerAd_ID = "ca-app-pub-xxx/xxx";
    string rewardedAd_ID = "ca-app-pub-xxx/xxx";    
    
    string testInterstitialAd_ID = "ca-app-pub-3940256099942544/1033173712";
    string testBannerAd_ID = "ca-app-pub-3940256099942544/6300978111";
    string testRewardedAd_ID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    private void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBannerAd();
        this.RequestInterstitialAd();
        this.RequestRewardedAd();
    }

    private void RequestBannerAd() 
    {
        this.bannerView = new BannerView(testBannerAd_ID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd() 
    {
        this.interstitialAd = new InterstitialAd(testInterstitialAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }
	
    private void RequestRewardedAd() 
    {
        this.rewardedAd = new RewardedAd(testRewardedAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public void ShowAd() 
    {
        if (this.interstitialAd.IsLoaded()) 
        {     
            DataManager.instance.SaveGold();
            DataManager.instance.level += 1;
            DataManager.instance.SaveLevel(); 
            StartCoroutine(ReloadWithDelay());
            this.interstitialAd.Show();
        }
    }
	
    public void ShowRewardedAd() 
    {
        if (this.rewardedAd.IsLoaded())
        {
            DataManager.instance.RewardedAdGold();
            DataManager.instance.SaveGold();
            DataManager.instance.level += 1;
            DataManager.instance.SaveLevel();
            StartCoroutine(ReloadWithDelay());
            this.rewardedAd.Show();
        }
    }
    
    IEnumerator ReloadWithDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}