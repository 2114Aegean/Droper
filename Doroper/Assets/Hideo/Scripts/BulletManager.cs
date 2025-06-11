using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class BulletManager : MonoBehaviour
{
    enum BulletMode
    {
        MODE_NO_RECOIL = 0,
        MODE_RECOIL = 1,
        MODE_FLOAT = 2,
    }

    [SerializeField] TestPlayer player;
    [SerializeField] GameObject bullet;

    private GameObject bulletInstance;
    private Coroutine coroutine;
    private Coroutine floatCoroutine;
    private Rigidbody2D rb;


    private float speed;

    private float playerSpeed;
    [SerializeField] float playerA = 5.0f;
    private float oldPlayerA;
    private float oldSpeed;
    private float floatCnt;
    [SerializeField] float floatMax = 2;
    [SerializeField] private bool floatFlag = false;

    private int bulletMax = 3;
    private int bulletCnt = 0;
    private float bulletSpeed;
    private float bulletA = 0.5f;
    private float oldBulletA;

    [SerializeField] private BulletMode bulletMode;

    IEnumerator BulletRevival()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (bulletCnt >= 0) bulletCnt--;
        }

    }

    IEnumerator BulletMove1()
    {
        Debug.Log("1:ìÆÇ´èoÇ∑");
        while (true)
        {
            bulletSpeed = speed * bulletA * Time.deltaTime;
            bulletInstance.transform.position -= new Vector3(0, bulletSpeed, 0);

            //bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, bulletSpeed, 0));


            bulletA += 0.1f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator BulletMove2()
    {
        player.moveFlg = false;
        Debug.Log("2:ìÆÇ´èoÇ∑");
        while (bulletInstance)
        {
            bulletSpeed = speed * bulletA * Time.deltaTime;
            playerSpeed = speed * playerA * Time.deltaTime;

            player.transform.position += new Vector3(0, playerSpeed, 0);
            bulletInstance.transform.position -= new Vector3(0, bulletSpeed, 0);

            //player.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, playerSpeed, 0));
            //bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, bulletSpeed, 0));


            bulletA += 0.1f;
            if (playerA <= 0.1f) playerA -= 0.2f;

            yield return new WaitForSeconds(0.01f);

        }
    }

    IEnumerator BulletMove3()
    {
        floatCoroutine = StartCoroutine(FloatTime());
        Debug.Log("3:ìÆÇ´èoÇ∑");
        while (true)
        {
            bulletSpeed = speed * bulletA * Time.deltaTime;
            bulletInstance.transform.position -= new Vector3(0, bulletSpeed, 0);

            //bulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, bulletSpeed, 0));

            bulletA += 0.1f;

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FloatTime()
    {
        floatFlag = true;
        floatCnt = 0;
        if(player.dropSpeed != 0) oldSpeed = player.dropSpeed;
        player.dropSpeed = 0;

        while (floatCnt < floatMax)
        {
            floatCnt++;
            yield return new WaitForSeconds(1f);
        }

        player.dropSpeed = oldSpeed;
        floatFlag = false;
        floatCnt = -1;
        StopCoroutine(floatCoroutine);
    }

    public void InstanceBullet()
    {
        if (bulletCnt < bulletMax)
        {
            if (bulletInstance == null && !floatFlag)
            {
                bulletInstance = Instantiate(bullet, new Vector3(player.transform.position.x, player.transform.position.y - 1, 0), Quaternion.identity);
                bulletA = 1.0f;
                //playerA = 5.0f;
                switch (bulletMode)
                {
                    case BulletMode.MODE_NO_RECOIL:
                        coroutine = StartCoroutine(BulletMove1());
                        break;
                    case BulletMode.MODE_RECOIL:
                        coroutine = StartCoroutine(BulletMove2());
                        break;
                    case BulletMode.MODE_FLOAT:
                        coroutine = StartCoroutine(BulletMove3());
                        break;
                }

                bulletCnt++;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<TestPlayer>();
        rb = GetComponent<Rigidbody2D>();
        bulletMode = BulletMode.MODE_NO_RECOIL;

        speed = player.dropSpeed;

        oldBulletA = bulletA;
        oldPlayerA = playerA;

        StartCoroutine(BulletRevival());
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletInstance != null)
        {
            if(player.transform.position.y - 5 > bulletInstance.transform.position.y)
            {
                StopCoroutine(coroutine);
                player.moveFlg = true;
                Destroy(bulletInstance.gameObject);
            }
        }


        if(Input.GetKeyDown(KeyCode.Alpha1)) bulletMode = BulletMode.MODE_NO_RECOIL;
        if(Input.GetKeyDown(KeyCode.Alpha2)) bulletMode = BulletMode.MODE_RECOIL;
        if(Input.GetKeyDown(KeyCode.Alpha3)) bulletMode = BulletMode.MODE_FLOAT;

        if(Input.GetKeyDown(KeyCode.R)) player.transform.position = new Vector3 (0, 0, 0);
    }
}
