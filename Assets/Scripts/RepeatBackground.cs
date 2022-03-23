using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    float yShift = 40.8f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float diff = player.transform.position.y - transform.position.y;
        if (Mathf.Abs(diff) >= yShift){
            float sign = diff / Mathf.Abs(diff);
            Vector2 newPos = new Vector2(0, transform.position.y + 2*yShift*sign);
            transform.SetPositionAndRotation(newPos, Quaternion.identity);
        }
    }
}
