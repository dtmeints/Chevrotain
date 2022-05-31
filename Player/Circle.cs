using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{

    [SerializeField] PlayerInputManager _pim;
    [SerializeField] PlayerData _pd;
    Animator anim;
    Inventory inventory;
    Player player;
    private int counter;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        inventory = GetComponent<Inventory>(); 
        player = GetComponent<Player>();
        counter = _pd.circleSaltCost;
    }

    public void StartCircle()
    {
        anim.SetBool("isCircling", true);
        StartCoroutine(ContinueCircle());
    }

    public IEnumerator ContinueCircle()
    {
        //exit and reset
        if (!_pim.isCirclePressed || inventory.Salt <= 0)
        {
            anim.SetBool("isCircling", false);
            counter = _pd.circleSaltCost;
            yield break;
        }
        //perform action and count down
        if (counter > 0)
        {
            inventory.SetSalt(-1);
            counter--;
            yield return new WaitForSeconds(_pd.circleLength / _pd.circleSaltCost);
            StartCoroutine(ContinueCircle());
            yield break;
        }
        //exit at end
        else if (counter <= 0)
        {
            player.GetHealed(1);
            anim.SetBool("isCircling", false);
            counter = _pd.circleSaltCost;
            yield break;
        }
    }

    private void InterruptCircle(Player player)
    {
        StopCoroutine(nameof(ContinueCircle));
        counter = 8;
        anim.SetBool("isCircling", false);
    }

    private void OnEnable()
    {
        Player.OnHurt += InterruptCircle;
    }

    private void OnDisable()
    {
        Player.OnHurt -= InterruptCircle;
    }
}
