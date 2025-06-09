using System;
using UnityEngine;

public enum ECurrencyType
{
    Gold,
    Diamond,

    Count
}

public class Currency : MonoBehaviour
{
    // ������ Ŭ������ ���� :
    // 1. ǥ������ �����Ѵ�.
    // -> ȭ���� ������ �� ��� ǥ���� �� �ִ�.
    // 2. ���Ἲ�� �����ȴ�. (���Ἲ : �������� ��Ȯ��/�ϰ���/��ȿ��)
    // -> ���� ���� 0 �̸� ���� / ������ ���� �ʰ� ����
    // 3. �����Ϳ� �����͸� �ٷ�� ������ �����ִ�. -> �������� ����.

    // �ڱ� �������� �ڵ尡 �ȴ�. (��ȹ���� �ǰ��� �ڵ尡 �ȴ�.)
    // ������(��ȹ��) ������ �Ͼ�� �ڵ忡 �ݿ��ϱ� ����.

    // ȭ�� '������' (������, ����, ����, ��ȹ���� �������� �ۼ��Ѵ�. : ��ȹ�ڶ� ���� ���ؾ� �Ѵ�.)
    private ECurrencyType _type;
    public ECurrencyType Type => _type;

    private int _value = 0;
    public int Value => _value;

    // �������� '��Ģ'�� �ִ�.
    public Currency(ECurrencyType type, int value)
    {
        if (value < 0)
        {
            throw new Exception("ȭ���� ���� 0 �̻��̾�� �մϴ�.");
        }
        _type = type;
        _value = value;
    }

    public void Add(int addedValue)
    {
        if (addedValue < 0)
        {
            throw new Exception("�߰� ���� ������ �� �� �����ϴ�.");
        }
        _value += addedValue;
    }

    public void Subtract(int subtractedValue)
    {
        if (subtractedValue < 0)
        {
            throw new Exception("���� ���� ������ �� �� �����ϴ�.");
        }
        if (_value < subtractedValue)
        {
            throw new Exception("���� ȭ�� �����մϴ�.");
        }
        _value -= subtractedValue;
    }
}
