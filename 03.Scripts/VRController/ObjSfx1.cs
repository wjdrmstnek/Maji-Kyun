using UnityEngine;

public class ObjSfx1 : MonoBehaviour
{
    //public AudioSource CanSound;
    //public AudioSource WBSound;
    bool checkPlayed;

    //void OnTriggerEnter(Collider other)
    //{
    //    if ((Equals(other.tag, "Beer") || Equals(other.tag, "Wastepaperbox")) && !checkPlayed)
    //    {
    //        other.GetComponent<AudioSource>().enabled = false;
    //        other.GetComponent<AudioSource>().enabled = true;
    //        Debug.Log("소리남");
    //        checkPlayed = true;
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (Equals(other.tag, "Beer") || Equals(other.tag, "Wastepaperbox"))
    //    {
    //        other.GetComponent<AudioSource>().enabled = false;
    //        checkPlayed = false;
    //        Debug.Log("소리꺼짐");
    //    }
    //}


    void OnCollisionEnter(Collision other)
    {
        if (Equals(other.gameObject.tag, "Beer") && !checkPlayed)
        {
            //other.gameObject.GetComponent<AudioSource>().enabled = false;
            //other.gameObject.GetComponent<AudioSource>().enabled = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("캔이랑 부딪혀서 소리남");
            checkPlayed = true;
        }
    }


    //void OnCollisionExit(Collision other)
    //{
    //    if (Equals(other.gameObject.tag, "Beer"))
    //    {
    //        //other.gameObject.GetComponent<AudioSource>().enabled = false;
    //        //other.gameObject.GetComponent<AudioSource>().enabled = true;
    //        checkPlayed = false;
    //        Debug.Log("소리꺼짐");
    //    }
    //}


}