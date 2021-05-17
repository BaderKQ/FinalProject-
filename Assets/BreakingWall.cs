using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWall : MonoBehaviour
{
    public float MaxHP = 5;
    public float CurrentHP = 5;

    public GameObject Sprite5;
    public GameObject Sprite4;
    public GameObject Sprite3;
    public GameObject Sprite2;
    public GameObject Sprite1;


    void Start()
    {
        CurrentHP = MaxHP;
        if (Sprite1 != null)
        {
            if (Sprite2 == null) Sprite2 = Sprite1;
            if (Sprite3 == null) Sprite3 = Sprite2;
            if (Sprite4 == null) Sprite4 = Sprite3;
            if (Sprite5 == null) Sprite5 = Sprite4;
        }
        UpdateState();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerStrike"))
        {
            CurrentHP -= 1;
            UpdateState();
        }
    }

    void UpdateState()
    {
        switch (CurrentHP)
        {
            case 5:
                Sprite5.SetActive(true);
                Sprite4.SetActive(false);
                Sprite3.SetActive(false);
                Sprite2.SetActive(false);
                Sprite1.SetActive(false);
                break;
            case 4:
                Sprite5.SetActive(false);
                Sprite4.SetActive(true);
                Sprite3.SetActive(false);
                Sprite2.SetActive(false);
                Sprite1.SetActive(false);
                break;
            case 3:
                Sprite5.SetActive(false);
                Sprite4.SetActive(false);
                Sprite3.SetActive(true);
                Sprite2.SetActive(false);
                Sprite1.SetActive(false);
                break;
            case 2:
                Sprite5.SetActive(false);
                Sprite4.SetActive(false);
                Sprite3.SetActive(false);
                Sprite2.SetActive(true);
                Sprite1.SetActive(false);
                break;
            case 1:
                Sprite5.SetActive(false);
                Sprite4.SetActive(false);
                Sprite3.SetActive(false);
                Sprite2.SetActive(false);
                Sprite1.SetActive(true);
                break;
            case 0:
                Destroy(gameObject);
                break;
            default:
                Sprite5.SetActive(true);
                Sprite4.SetActive(false);
                Sprite3.SetActive(false);
                Sprite2.SetActive(false);
                Sprite1.SetActive(false);
                break;
        }
    }
}
