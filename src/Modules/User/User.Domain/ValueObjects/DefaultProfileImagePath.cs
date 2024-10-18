namespace User.Domain.ValueObjects
{
    public sealed record DefaultProfileImagePath
    {
        public string Value { get; private set; }

        private DefaultProfileImagePath(string value) { 
            Value = value;
        }

        public static DefaultProfileImagePath Create(string? value = null)
        {
            if(value == null)
            {
                return new DefaultProfileImagePath(GenerateDefaultPath());
            }
            return new DefaultProfileImagePath(value);
        }

        private static string GenerateDefaultPath()
        {
            Random _random = new();
            return _random.Next(10).ToString();
        }

        public static implicit operator DefaultProfileImagePath(string value) => Create(value);
        public static implicit operator string(DefaultProfileImagePath value) => value.Value;
    }
}
