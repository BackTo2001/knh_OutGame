public class AccountPasswordSpecification : ISpecification<string>
{
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "��й�ȣ�� ������� �� �����ϴ�.";
            return false;
        }

        // ��ȣȭ�� SHA256 �ؽ�(64�ڸ�)�� �����Ŵ
        if (value.Length == 64) return true;

        if (value.Length < 6 || value.Length > 12)
        {
            ErrorMessage = "��й�ȣ�� 6�� �̻� 12�� �����̾�� �մϴ�.";
            return false;
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
