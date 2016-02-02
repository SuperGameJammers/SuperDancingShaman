#pragma strict

var pressStartScreen : GameObject;
var startMenu : GameObject;
var characterSelectMenu : GameObject;
var optionsMenu : GameObject;
var aboutScreen : GameObject;
var hiScoresScreen : GameObject;
var panelCount : GameObject [];


function Awake ()
{
	//This function makes panels behave like they should, since they need to be active at the beggining
	pressStartScreen = GameObject.Find ("pressStartScreen");
	startMenu = GameObject.Find("startMenu");
	characterSelectMenu = GameObject.Find("characterSelectMenu");
	optionsMenu = GameObject.Find("optionsMenu");
	aboutScreen = GameObject.Find("aboutScreen");
	hiScoresScreen = GameObject.Find("hiScoresScreen");
}


function Start () 
{
	// creates a list with all marked panels and disables them to keep stuff neat:
	panelCount = GameObject.FindGameObjectsWithTag("menuPanel");
	bootMenu();
}


function bootMenu()
{
	for (var panels : GameObject in panelCount)
	{ 
		panels.SetActive(false);
	}
	startMenu.SetActive (!startMenu.activeSelf); 
}
