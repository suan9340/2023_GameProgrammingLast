using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 				// System을 포함시켜준다
using System.Reflection; 	// System.Reflection 를 사용하겠다

public class Temp
{
    public float currentHP;
    public string name;

    public void Func()
    {
        Debug.Log("어떠한 기능");
    }
}

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Temp temp = new Temp();

        // Type 이란 클래스는 테이터 타입의 정보가 담겨있는 클래스이다.
        Type type;

        type = temp.GetType();  // temp가 가지고있는 데이터타입의 정보를 넘겨준다
                                // type에 담기는건 temp의 클래스 정보이지 Temp 자체는 아니다.(Temp의 속성과 기능은 아직 못쓴다)
                                // 즉 가져온 것은 temp의 값이 아니라, 테이터 타입의 정보를 가져오는것이다 (Temp 데이터타입의 정보)
                                // 여기에는 currenthp, attackdamage 이런거 없어!!객체가 아님

        temp.Func();
        // type.Func(); type에는 클래스 자체만을 넘겨주었기 때문에 Func()함수를 이용할 수 없다


        MethodInfo[] methos = type.GetMethods();
        foreach (MethodInfo method in methos)
        {
            Debug.Log("메소드의 이름 : " + method.Name);
        }// 배열로 이루어진 Temp클래스 안의 모든 함수들을 로그로 확인해본다


        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            Debug.Log("멤버 변수의 이름 : " + field.Name);
        }// 배열로 이루어진 Temp클래스 안의 모든 변수들을 로그로 확인해본다


        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            Debug.Log("프로퍼티 이름 : " + property.Name);
        }// 배열로 이루어진 Temp클래스 안의 모든 변수들을 로그로 확인해본다
    }
}
