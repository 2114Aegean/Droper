using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // public GameObject bullet;
    public BulletManager bulletMgr;

    public float dropSpeed = 0.1f;
    public bool moveFlg = true;

    IEnumerator PlayerMove()
    {
        while (true)
        {
            if(moveFlg) transform.position -= new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletMgr = FindObjectOfType<BulletManager>();

        StartCoroutine(PlayerMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletMgr.InstanceBullet();
            Debug.Log("’e”­ŽË");
        }
    }
}
