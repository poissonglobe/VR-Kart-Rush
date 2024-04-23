using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure : MonoBehaviour
{
    public GameObject plan1;    //module initiale 1
    public GameObject plan2;    //module initiale 2
    public GameObject plan3;    //module initiale 3

    public GameObject[] prefabTab = new GameObject[10] ;    //liste des differents modules

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > plan2.transform.position.x - 40)
        {
            int randomNumber = Random.Range(0, 10);
            
            // generation du prochain module
            GameObject newPrefabInstance = Instantiate(prefabTab[randomNumber]);
            Vector3 newPosition = new Vector3(plan3.transform.position.x + 100,0, 0); 
            newPrefabInstance.transform.position = newPosition;

            Destroy(plan1);
            plan1 = plan2;
            plan2 = plan3;
            plan3 = newPrefabInstance;
        }

    }
}
