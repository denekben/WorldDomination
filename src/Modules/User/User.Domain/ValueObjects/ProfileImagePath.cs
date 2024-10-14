using User.Domain.Exceptions;

namespace User.Domain.ValueObjects
{
    public class ProfileImagePath
    {
        public string Value { get; private set; }

        private ProfileImagePath(string value) {
            Value = value;
        }

        public static ProfileImagePath Create(string? value = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ProfileImagePath(GenerateRandomCode().ToString());
            }
            return new ProfileImagePath(value);
        }

        private static int GenerateRandomCode()
        {
            Random _random = new Random();
            return _random.Next(10);
        }

        public static implicit operator ProfileImagePath(string value)
            => Create(value);

        public static implicit operator string(ProfileImagePath value)
            => value.Value;
    }
    
}
