namespace User.Domain.ValueObjects
{
    public sealed class ProfileImagePath
    {
        public string Value { get; private set; }

        private ProfileImagePath(string value) {
            Value = value;
        }

        public static ProfileImagePath Create(string value)
        {
            return new ProfileImagePath(value);
        }

        public static implicit operator ProfileImagePath(string value) => Create(value);
        public static implicit operator string(ProfileImagePath value) => value.Value;
    }
    
}
