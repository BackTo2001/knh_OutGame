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
                Debug.Log("파이어베이스 연결 성공");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                //TryRegister();
                Login();
            }
            else
            {
                UnityEngine.Debug.LogError($"파이어베이스 연결 실패 ${dependencyStatus}");
            }
        });
    }

    // DDD구조에 따르면 Init()외 전부 Repository에 들어가야할 코드

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
                Debug.LogError($" 랭킹 등록 실패: {task.Exception.Message}");
                return;
            }

            Debug.Log("랭킹 등록 성공");
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
        // 쿼리란 z컬렉션으로부터 데이터를 가져올때 어떻게 가져와라고 쓰는 명령문

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
