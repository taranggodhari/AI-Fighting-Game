using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public string[] levelNames; 
	public int gameLevelNum;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	
	}
	
	public void LoadLevel(string sceneName){
		Application.LoadLevel (sceneName);
	}
	public void GoNextLevel(){
		if(gameLevelNum>=levelNames.Length){
			gameLevelNum=0;
			}
		LoadLevel (gameLevelNum);
		gameLevelNum++;
	}
	private void LoadLevel( int indexNum ) {  
		// load the game level  
		LoadLevel( levelNames[indexNum] ); 
	}
	public void ResetGame() { 
		// reset the level index counter  
		gameLevelNum = 0; 
	} 
}
