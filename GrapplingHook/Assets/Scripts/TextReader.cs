using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    private string fileName = "this.txt";
    private string filePath;
    public string txt, line1, line2;
    [SerializeField] Text spkr;
    [SerializeField] Text speech;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ReadTxt()
    {
        filePath = "C:/Users/Jawad Usman/Documents/GitHub/Teleport/GrapplingHook/Assets/Scripts/this.txt";//Application.dataPath + "/" + fileName; /*Path.Combine(Application.dataPath, fileName);*/

        System.IO.StreamReader MyReader = new System.IO.StreamReader(filePath);


        //txt = File.ReadAllText(filePath);

        line1 = MyReader.ReadLine();
        line2 = MyReader.ReadLine();

        Debug.Log(line2);
    }

    // Update is called once per frame
    void Update()
    {
        ReadTxt();

        spkr.text = line1;
        speech.text = line2;
    }
}
