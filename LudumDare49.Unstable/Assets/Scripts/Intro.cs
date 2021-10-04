using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    void Start()
    {
        //StartCoroutine(streamVideo(movie));
    }

    /*private IEnumerator streamVideo(string video)
    {
        //Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        //yield return new WaitForEndOfFrame();
        //SceneManager.LoadScene("SampleScene");
    }
    */

    // Update is called once per frame
    void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}


