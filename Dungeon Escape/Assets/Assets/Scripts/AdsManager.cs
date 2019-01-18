using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public void ShowRewarded()
	{
		Debug.Log("Showing Rewarded Ad");
		//check if the advertisement is ready (rewardedVideo)
		//Show(rewardedVideo)

		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions
			{
				resultCallback = HandleShowResult
			};

			Advertisement.Show("rewardedVideo", options);
		}
	}

	void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
			case ShowResult.Finished:
				//Award 100 gems to player
				GameManager.Instance.player.AddGems(100);
				UIManager.Instance.OpenShop(GameManager.Instance.player.diamonds);
				Debug.Log("You finished the ad!, here's 100 Gems!");
				break;
			case ShowResult.Skipped:
				Debug.Log("You skipped the ad! No gems for you");
				break;
			case ShowResult.Failed:
				Debug.Log("The Video failed, it must not have been ready");
				break;
		}
	}
}
