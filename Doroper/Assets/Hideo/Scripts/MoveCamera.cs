using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public GameObject Player;

    IEnumerator CameraMove()
    {
        while (true)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -5);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraMove());

    }

    private void Update()
    {
        // transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -5);
    }
}
