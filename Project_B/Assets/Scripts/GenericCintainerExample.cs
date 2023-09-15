using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCintainerExample : MonoBehaviour
{
    private GenericContainer<int> intContainer;
    private GenericContainer<string> stringContainer;

    void Start()
    {
        intContainer = new GenericContainer<int>(10);
        stringContainer = new GenericContainer<string>(15);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            intContainer.Add(Random.Range(0, 100));
            DisplayContainerItems(intContainer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string randomString = "item" + Random.Range(0, 100);
            stringContainer.Add(randomString);
            DisplayContainerItems(stringContainer);
        }
    }

    private void DisplayContainerItems<T>(GenericContainer<T> container)
    {//생성한 컨테이너를 Debug.Log에서 볼 수 있게 만든 함수 
        T[] items = container.GetItems();
        string temp = "";
        for (int i = 0; i < items.Length; i++) //컨테이너를 순환해서 배열값을 문자열로 변환
        {
            if (items[i] != null)
            {
                temp += items[i].ToString() + " - ";    //제너릭 형태값를 String 으로 변환
            }
            else
            {
                temp += "Empty - ";                     //비어있으면 Empty 문자열
            }
        }
        Debug.Log(temp);
    }
}
