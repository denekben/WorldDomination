using User.Domain.Exceptions;

namespace User.Domain.ValueObjects
{
    public class ProfileImagePath
    {
        public string Value { get; private set; }

        private ProfileImagePath(string value) {
            Value = value;
        }

        public static ProfileImagePath Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new ProfileImagePath(value);
            }
            return new ProfileImagePath(value);
        }



        public static implicit operator ProfileImagePath(string value)
            => Create(value);

        public static implicit operator string(ProfileImagePath value)
            => value.Value;
    }
    
}
