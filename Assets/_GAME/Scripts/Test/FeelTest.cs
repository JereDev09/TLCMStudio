using MoreMountains.Feedbacks;
using System;
using UnityEngine;
using UnityEngine.Events;

public class FeelTest : MonoBehaviour
{
    [SerializeField] private MMF_Player feelPlayer;

    private void OnEnable()
    {
        feelPlayer.Events.OnComplete.AddListener(test);
    }

    private void test()
    {
        Debug.Log("termino evento xd");
    }
}
