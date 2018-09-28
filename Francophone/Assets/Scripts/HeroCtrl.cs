using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCtrl : MonoBehaviour {

    Vector2 targetPosition;
    Animator animator;

    public Button up;
    public Button down;
    public Button left;
    public Button right;

    const int STATE_RIGHT = 0;
    const int STATE_LEFT = 1;

    int _currentAnimationState = STATE_RIGHT;

    float speed = 3;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();

    }

    void changeState(int state)
    {

        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_RIGHT:
                animator.SetInteger("State", STATE_RIGHT);
                break;

            case STATE_LEFT:
                animator.SetInteger("State", STATE_LEFT);
                break;
        }

        _currentAnimationState = state;
    }

    public void UpClicked()
    {
        Vector2 position = transform.position;
        position.y = position.y + speed * Time.deltaTime;
        transform.position = position;

    }

    public void DownClicked()
    {
        Vector2 position = transform.position;
        position.y = position.y - speed * Time.deltaTime;
        transform.position = position;

    }
    public void LeftClicked()
    {
        changeState(STATE_LEFT);
        Vector2 position = transform.position;
        position.x = position.x - speed * Time.deltaTime;
        transform.position = position;

    }
    public void RightClicked()
    {
        changeState(STATE_RIGHT);
        Vector2 position = transform.position;
        position.x = position.x + speed * Time.deltaTime;
        transform.position = position;

    }
    // Update is called once per frame
    void Update () {



    }
}
