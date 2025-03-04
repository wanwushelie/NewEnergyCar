using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using StarterAssets;

public class PlayerOprate : MonoBehaviour
{
    private bool isPlaying =  false;
    public GameObject PlayerFollowCamera;
    //按键F操作
    IEnumerator OnOprerate() 
    {
        Debug.Log("OnOperate");
        if(list.Count > 0 && !isPlaying) {
            isPlaying = true;
            var thirdPersonController = GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = false;
            var director = list[0];
            list.RemoveAt(0);
            var pos = director.transform.Find("Target1"); 
            // transform.position = pos.position;
            // transform.rotation = pos.rotation;
            PlayerFollowCamera.SetActive(false);
            director.transform.Find("VC1").gameObject.SetActive(true);
            while (transform.position != pos.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos.position, 0.5f * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, pos.rotation, 360 * Time.deltaTime);
                yield return null;
                
            }
            var animator = GetComponent<Animator>();
            foreach (var output in director.playableAsset.outputs)
            {
                if (output.streamName == "Player Track")
                {
                    director.SetGenericBinding(output.sourceObject, animator);
                    break;
                }
            }
            director.Play();
            while (director.state == PlayState.Playing) {
                yield return null;
            }
            thirdPersonController.enabled = true;
            PlayerFollowCamera.SetActive(true);  
            isPlaying = false;
        }
    }

    //碰撞检测 播放列表
    List<PlayableDirector> list = new List<PlayableDirector>();

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "npc")
        {
            Debug.Log("OnTriggerEnter");
            var director = other.gameObject.GetComponent<PlayableDirector>();
            if (director != null && !list.Contains(director))
            {
                list.Add(director);
            }
            Debug.Log(list.Count);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "npc")
        {
            Debug.Log("OnTriggerExit");
            var director = other.gameObject.GetComponent<PlayableDirector>();
            if (director != null && list.Contains(director))
            {
                list.Remove(director);
            }
            Debug.Log(list.Count);
        }
    }
}
