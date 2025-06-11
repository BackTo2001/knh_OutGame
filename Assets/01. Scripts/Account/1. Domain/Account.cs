using System;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {

        // 규칙을 객체로 캡슐화해서 분
        // 리한다.
        // 그래서 도메인과 UI는 모두 "이 규칙을 만족하니?" 라고 물어보는 형태로 구현한다.
        // 캡슐화한 규칙 : 명세(specification)


        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // 닉네임 검증
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        // 비밀번호 검증
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
