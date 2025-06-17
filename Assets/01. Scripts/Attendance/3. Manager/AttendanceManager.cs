using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;

    [SerializeField]
    private List<AttendanceSO> _attendancesSOList;

    private List<Attendance> _attendances;

    public event Action OnDataChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {
        _attendances = new List<Attendance>(_attendancesSOList.Count);
        DateTime today = DateTime.Today;

        foreach (var attendanceSO in _attendancesSOList)
        {
            if (attendanceSO.StartDate < today)
            {
                continue;
            }

            if (FindById(attendanceSO.Id) != null)
            {
                Debug.LogWarning($"Attendance with ID {attendanceSO.Id} already exists. Skipping duplicate.");
                continue;
            }

            Attendance attendance = new Attendance(attendanceSO.Id, attendanceSO.StartDate, today, 1);
            foreach (var attendanceRewardSO in attendanceSO.AttendanceRewards)
            {
                attendance.AddReward(new AttendanceReward(attendanceRewardSO.CurrencyType, attendanceRewardSO.Amount, false));
            }
            _attendances.Add(attendance);
        }

        StartCoroutine(Check_Coroutine());
    }

    private Attendance FindById(string id)
    {
        Attendance attendance = _attendances.Find(a => a.Id == id);

        return attendance;
    }

    public AttendanceDTO GetAttendance(string id)
    {
        Attendance attendance = FindById(id);
        if (attendance == null)
        {
            throw new Exception("Attendance Not Found");
        }
        return attendance.ToDTO();
    }

    public bool TryRewardClaim(string attendanceId, int index)

    {
        Attendance attendance = FindById(attendanceId);
        if (attendance == null)
        {
            return false;
        }

        if (attendance.TryClaim(index))
        {
            AttendanceRewardDTO reward = attendance.GetReward(index);

            return true;

            CurrencyManager.Instance.Add(reward.CurrencyType, reward.Amount);

            OnDataChanged?.Invoke();

        }
    }

    private IEnumerator Check_Coroutine()
    {
        var hourTimeWait = new WaitForSecondsRealtime(60 * 60); // 1시간 대기

        while (true)
        {
            DateTime today = DateTime.Today;

            foreach (Attendance attendance in _attendances)
            {
                // 묻지 말고 시켜라!
                // 출석일을 비교해서 count를 올리는 행위
                attendance.Check(today);
            }

            OnDataChanged?.Invoke();

            yield return hourTimeWait; // 1시간마다 체크
        }
    }
}
