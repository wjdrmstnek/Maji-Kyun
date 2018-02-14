using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteText : MonoBehaviour
{
    // 스크립트 텍스트
    public Text ScriptText;
    // 대사간 딜레이
    public float scriptDelay = 4.0f;

    public ReadText RT;

    void Awake()
    {
        if (Equals(ScriptText, null))
            ScriptText = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<Text>();
        if (Equals(RT, null))
            RT = GameObject.FindGameObjectWithTag("ScriptText").GetComponent<ReadText>();

        StartCoroutine(ScriptSliding(ScriptText));
    }

    public IEnumerator ScriptSliding(Text scriptText)
    {
        scriptText = ScriptText;

        // 스크립트 지속시간
        yield return new WaitForSeconds(scriptDelay);
        // 스크립트 내용 초기화
        scriptText.text = "";
        //Debug.Log("지워지워 " + RT.scriptIndex);
        StopCoroutine("ScriptSliding");
    }
}