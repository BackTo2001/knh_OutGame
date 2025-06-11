public class AccountPasswordSpecification : ISpecification<string>
{
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "비밀번호는 비어있을 수 없습니다.";
            return false;
        }

        // 암호화된 SHA256 해시(64자리)는 통과시킴
        if (value.Length == 64) return true;

        if (value.Length < 6 || value.Length > 12)
        {
            ErrorMessage = "비밀번호는 6자 이상 12자 이하이어야 합니다.";
            return false;
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
