using System;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {

        // ��Ģ�� ��ü�� ĸ��ȭ�ؼ� ��
        // ���Ѵ�.
        // �׷��� �����ΰ� UI�� ��� "�� ��Ģ�� �����ϴ�?" ��� ����� ���·� �����Ѵ�.
        // ĸ��ȭ�� ��Ģ : ��(specification)


        // �̸��� ����
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // �г��� ����
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        // ��й�ȣ ����
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            throw new Exception(passwordSpecification.ErrorMessage);
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}
