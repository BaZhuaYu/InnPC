using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMBattleManager : MMNode
{

    public static MMBattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    //
    public MMNode background;
    public MMNode backgroundNote;
    public Button buttonMain;
    public Button buttonAwait;
    public Text textButtonMain;

    public Text title;
    public Text textPhase;
    public Text textHP;
    public Button buttonDeck;
    public Button buttonUsed;
    public GameObject panelGameover;
    public Text textGameOver;
    public Button buttonGameOver;
    public GameObject panelSettings;

    //
    public MMBattlePhase phase;
    public MMBattleState state;


    public List<MMUnitNode> units1;
    public List<MMUnitNode> units2;

    public MMCardNode selectingCard;
    public MMSkillNode selectingSkill;
    public MMUnitNode sourceUnit;
    public MMUnitNode targetUnit;


    //
    public int round;
    public int isPlayerRound;

    public Dictionary<int, List<MMSkillNode>> historySkills;


    private void Start()
    {
        //TopBar
        buttonMain = GameObject.Find("MainButton").GetComponent<Button>();
        backgroundNote = GameObject.Find("Canvas/PanelBattle/TopBar/Note").GetComponent<MMNode>();

        //PanelButton
        //buttonAttack = GameObject.Find("AttackButton").GetComponent<Button>();

        textButtonMain = GameObject.Find("MainButton").GetComponentInChildren<Text>();
        buttonMain.onClick.AddListener(OnClickButtonMain);

        //PanelSkill
        buttonAwait = GameObject.Find("AwaitButton").GetComponent<Button>();
        buttonAwait.onClick.AddListener(OnClickButtonAwait);
        textHP = GameObject.Find("TextHP").GetComponent<Text>();
        

        panelGameover.SetActive(false);
        buttonGameOver.onClick.AddListener(OnClickGameOverButton);

        panelSettings.SetActive(false);

        MMCardPanel.Instance.CloseUI();
        MMSkillPanel.Instance.CloseUI();
        MMUnitPanel.Instance.CloseUI();

        historySkills = new Dictionary<int, List<MMSkillNode>>();

        isPlayerRound = 0;

        CloseUI();
        
    }

    
    private void Update()
    {
        textHP.text = MMExplorePanel.Instance.hp + "";

        if (Input.GetMouseButtonDown(1))
        {
            OnClickButtonBack();
            return;
        }

        if (this.phase == MMBattlePhase.UnitActing)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TryEnterStateSelectingCard(MMCardPanel.Instance.hand[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TryEnterStateSelectingCard(MMCardPanel.Instance.hand[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TryEnterStateSelectingCard(MMCardPanel.Instance.hand[2]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TryEnterStateSelectingCard(MMCardPanel.Instance.hand[3]);
            }
        }

        textPhase.text = phase.ToString() + " " + state.ToString();
        
    }


    public void DebugConfig()
    {
        
    }


    public MMUnitNode FindUnit(int id)
    {
        foreach (var unit in units1)
        {
            if (unit.id == id)
            {
                return unit;
            }
        }
        return null;
    }
    

    public void LoadLevel()
    {
        LoadPlayerUnits();
        MMUnitPanel.Instance.OpenUI();
        MMUnitPanel.Instance.Accept(MMExplorePanel.Instance.minions);

        LoadLevel(MMExplorePanel.Instance.levelBattle);
        if (GameObject.Find("BattleStartButton") == null)
        {

        }
        else
        {
            GameObject.Find("BattleStartButton").SetActive(false);
        }
    }


    public void LoadCard()
    {

    }

    
    public void OnClickButtonMain()
    {
        switch (phase)
        {
            case MMBattlePhase.None:
                EnterPhase(MMBattlePhase.BattleBegin);
                break;
                
            default:
                ShowTitle(phase.ToString());
                Debug.LogError(phase.ToString());
                break;

                //case MMBattlePhase.Begin:
                //    EnterPhase(MMBattlePhase.PlayerRound);
                //    break;

                //case MMBattlePhase.PlayerRound:
                //    if (sourceUnit == null)
                //    {
                //        EnterPhase(MMBattlePhase.EnemyRound);
                //    }
                //    else
                //    {
                //        UnselectSourceCell();
                //    }
                //    break;
                //case MMBattlePhase.EnemyRound:
                //    MMDebugManager.FatalError("OnClickMainButton: EnemyRound");
                //    break;


                //case MMBattlePhase.UnitEnd:
                //    if (sourceUnit.group == 1)
                //    {
                //        EnterPhase(MMBattlePhase.EnemyRound);
                //    }
                //    else
                //    {
                //        EnterPhase(MMBattlePhase.PlayerRound);
                //    }
                //    break;

        }

    }


    public void OnClickButtonAwait()
    {
        if (this.sourceUnit == null)
        {
            return;
        }

        EnterPhase(MMBattlePhase.UnitEnd);
    }


    public void OnClickButtonBack()
    {
        if (this.state == MMBattleState.SelectingSkill)
        {
            this.EnterState(MMBattleState.SelectedSourceUnit);
        }
        else if (this.state == MMBattleState.SelectingCard)
        {
            this.EnterState(MMBattleState.SelectedSourceUnit);
        }


        //移动以后
        //选择卡牌以后



    }
    

    public void Clear()
    {
        foreach (var unit in units1)
        {
            unit.Clear();
        }

        foreach (var unit in units2)
        {
            unit.Clear();
        }

        units1.Clear();
        units2.Clear();
        MMMap.Instance.Clear();
        MMCardPanel.Instance.Clear();
        MMSkillPanel.Instance.Clear();
        MMUnitPanel.Instance.Clear();

    }


    public void ShowButton(string s, bool isEnabled = true)
    {
        textButtonMain.text = s;
        buttonMain.enabled = isEnabled;
    }


    public void ShowTitle(string s)
    {
        title.text = s;
    }


    public void OnClickGameOverButton()
    {
        panelGameover.SetActive(false);
        this.CloseUI();
        MMExplorePanel.Instance.SetWin();
    }
    

    public void OpenSettingPanel()
    {
        panelSettings.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        panelSettings.SetActive(false);
    }

}
