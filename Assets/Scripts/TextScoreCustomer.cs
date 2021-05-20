using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScoreCustomer : MonoBehaviour
{
    public Text text;
    private float y;
    private float initY;
    public float fadeDuration = 2.0f;
    public float speed = 2.0f;
    void Start()
    {
        y = gameObject.transform.position.y + 1f;
        text = gameObject.GetComponent<Text>();
        text.text = "";
        initY = gameObject.transform.position.y;
        //StartCoroutine(FadeText(GetComponent<Text>()));
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text != "" && y >= gameObject.transform.position.y)
        {
            gameObject.transform.Translate(Vector3.up * 0.015f);
        }
        else if (y < gameObject.transform.position.y)
        {
            text.text = "";
            gameObject.transform.position = new Vector3(gameObject.transform.position.x , initY , gameObject.transform.position.z);
        }
        //this.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    //public IEnumerator FadeImage(SpriteRenderer _sprite)
    //{
    //    float fadespeed = (float)1.0 / fadeDuration;
    //    Color c = _sprite.color;
    //    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadespeed)
    //    {
    //        c.a = Mathf.Lerp(1, 0, t);
    //        _sprite.color = c;
    //        text.GetComponent<Text>().color = c;
    //        yield return true;
    //    }

    //    _sprite.color = c;
    //    Destroy(this.gameObject);
    //}

    //public IEnumerator FadeText(Text text)
    //{
    //    float fadespeed = (float)1.0 / fadeDuration;
    //    Color c = text.color;
    //    for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * fadespeed)
    //    {
    //        c.a = Mathf.Lerp(1, 0, t);
    //        text.color = c;
    //        text.GetComponent<Text>().color = c;
    //        yield return true;
    //    }

    //    text.color = c;
    //    Destroy(this.gameObject);
    //}
}
