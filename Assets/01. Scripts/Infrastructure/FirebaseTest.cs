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
                Debug.Log("파이어베이스 연결 성공");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;

                //TryRegister();
                Login();
            }
            else
            {
                UnityEngine.Debug.LogError($"파이어베이스 연결 실패 ${dependencyStatus}");
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
                Debug.LogError($" 회원가입에 실패했습니다 :${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다.: {0} ({1})",
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
                Debug.LogError($"로그인 실패 : {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그인 성공 : {0} ({1})",
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
            DisplayName = "비모",
        };
        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"닉네임 변경 실패 : {task.Exception.Message}");
                return;
            }

            Debug.Log("닉네임 변경 성공~!");
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
