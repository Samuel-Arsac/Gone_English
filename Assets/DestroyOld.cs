using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOld : MonoBehaviour
{
    [SerializeField] private List<GameObject> toDestroy;
    [SerializeField] private List<GameObject> toSpawn;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in toDestroy)
        {
            Destroy(g);
        }

        foreach(GameObject g in toSpawn)
        {
            g.GetComponent<Image>().enabled = true;
        }

        
    }
}
