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

    public MMNode panelAvatar;


    
    [HideInInspector]
    public MMBattlePhase phase;
    [HideInInspector]
    public MMBattleState state;

    [HideInInspector]
    public List<MMUnitNode> units1;
    [HideInInspector]
    public List<MMUnitNode> units2;

    [HideInInspector]
    public MMCardNode selectingCard;
    [HideInInspector]
    public MMSkillNode selectingSkill;
    [HideInInspector]
    public MMUnitNode sourceUnit;
    [HideInInspector]
    public MMUnitNode targetUnit;
    


    //Private

    int round;
    int isPlayerRound;
    bool isLocked;
    


    private void Start()
    {
        //TopBar

        buttonMain.onClick.AddListener(OnClickButtonMain);
        buttonAwait.onClick.AddListener(OnClickButtonAwait);
        buttonGameOver.onClick.AddListener(OnClickGameOverButton);

        panelGameover.SetActive(false);
        panelSettings.SetActive(false);

        MMCardPanel.Instance.CloseUI();
        MMSkillPanel.Instance.CloseUI();
        MMUnitPanel.Instance.CloseUI();
        
        isPlayerRound = 0;
        isLocked = false;

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
        MMExplorePanel.Instance.SetWin();
        MMExplorePanel.Instance.ExitBattle();
        MMExplorePanel.Instance.EnterExplore();
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
