using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GameManager", menuName = "GameManager")]
public class GameManager : ScriptableObject
{
    public GameManager Instance;

    void Awake()
    {
        ValidateSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ValidateSingleton()
    {
        if(Instance != null && Instance != this)
        {
            Debug.LogError("MULTIPLE GAME MANAGER OBJECTS!");
            Destroy(this);
        }
        else {Instance = this;}
    }
}
