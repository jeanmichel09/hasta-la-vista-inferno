using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelinePonte : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;
    public bool hasFinished;
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_played;
        director.stopped += Director_stopped;
    }

    private void Director_stopped(PlayableDirector obj)
    {
        hasFinished = true;
        controlPanel.SetActive(true);
    }

    private void Director_played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }

    public void StartTimeline()
    {
        director.Play();
    }
    public void StopTimeline()
    {
        director.Stop();
    }

}
