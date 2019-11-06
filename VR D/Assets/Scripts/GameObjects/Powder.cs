using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powder : MonoBehaviour
{
    public enum Type : uint
    {
        Sugar = 0,
        BlackSuger
    }
    [SerializeField] Type type_;
    public Type type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
