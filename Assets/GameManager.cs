using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   private DataPersisted DataPers;

    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            DataPers = DataChanges.LoadData();
            DontDestroyOnLoad(this);
        }
        else if (Instance != this) Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
