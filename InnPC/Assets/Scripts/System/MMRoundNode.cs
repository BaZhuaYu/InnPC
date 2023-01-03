using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMRoundNode : MMNode
{
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerRound()
    {
        SetParent(MMBattleManager.Instance);
        MoveUp(MMBattleManager.Instance.FindHeight() * 0.35f);
        LoadImage("UI/ddd/Icon_PlayerRound");

        Vector3 a = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 b = new Vector3(2.5f, 2.5f, 2.5f);

        transform.localScale = a;
        transform.localScale = Vector3.Lerp(transform.localScale, b, 0.05f);
        
        StartCoroutine(aaaa());
    }


    IEnumerator aaaa()
    {
        int i = 0;
        Vector3 b = new Vector3(2.0f, 2.0f, 2.0f);
        while (i++ < 200)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, b, 0.05f);
            yield return new WaitForEndOfFrame();
        }
    }



    public void SetEnemyRound()
    {
        SetParent(MMBattleManager.Instance);
        MoveUp(MMBattleManager.Instance.FindHeight() * 0.35f);

        LoadImage("UI/ddd/Icon_EnemyRound");

        Vector3 a = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 b = new Vector3(2.5f, 2.5f, 2.5f);

        transform.localScale = a;
        transform.localScale = Vector3.Lerp(transform.localScale, b, 0.05f);
        
        StartCoroutine(aaaa());
    }

    
    public static MMRoundNode Create()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/MMRoundNode") as GameObject);
        obj.name = "MMCardNode";
        return obj.GetComponent<MMRoundNode>();
    }
}
