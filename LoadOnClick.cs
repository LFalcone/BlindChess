using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour
{
    static List<int> Choices = new List<int>();
    public Dropdown c1;
    public Dropdown c2;

    public void LoadScene(int level)
    {
        Choices.Add(c1.value);
        Choices.Add(c2.value);
        for (int i = 0; i < Choices.Count; i++)
        {
            print(Choices[i]);
        }
        Application.LoadLevel(level);
    }
}