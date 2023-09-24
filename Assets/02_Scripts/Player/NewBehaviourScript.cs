using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 				// System�� ���Խ����ش�
using System.Reflection; 	// System.Reflection �� ����ϰڴ�

public class Temp
{
    public float currentHP;
    public string name;

    public void Func()
    {
        Debug.Log("��� ���");
    }
}

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        Temp temp = new Temp();

        // Type �̶� Ŭ������ ������ Ÿ���� ������ ����ִ� Ŭ�����̴�.
        Type type;

        type = temp.GetType();  // temp�� �������ִ� ������Ÿ���� ������ �Ѱ��ش�
                                // type�� ���°� temp�� Ŭ���� �������� Temp ��ü�� �ƴϴ�.(Temp�� �Ӽ��� ����� ���� ������)
                                // �� ������ ���� temp�� ���� �ƴ϶�, ������ Ÿ���� ������ �������°��̴� (Temp ������Ÿ���� ����)
                                // ���⿡�� currenthp, attackdamage �̷��� ����!!��ü�� �ƴ�

        temp.Func();
        // type.Func(); type���� Ŭ���� ��ü���� �Ѱ��־��� ������ Func()�Լ��� �̿��� �� ����


        MethodInfo[] methos = type.GetMethods();
        foreach (MethodInfo method in methos)
        {
            Debug.Log("�޼ҵ��� �̸� : " + method.Name);
        }// �迭�� �̷���� TempŬ���� ���� ��� �Լ����� �α׷� Ȯ���غ���


        FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            Debug.Log("��� ������ �̸� : " + field.Name);
        }// �迭�� �̷���� TempŬ���� ���� ��� �������� �α׷� Ȯ���غ���


        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            Debug.Log("������Ƽ �̸� : " + property.Name);
        }// �迭�� �̷���� TempŬ���� ���� ��� �������� �α׷� Ȯ���غ���
    }
}
