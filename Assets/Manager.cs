using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manager : MonoBehaviour
{
    [HideInInspector]
    public static Manager Instance { get; private set; } //for singleton

    public GameObject FactoryPrefab;
    public string TargetTag;
    public string FactoryTag;
    public string AgentTag;

    public int MaximumAgents = 100;

    //private variables
    private GameObject[] _targets;

    private List<GameObject> _occupiedTargets = new List<GameObject>();

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _targets = GameObject.FindGameObjectsWithTag(TargetTag);
        //setup initial factorys
        int factoryCount = (int)(_targets.Length * 0.5f);
        for (int i = 0; i < factoryCount; i++)
        {
            GameObject target = GetTarget();
            InstantiateFactory(target);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void InstantiateFactory(GameObject target)
    {
        //Reroute units heading to target
        MobileUnit[] units = Object.FindObjectsOfType<MobileUnit>();
        foreach (MobileUnit unit in units)
        {
            if (unit.Target != target) { continue; }
            GameObject newTarget = Manager.Instance.GetTarget();
            unit.UpdateTarget(newTarget);
        }

        //guard statement, we want the number of factories to only max out at half the targets
        double factoryCount = (GameObject.FindGameObjectsWithTag(FactoryTag).Length / (_targets.Length * 1.00f));
        //Debug.Log("FactoryCount: " + factoryCount);
        if ((GameObject.FindGameObjectsWithTag(FactoryTag).Length / _targets.Length) >= 0.5f) { Debug.Log("Too Many Factories"); return; }

        GameObject go = Instantiate(FactoryPrefab, target.transform.position, target.transform.rotation);
        float moveY = go.transform.localScale.y / 2.0f; //get half factory height
        go.transform.position += new Vector3(0, moveY, 0); //move factory up
        Factory factory = go.GetComponent<Factory>();
        factory.Target = target;
        _occupiedTargets.Add(target);
    }

    public GameObject GetTarget()
    {
        int targetIndex = Random.Range(0, _targets.Length);
        GameObject target = _targets[targetIndex];
        //*/
        int startIndex = targetIndex;
        while (_occupiedTargets.Contains(target))
        {
            Debug.Log("test");
            targetIndex++;
            if (targetIndex >= _targets.Length)
            {
                targetIndex = 0;
            }
            target = _targets[targetIndex];
            //catch if there is an issue
            if (startIndex == targetIndex) { break; }
        }
        return target;
    }

    public bool IsTargetOccupied(GameObject target)
    {
        return _occupiedTargets.Contains(target);
    }

    public void RemoveTargetFromList(GameObject go)
    {
        _occupiedTargets.Remove(go);
    }

    public bool MakeAgentsAllowed()
    {
        int agentCount = Object.FindObjectsOfType<MobileUnit>().Length;
        if (agentCount < MaximumAgents) { return true; }
        return false;
    }
}