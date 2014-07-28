using UnityEngine;
using System.Collections;

public class GameCenterScript : MonoBehaviour {
#if UNITY_IOS
	public string totalDistanceLeaderboard = "traveled.total";
	public string longestDistanceLeaderboard = "traveled.longest";

	// Use this for initialization
	void Start () {
		GameCenterManager.init(); //Initilize GameCenter 

	}
	void Awake(){
		DontDestroyOnLoad(gameObject); //Keep the GameObject that this is attached to alive for the entire session. 
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void CheckAchievements()
	{
		//TODO: Open up the achievmenets dialogue. 
	}

	public void ReportLeaderboard() {

		GameCenterManager.reportScore(0,longestDistanceLeaderboard); //TODO: Report a score here when ReportLeaderboard called. 
		GameCenterManager.reportScore(0,totalDistanceLeaderboard); //TODO: Report a score here when ReportLeaderboard called.
	}
#endif
}
