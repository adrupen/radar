using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ScenarioData {
	public int jetPicked;
	public float reactionTime;
	public bool isCorrect;
    public int scenario;
    public int radar;
}

[RequireComponent(typeof(SpawnObjects))]
public class EventChain : MonoBehaviour {

	[Header("Tutorial 1")]
	public List<Marker> tutorial1Markers;

	[Header("Tutorial 2")]
	public List<Marker> tutorial2Markers;

	[Header("Tutorial 3")]
	public List<Marker> tutorial3Markers;

    [Header("Tutorial 4")]
    public List<Marker> tutorial4Markers;

    [Header("Tutorial 5")]
    public List<Marker> tutorial5Markers;

    [Header("Scenario 1")]
	public List<Marker> scenario1Markers;

	[Header("Scenario 2")]
	public List<Marker> scenario2Markers;

	[Header("Scenario 3")]
	public List<Marker> scenario3Markers;

	[Header("Scenario 4")]
	public List<Marker> scenario4Markers;

	[Header("Scenario 5")]
	public List<Marker> scenario5Markers;

	[Header("Scenario 6")]
	public List<Marker> scenario6Markers;

	[Header("Scenario 7")]
	public List<Marker> scenario7Markers;

	[Header("Scenario 8")]
	public List<Marker> scenario8Markers;

	[Header("Scenario 9")]
	public List<Marker> scenario9Markers;

	[Header("Scenario 10")]
	public List<Marker> scenario10Markers;

    [Header("Other Variables")]
	public GameState currentState = GameState.TutorialScreen;
    public float transitionTime = 2f;

    private List<List<Marker>> scenarios;
    private List<List<Marker>> tutorials;

    public enum GameState {TutorialScreen, TutorialRunning, TryAgain, RadarSceen, ScenarioRunning, TransitionScreen, Finished}

	private GameObject ContinueUI;
	private Text FirstRow;
	private Text SecondRow;
	private Image[] inputButtons;

	private SpawnObjects objectSpawner;

	private bool loadingScenario = false;
	private int tutorialNum = 5;
	private int tutorialCounter = 0;
    private int radarNum = 3;
    private int radarCounter = 0;
    private int scenarioNum = 10;
    private int scenarioCounter = 0;
	private ScenarioData currentScenarioData;
    private string csvFileName;
    private string playerId;
    private int currentRadar;

    private int[] indexMap;
    private int[] radars;
    private int[] tutorialRadarOrder;
    private Dictionary<int, GameObject> RadarObjects;


	// Use this for initialization
	void Start () {

		objectSpawner = GetComponent<SpawnObjects> ();

		ContinueUI = GameObject.FindWithTag ("ContinueUI").gameObject;
		FirstRow = ContinueUI.transform.GetChild (0).GetComponent<Text>();
		SecondRow = ContinueUI.transform.GetChild (1).GetComponent<Text>();
		inputButtons = GameObject.FindWithTag ("InputButtons").GetComponentsInChildren<Image> ();

        radars = new int[] {1, 2, 3};
        tutorialRadarOrder = new int[] { 1, 1, 1, 2, 3 };

        // Shuffle radar order
        radars = randomizeRadarOrder(radars);

        currentRadar = tutorialRadarOrder[0];

        GameObject radarRootObject = GameObject.Find("Radars");

        RadarObjects = new Dictionary<int, GameObject>();
        RadarObjects.Add(1, radarRootObject.transform.GetChild(0).gameObject);
        RadarObjects.Add(2, radarRootObject.transform.GetChild(1).gameObject);
        RadarObjects.Add(3, radarRootObject.transform.GetChild(2).gameObject);

        RadarObjects[currentRadar].SetActive(true);
        objectSpawner.setLabelCamera(RadarObjects[currentRadar].GetComponentInChildren<Camera>());

        tutorials = new List<List<Marker>>();
        scenarios = new List<List<Marker>>();

        tutorials.Add (tutorial1Markers);
        tutorials.Add (tutorial2Markers);
        tutorials.Add (tutorial3Markers);
        tutorials.Add (tutorial4Markers);
        tutorials.Add (tutorial5Markers);

        scenarios.Add (scenario1Markers);
        scenarios.Add (scenario2Markers);
        scenarios.Add (scenario3Markers);
        scenarios.Add (scenario4Markers);
        scenarios.Add (scenario5Markers);
        scenarios.Add (scenario6Markers);
        scenarios.Add (scenario7Markers);
        scenarios.Add (scenario8Markers);
        scenarios.Add (scenario9Markers);
        scenarios.Add (scenario10Markers);

        indexMap = randomizeScenarioOrder(scenarios);
        
        csvFileName = Application.dataPath + "/Data/participantData.csv";
        playerId = generateID();
        Debug.Log("Player Id is: " + playerId);
	}

	void Update(){
        //Debug.Log(currentState);
        int keyNum = -1;
		switch (currentState) {
            case GameState.TutorialScreen:
                objectSpawner.pauseGame();
			    FirstRow.text = "Tutorial";
                SecondRow.text = "Press any button to continue";
                if (Input.anyKeyDown) { 
                    setupNextScenario(0f);
			    }
			    break;

		    case GameState.TutorialRunning:
			    keyNum = checkKeyPressed ();
			    if (keyNum > 0) {
                    highlightInputUI(keyNum);
				    if (tutorials [tutorialCounter] [keyNum - 1].isMostDangerous) {
					    tutorialCounter++;
                        if (tutorialCounter >= tutorialNum)
                        {
                            currentState = GameState.RadarSceen;
                            switchRadar();
                        }
                        else
                        {
                            currentState = GameState.TransitionScreen;
                            switchRadar(true);
                        }
				    } else {
					    currentState = GameState.TryAgain;
				    }
			    }
			    break;

		    case GameState.RadarSceen:
                showGUI();
                objectSpawner.pauseGame();
                FirstRow.text = "Radar " + (radarCounter + 1);
                SecondRow.text = "Press any button to continue";
                if (Input.anyKeyDown)
                {
                    if (!loadingScenario)
                    {
                        setupNextScenario(0f);
                    }
                }
                break;

            case GameState.ScenarioRunning:
                keyNum = checkKeyPressed();
                if (keyNum > 0)
                {
                    highlightInputUI(keyNum);
                    objectSpawner.pauseGame();
                    currentScenarioData.radar = currentRadar;
                    currentScenarioData.scenario = indexMap[scenarioCounter];
                    currentScenarioData.reactionTime = objectSpawner.getReactionTime();
                    currentScenarioData.jetPicked = keyNum;
                    if (scenarios[scenarioCounter][keyNum - 1].isMostDangerous)
                    {
                        currentScenarioData.isCorrect = true;
                    }
                    else
                    {
                        currentScenarioData.isCorrect = false;
                    }
                    saveData();
                    scenarioCounter++;
                    currentState = GameState.TransitionScreen;
                }
                break;

            case GameState.TransitionScreen:
                if (scenarioCounter >= scenarioNum)
                {
                    scenarioCounter = 0;
                    radarCounter++;
                    if (radarCounter >= radarNum)
                    {
                        currentState = GameState.Finished;
                    }
                    else
                    {
                        switchRadar();
                        currentState = GameState.RadarSceen;
                    }
                }
                else
                {
                    showGUI();
                    objectSpawner.pauseGame();
                    FirstRow.text = "Good job!";
                    SecondRow.text = "Next Scenario Loading";
                    if (!loadingScenario)
                        setupNextScenario(transitionTime);
                }
            break;

            case GameState.TryAgain:
                showGUI();
                objectSpawner.pauseGame();
                FirstRow.text = "Sorry wrong answer";
                SecondRow.text = "Try Again";
                if (!loadingScenario)
                    setupNextScenario(transitionTime);
                break;

		    case GameState.Finished:
                showGUI();
                objectSpawner.pauseGame();
                FirstRow.text = "Finished";
                SecondRow.text = "Thank you!";
                if (Input.GetKeyDown(KeyCode.Return)) {
                    resetGame();
                }
			    break;
		    }
	}

    void switchRadar(bool tutorial = false) {
        RadarObjects[currentRadar].SetActive(false);
        if (!tutorial)
        {
            currentRadar = radars[radarCounter];
            indexMap = randomizeScenarioOrder(scenarios);
        }
        else {
            currentRadar = tutorialRadarOrder[tutorialCounter];
        }
        RadarObjects[currentRadar].SetActive(true);
        objectSpawner.setLabelCamera(RadarObjects[currentRadar].transform.GetComponentInChildren<Camera>());
        if (currentRadar == 3)
            objectSpawner.setIs2d(true);
        else
            objectSpawner.setIs2d(false);
    }

	int checkKeyPressed(){
		if (Input.GetKeyDown (KeyCode.Keypad1))
			return 1;
		if (Input.GetKeyDown (KeyCode.Keypad2))
			return 2;
		if (Input.GetKeyDown (KeyCode.Keypad3))
			return 3;
        return -1;
	}

	void saveData(){
        if (!File.Exists(csvFileName))
        {
            string fileHeader = "Player id" + "," + "Radar" + "," + "Scenario" + "," + "Jet Picked" + "," + "Reaction Time" + "," + "is Correct" + System.Environment.NewLine;
            File.WriteAllText(csvFileName, fileHeader);
        }
        string data = playerId + "," + currentRadar.ToString() + "," + currentScenarioData.scenario.ToString() + "," + currentScenarioData.jetPicked.ToString() + "," + currentScenarioData.reactionTime.ToString() + "," + currentScenarioData.isCorrect.ToString() + System.Environment.NewLine;
        File.AppendAllText(csvFileName, data);
    }

    void setupNextScenario(float waitTime) {
        objectSpawner.clearMarkers();
        if (tutorialCounter >= tutorialNum)
        {
            objectSpawner.populateMarkers(scenarios[scenarioCounter]);
        }
        else
        {
            objectSpawner.populateMarkers(tutorials[tutorialCounter]);
        }
        StartCoroutine(waitAndStart(waitTime));
    }

    private IEnumerator waitAndStart(float waitTime)
    {
        loadingScenario = true;
        yield return new WaitForSeconds(waitTime);
        hideGUI();
        resetInputUI();
        objectSpawner.startGame();
        if (tutorialCounter >= tutorialNum)
        {
            currentState = GameState.ScenarioRunning;
        }
        else
        {
            currentState = GameState.TutorialRunning;
        }
        loadingScenario = false;
    }

    void hideGUI() {
        ContinueUI.SetActive(false);
    }

    void showGUI() {
        ContinueUI.SetActive(true);
    }

    int[] randomizeScenarioOrder(List<List<Marker>> scenarioList) {
        int n = scenarioList.Count;
        int[] indexMapLocal = new int[n];
        for (int i = 0; i < n; i++)
            indexMapLocal[i] = i;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            List<Marker> value = scenarioList[k];
            int index = indexMapLocal[k];
            scenarioList[k] = scenarioList[n];
            scenarioList[n] = value;
            indexMapLocal[k] = indexMapLocal[n];
            indexMapLocal[n] = index;
        }
        return indexMapLocal;
    }

    int[] randomizeRadarOrder(int[] radars) {
        int n = radars.Length;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = radars[k];
            radars[k] = radars[n];
            radars[n] = value;
        }
        return radars;
    }

    void highlightInputUI(int button)
    {
        inputButtons[button].color = new Color(0.27f, 0.74f, 0.44f);
    }

    void resetInputUI(){
        foreach (Image img in inputButtons)
        {
            img.color = new Color(0.11f, 0.32f, 0.18f);
        }
    }

    string generateID()
    {
        return System.Guid.NewGuid().ToString();
    }

    void resetGame() {
        tutorialCounter = 0;
        scenarioCounter = 0;
        radarCounter = 0;
        playerId =  generateID();
        switchRadar(true);
        currentState = GameState.TutorialScreen;
    }
}
