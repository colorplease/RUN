using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageText : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI text;
    [SerializeField]TextMeshProUGUI textShadow;
    public bool screenShakeEnabled;
    Vector3 startPosition;
    bool shouldShake;
    [SerializeField]float power;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if(shouldShake)
        {
                transform.localPosition = startPosition+Random.insideUnitSphere*power;
        }
        else
        {
                transform.localPosition = startPosition;
        }
    }

    public void NewText(string newMessage, float displayTime)
    {
        text.text = newMessage;
        textShadow.text = newMessage;
        shouldShake = true;
        StartCoroutine(displayText(displayTime));
    }

    IEnumerator displayText(float displayTime)
    {
        yield return new WaitForSeconds(displayTime);
        text.text = "";
        textShadow.text = "";
        shouldShake = false;
    }
}
