using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float SpeedBuff = 5f;
    public enum ItemType
    {
        SpeedUp,
        unko
    }

    public ItemType type;
    private void OnItemPick(GameObject Player)
    {
        switch (type)
        {
            case ItemType.SpeedUp:
                Player.GetComponent<PlayerManager>().BuffTime(SpeedBuff, 3);
                break;
        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            OnItemPick(other.gameObject); // プレイヤーが触れたら取得処理を呼ぶ
        }
    }
}
