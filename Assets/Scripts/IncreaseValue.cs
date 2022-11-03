using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IncreaseValue : MonoBehaviour
{
    //float v = 0;

    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis(Globals.HORIZONTAL_AXIS);
        if (Mathf.Abs(a) > 0)
        //{
        //    v = Mathf.MoveTowards(v, a * 5, Time.deltaTime);
        //}
        //else
        //{
        //    v = Mathf.MoveTowards(v, 0, Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    v = 0.5f;
        //}
        text.text = a.ToString("f2");
    }
}
