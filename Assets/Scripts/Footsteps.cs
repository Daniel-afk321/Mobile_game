using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource footstep_sound;
    [SerializeField]
    private AudioClip[] footstep_clip;
    private CharacterController characterControl;
    [HideInInspector]
    public float Volum_min, Volum_max;
    private float accumulated_Distance;
    [HideInInspector]
    public float step_Distance;
    void Awake()
    {
        footstep_sound = GetComponent<AudioSource>();
        characterControl = GetComponentInParent<CharacterController>();
    }


    void Update()
    {
        CheckToPlayFootSound();
    }

    void CheckToPlayFootSound()
    {
        if (!characterControl.isGrounded)
            return;
        if (characterControl.velocity.sqrMagnitude > 0)
        {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > step_Distance)
            {
                footstep_sound.volume = Random.Range(Volum_min, Volum_max);
                footstep_sound.clip = footstep_clip[Random.Range(0, footstep_clip.Length)];
                footstep_sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }
    }
}
