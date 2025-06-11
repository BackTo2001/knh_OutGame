using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;  // 결과 텍스트
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;  // 로그인용 아이디
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordComfirmInputField;
    public Button ConfirmButton;   // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;

    [Header("회원가입")]
    public UI_InputFields RegisterInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "20250611";



    // 게임 시작하면 로그인 켜주고 회원가입은 꺼주고..
    private void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);

        LoginInputFields.ResultText.text = string.Empty;
        RegisterInputFields.ResultText.text = string.Empty;
    }

    // 회원가입하기 버튼 클릭
    public void OnClickGoToRegisterButton()
    {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }


    // 회원가입
    public void Register()
    {
        // 1. 이메일 입력을 확인한다.
        string email = RegisterInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();

        if (!emailSpecification.IsSatisfiedBy(email))
        {
            RegisterInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        // 2. 닉네임 입력을 확인한다.
        string nickname = RegisterInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            RegisterInputFields.ResultText.text = nicknameSpecification.ErrorMessage;
            return;
        }

        // 3. 1차 비밀번호 입력을 확인한다.
        string password = RegisterInputFields.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        // 4. 2차 비밀번호 입력을 확인하고, 1차 비밀번호 입력과 같은지 확인한다.
        string password2 = RegisterInputFields.PasswordComfirmInputField.text;
        if (string.IsNullOrEmpty(password2))
        {
            RegisterInputFields.ResultText.text = "비밀번호를 입력해주세요.";
            return;
        }

        if (password != password2)
        {
            RegisterInputFields.ResultText.text = "비밀번호가 다릅니다.";
            return;
        }


        Result result = AccountManager.Instance.TryRegister(email, nickname, password);
        if (result.IsSuccess)
        {
            // 5. 로그인 창으로 돌아간다.
            // (이때 아이디는 자동 입력되어 있다.)
            OnClickGoToLoginButton();
        }
        else
        {
            // 6. 실패 메시지를 출력한다.
            RegisterInputFields.ResultText.text = result.Message;
        }

    }


    public void Login()
    {
        // 1. 이메일 입력을 확인한다.
        string email = LoginInputFields.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            LoginInputFields.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            LoginInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            return;
        }

        if (AccountManager.Instance.TryLogin(email, password))
        {
            SceneManager.LoadScene("1");
        }
        else
        {
            LoginInputFields.ResultText.text = "이메일이 중복되었습니다.";
        }

    }
}