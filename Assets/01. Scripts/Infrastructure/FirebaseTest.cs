using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;


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
                _db = FirebaseFirestore.DefaultInstance;

                //TryRegister();
                Login();
            }
            else
            {
                UnityEngine.Debug.LogError($"���̾�̽� ���� ���� ${dependencyStatus}");
            }
        });
    }

    // DDD������ ������ Init()�� ���� Repository�� ������ �ڵ�

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
        });

        ProfileChange();
        //AddMyRanking();
        GetRankings();
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

    private void AddMyRanking()
    {
        Ranking ranking = new Ranking("tetete@gmail.com", "xoxo", 2400);

        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email},
            { "Nickname", ranking.Nickname },
            { "Score", ranking.Score },
        };
        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($" ��ŷ ��� ����: {task.Exception.Message}");
                return;
            }

            Debug.Log("��ŷ ��� ����");
        });
    }

    private void GetMyRanking()
    {
        var email = "tetete@gmail.com";

        var docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> city = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in city)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }


    private void GetRankings()
    {
        // ������ z�÷������κ��� �����͸� �����ö� ��� �����Ͷ�� ���� ��ɹ�

        Query allRankingsQuery = _db.Collection("rankings").OrderByDescending("Score");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot allRankingsQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allRankingsQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> ranking = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ranking)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            }
        });
    }

}
