using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class MMBattleManager : MonoBehaviour
{

    public static MMBattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public MMNode background;
    public Button main;
    public Text buttonMain;
    public Text title;


    public MMBattlePhase phase;
    public MMBattleState state;


    public List<MMUnitNode> units1;
    public List<MMUnitNode> units2;


    public MMSkillNode selectingSkill;
    public MMUnitNode sourceUnit;
    public MMUnitNode targetUnit;
    
    public int round;
    public int level;

    public Dictionary<int, List<MMSkillNode>> historySkills;



    private void Start()
    {
        main.onClick.AddListener(OnClickMainButton);

        MMCardPanel.Instance.CloseUI();
        MMSkillPanel.Instance.CloseUI();
        MMUnitPanel.Instance.CloseUI();

        historySkills = new Dictionary<int, List<MMSkillNode>>();

        EnterPhase(MMBattlePhase.Begin);
    }


    public void LoadLevel()
    {
        level = MMPlayerManager.Instance.level;
        LoadPlayerUnits();
        LoadLevel(this.level);
    }


    public void LoadCard()
    {

    }



    public void PlayCard()
    {

    }





    public void OnClickMainButton()
    {
        switch (phase)
        {
            case MMBattlePhase.Begin:
                BroadCast(MMTriggerTime.OnBattleBegin);
                EnterPhase(MMBattlePhase.PlayerRound);
                break;
            case MMBattlePhase.PlayerRound:
                if (sourceUnit == null)
                {
                    BroadCast(MMTriggerTime.OnRoundEnd);
                    EnterPhase(MMBattlePhase.EnemyRound);
                }
                else
                {
                    UnselectSourceCell();
                }
                break;
            case MMBattlePhase.EnemyRound:
                BroadCast(MMTriggerTime.OnRoundEnd);
                OnPhaseEnd();
                EnterPhase(MMBattlePhase.PlayerRound);
                break;
            case MMBattlePhase.End:
                EnterPhase(MMBattlePhase.Begin);
                break;
        }

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

    }



    public void OnClickButtonBack()
    {
        if (this.state == MMBattleState.SelectSkill)
        {
            this.ClearSelectSkill();
            this.EnterState(MMBattleState.SourMoved);
        }
        else if (this.state == MMBattleState.SourMoved)
        {
            this.EnterState(MMBattleState.SelectSour);
        }
        else if (this.state == MMBattleState.SelectSour)
        {
            this.ClearSource();
            this.EnterState(MMBattleState.Normal);
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnClickButtonBack();
        }


        if (this.state == MMBattleState.SelectSour || this.state == MMBattleState.SourMoved)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //SetSelectingSkill(sourceUnit.skills[0]);
                SelectSkill(sourceUnit.skills[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //SetSelectingSkill(sourceUnit.skills[1]);
                SelectSkill(sourceUnit.skills[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                //SetSelectingSkill(sourceUnit.skills[2]);
                SelectSkill(sourceUnit.skills[2]);
            }
            //else if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    SetSelectSkill(MMCardManager.instance.hand.cards[3]);
            //}
        }

    }


    private void UpdateUI()
    {
        foreach (var cell in MMMap.Instance.cells)
        {
            cell.HandleState(MMNodeState.Normal);
            cell.HandleHighlight(MMNodeHighlight.Normal);
        }

        MMSkillPanel.Instance.UpdateUI();

        switch (this.state)
        {
            case MMBattleState.Normal:
                break;
            case MMBattleState.SelectSour:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowMoveCells();
                break;
            case MMBattleState.SourMoved:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                break;
            case MMBattleState.SelectSkill:
                sourceUnit.cell.HandleHighlight(MMNodeHighlight.Green);
                sourceUnit.ShowAttackCells();
                MMSkillPanel.Instance.SetSelectedSkill(selectingSkill);
                break;
        }

    }



    public void ShowButton(string s)
    {
        buttonMain.text = s;
    }

    public void ShowTitle(string s)
    {
        title.text = s;
    }





}
