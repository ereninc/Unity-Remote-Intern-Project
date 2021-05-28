using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour
{
    string App_ID = "ca-app-pub-8665605135575423~4849708017";
    string interstitialAd_ID = "ca-app-pub-8665605135575423/6578271398";
    string bannerAd_ID = "ca-app-pub-8665605135575423/9910463002";
    string rewardedAd_ID = "ca-app-pub-8665605135575423/8405809644";    
    
    string testInterstitialAd_ID = "ca-app-pub-3940256099942544/1033173712";
    string testBannerAd_ID = "ca-app-pub-3940256099942544/6300978111";
    string testRewardedAd_ID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    void Start() 
    {
        MobileAds.Initialize(initStatus => { });
        this.RequestBannerAd();
        this.RequestInterstitialAd();
        this.RequestRewardedAd();
    }

    private void Update()
    {
        if (UIController.instance.AdShow)
        {
            ShowAd();
        }
        
        if (UIController.instance.RewardedAdShow)
        {
            ShowRewardedAd();
        }
    }

    private void RequestBannerAd() 
    {
        this._bannerView = new BannerView(bannerAd_ID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this._bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd() 
    {
        this._interstitialAd = new InterstitialAd(interstitialAd_ID);
        AdRequest request = new AdRequest.Builder().Build();    
        
        // Called when an ad is shown.
        this._interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this._interstitialAd.OnAdClosed += HandleOnAdClosed;
        
        this._interstitialAd.LoadAd(request);
    }
	
    private void RequestRewardedAd() 
    {
        this._rewardedAd = new RewardedAd(rewardedAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        
        
        // Called when an ad is shown.
        this._rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when the ad is closed.
        this._rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        
        this._rewardedAd.LoadAd(request);
    }

    private void ShowAd() 
    {
        if (this._interstitialAd.IsLoaded()) 
        {
            this._interstitialAd.Show();
        }
    }
	
    private void ShowRewardedAd() 
    {
        if (this._rewardedAd.IsLoaded())
        {
            this._rewardedAd.Show();
        }
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {
        DataManager.instance.SaveGold();
        DataManager.instance.level += 1;
        DataManager.instance.SaveLevel();
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        DataManager.instance.RewardedAdGold();
        DataManager.instance.SaveGold();
        DataManager.instance.level += 1;
        DataManager.instance.SaveLevel();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        SceneManager.LoadScene(0);
    }
}
