namespace UserAccess.Domain.ValueObjects
{
    public record UserRole
    {
        public static UserRole Admin => new UserRole(nameof(Admin));
        public static UserRole Member => new UserRole(nameof(Member));
        public string Value { get; }

        public UserRole(string value)
        {
            Value = value;
        }
    }
}
