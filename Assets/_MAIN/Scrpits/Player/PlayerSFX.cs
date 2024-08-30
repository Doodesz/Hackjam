using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] BoxAnimationTrigger boxTrigger;

    [SerializeField] AudioSource drawShard;
    [SerializeField] AudioSource connectShard;
    [SerializeField] AudioSource enterPortal;
    [SerializeField] AudioSource exitPortal;
    [SerializeField] AudioSource nextDialogue;
    [SerializeField] AudioSource jump;
    //[SerializeField] AudioSource land;
    [SerializeField] AudioSource lever;
    [SerializeField] AudioSource pressurePlate;
    [SerializeField] AudioSource push;
    [SerializeField] AudioSource specialWall;
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource openWall;


    public bool isPlayingWalk;
    public bool isPlayingPush;

    public static PlayerSFX Instance;

    private void Start()
    {
        Instance = this;
    }

    public void PlayDrawShard() { drawShard.Play(); }
    public void PlayConnectShard() { connectShard.Play(); }
    public void PlayEnterPortal() { enterPortal.Play(); }
    public void PlayExitPortal() { exitPortal.Play(); }
    public void PlayNextDialogue() { nextDialogue.Play(); }
    public void PlayJump() { jump.Play(); }
    //public void PlayLand() { land.Play(); }
    public void PlayLever() { lever.Play(); }
    public void PlayPressurePlate() { pressurePlate.Play(); }
    public void PlayPush()
    {
        isPlayingPush = true;
        push.Play(); 
    }
    public void StopPush() 
    { 
        isPlayingPush = false;
        push.Stop(); 
    }
    public void PlaySpecialWall() { specialWall.Play(); }
    public void PlayWalk() 
    { 
        isPlayingWalk = true;
        walk.Play();
    }
    public void StopWalk()
    {
        isPlayingWalk = false;
        walk.Pause();
    }
    public void PlayOpenWall() { openWall.Play(); }
}
