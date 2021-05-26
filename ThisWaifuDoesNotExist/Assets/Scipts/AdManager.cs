using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour {
    string App_ID = "ca-app-pub-xxx~xxx";
    string interstitialAd_ID = "ca-app-pub-xxx/xxx";
    string bannerAd_ID = "ca-app-pub-xxx/xxx";
    string rewardedAd_ID = "ca-app-pub-xxx/xxx";    
    
    string testInterstitialAd_ID = "ca-app-pub-3940256099942544/1033173712";
    string testBannerAd_ID = "ca-app-pub-3940256099942544/6300978111";

    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    


    void Start() {
        MobileAds.Initialize(initStatus => { });
        this.RequestBannerAd();
        this.RequestInterstitialAd();
        //this.RequestRewardedAd();
    }

    private void RequestBannerAd() {
        this.bannerView = new BannerView(testBannerAd_ID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd() {
        this.interstitialAd = new InterstitialAd(testInterstitialAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(request);
    }
	
    private void RequestRewardedAd() {
        this.rewardedAd = new RewardedAd(rewardedAd_ID);
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    private void Update() {
        ShowAd();
    }

    private void ShowAd() {
        /*if (UIController.AdShow) {
            if (this.interstitialAd.IsLoaded()) {
                this.interstitialAd.Show();
            }
        }*/
    }
	
    private void ShowRewardedAd() {
        /*if (UIController.RewardedAdShow) {
            if (this.rewardedAd.IsLoaded()) {
                this.rewardedAd.Show();
            }
        }*/
    }
}