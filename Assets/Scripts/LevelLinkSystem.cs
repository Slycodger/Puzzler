using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLinkSystem : MonoBehaviour
{
    public List<ObjectCheck> ObjectsInLevel;
    public List<ObjectCheck> Completed;
    public ObjectCheck[] ObjectsInScene;
    public GameObject Door;
    public GameObject Door1;
    public int Checkers;
    public int Level;
    public int GoodCount;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
        ObjectsInScene = new ObjectCheck[GetComponentsInChildren<ObjectCheck>().Length];
        ObjectsInScene = FindObjectsOfType<ObjectCheck>();
        if(ObjectsInScene.Length > Checkers)
        {
            Checkers++;
        }
        for (int i =0; i<ObjectsInScene.Length; i++)
        {
            if (ObjectsInScene[i].level == Level)
            {
                if (!ObjectsInLevel.Contains(ObjectsInScene[i]))
                {
                    if (!Completed.Contains(ObjectsInScene[i]))
                    {
                        ObjectsInLevel.Add(ObjectsInScene[i]);
                    }
                }
            }
        }
        for(int i = 0; i< ObjectsInLevel.Count; i++)
        {
            if (ObjectsInLevel[i].GoodToGo == 1)
            {
                GoodCount++;
                if (!Completed.Contains(ObjectsInLevel[i]))
                {
                    Completed.Add(ObjectsInLevel[i]);
                }
                ObjectsInLevel.Remove(ObjectsInLevel[i]);
            }
        }
        for(int i =0; i<Completed.Count; i++)
        {
            if(Completed[i].GetComponent<ObjectCheck>().GoodToGo == 0)
            {
                if (!ObjectsInLevel.Contains(Completed[i]))
                {
                    ObjectsInLevel.Add(Completed[i]);
                }
                Completed.Remove(Completed[i]);
                GoodCount -= 1;
            }
        }
        if(GoodCount == Checkers)
        {
            Door.GetComponent<Door>().LevelComplete = true;
            Door1.GetComponent<Door>().LevelComplete = true;
        }
        if (GoodCount != Checkers)
        {
            Door.GetComponent<Door>().LevelComplete = false;
            Door1.GetComponent<Door>().LevelComplete = false;
        }
    }
}
