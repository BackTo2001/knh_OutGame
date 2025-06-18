using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
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
            }
            else
            {
                UnityEngine.Debug.LogError($"���̾�̽� ���� ���� ${dependencyStatus}");
            }
        });
    }
}
