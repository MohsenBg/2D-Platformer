using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private new ParticleSystem particleSystem;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_JUMPING = "IsJumping";

    private void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        animator = this.GetComponent<Animator>();
        playerJump = this.GetComponent<PlayerJump>();
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_RUNNING, playerMovement.IsRunning);
        animator.SetBool(IS_JUMPING, playerJump.IsJumping);
    }


    public void PlayDeadAndThen(Action callback)
    {
        playerMovement.IsDead = true;
        playerJump.IsDead = true;
        StartCoroutine(PlayDeadAnimation(callback));
    }

    private IEnumerator PlayDeadAnimation(Action callback)
    {

        var sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 0;

        Color color = sprite.color;
        color.a = 0;
        sprite.color = color;

        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.duration);
        callback?.Invoke();
    }
}
