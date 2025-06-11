using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //private Rigidbody2D rb;
    public float DownSpeed = 5f;
    private float BaseDownSpeed;//元の速度
    private Coroutine BuffCoroutine;//実行中のバフコルーチンの制御

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        BaseDownSpeed = DownSpeed;
        StartCoroutine(DownPlayer());
    }

    public IEnumerator DownPlayer()
    {
        while (true)
        {
            Vector2 pos = transform.position;
            pos.y -= DownSpeed * Time.deltaTime;
            transform.position = pos;
            //rb.AddForce(Vector2.down * DownSpeed, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.01f);
            //yield return null;
        }
    }

    //バフ時間
    public void BuffTime(float upSpeed, float second)
    {
        if (BuffCoroutine != null)
        {
            StopCoroutine(BuffCoroutine);
        }

        BuffCoroutine = StartCoroutine(BuffRoutine(upSpeed, second));
    }

    //加算処理
    private IEnumerator BuffRoutine(float upSpeed, float second)
    {
        DownSpeed += upSpeed;
        yield return new WaitForSeconds(second);
        DownSpeed -= upSpeed;
        BuffCoroutine = null;
        Debug.Log(DownSpeed);
    }
}
