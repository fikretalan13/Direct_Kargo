using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CikisBanti : MonoBehaviour
{
    public string sehirAdi;

    [SerializeField] Sprite[] isiklar;
    [SerializeField] SpriteRenderer isikSprite;

    public Transform varisNoktasi;
    //--ospiyonel

    public void SecimIslemi(int durum)
    {
        isikSprite.sprite = isiklar[durum];
    }
}
