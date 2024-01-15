using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerDungeon : MonoBehaviour
{
    public static GameManagerDungeon Instance { get; private set; }

    private GameObject m_player;

    private ProcGen m_procGen;

    public void SubscribeToOnDeathEvent(IEnemy enemy)
    {

    }

    public void SetPlayerPosition(Vector2 position) { m_player.transform.position = position; }

    private void Awake()
    {
        Instance = this;

        m_player = GameObject.Find("Player");

        Transform gun = Instantiate(GunManager.Instance.GetPrefab(PlayerStats.Instance.GetEquipped()), Vector3.zero, Quaternion.identity);
        gun.parent = m_player.transform.GetChild(0);
        gun.localPosition = Vector3.zero;

        m_procGen = GameObject.Find("ProcGen").GetComponent<ProcGen>();
        m_procGen.RunProcGen();
    }

    private void Start()
    {
        m_procGen.Init();

        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        yield return new WaitForSeconds(0.4f);
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }
}
