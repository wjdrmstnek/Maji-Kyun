using UnityEngine;

public class ChangeSpoon : MonoBehaviour
{
    public ReadText RT;
    public StateManager SM;
    bool isSpoonFeed;

    void Awake()
    {
        if (Equals(RT, null))
            RT = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<ReadText>();
        if (Equals(SM, null))
            SM = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TableSpoon") && !isSpoonFeed)
        {
            //Debug.Log("스푼 교체");
            RT.OffTableSpoon();
            Destroy(GameObject.FindGameObjectWithTag("TableSpoon"));
            SoupSound();
            isSpoonFeed = true;
        }
        //if (Equals(other.gameObject.tag, "TableSpoon"))
        //{
        //    Debug.Log("스푼 교체");
        //    RT.OffTableSpoon();
        //    SoupSound();
        //}
    }
    void SoupSound()
    {
        SM.SfxSource.enabled = false;
        SM.SfxSource.clip = SM.VoiceDialogs[86];
        SM.SfxSource.enabled = true;
    }
}