using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private Firebase.Auth.FirebaseAuth _auth;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("���̾�̽� ���� ����");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;

                //TryRegister();
                Login();
            }
            else
            {
                UnityEngine.Debug.LogError($"���̾�̽� ���� ���� ${dependencyStatus}");
            }
        });
    }

    private void TryRegister()
    {
        string email = "skgus917@gmail.com";
        string password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($" ȸ�����Կ� �����߽��ϴ� :${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("ȸ�����Կ� �����߽��ϴ�.: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    private void Login()
    {
        string email = "skgus917@gmail.com";
        string password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�α��� ���� : {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("�α��� ���� : {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            ProfileChange();
        });
    }

    private void ProfileChange()
    {
        var user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }
        var profile = new UserProfile
        {
            DisplayName = "���",
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"�г��� ���� ���� : {task.Exception.Message}");
                return;
            }

            Debug.Log("�г��� ���� ����~!");
        });
    }

    private void GetProfile()
    {
        Firebase.Auth.FirebaseUser user = _auth.CurrentUser;
        if (user == null) return;

        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, "firebase");
    }
}
