using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAIController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    private AudioManager myAudioManager;

    public string state;

    void Start()
    {
        GetAllReferences();
        state = "Unalerted";
    }

    // Update is called once per frame
    void Update()
    {
        StateBehaviour();
        Clocks();
    }

    private void GetAllReferences()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAudioManager = FindObjectOfType<AudioManager>();
        myAnimator = transform.GetComponentInChildren<Animator>();
    }

    private void Clocks()
    {

    }

    private void StateBehaviour()
    {
        switch (state)
        {
            case "Unalerted":
                
                break;
            case "Pursuing":
                
                break;
            default:
                break;
        }
    }
}
