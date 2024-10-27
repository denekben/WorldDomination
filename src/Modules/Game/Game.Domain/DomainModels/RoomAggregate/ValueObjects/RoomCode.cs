using System.Security.Cryptography;

namespace Game.Domain.RoomAggregate.ValueObjects
{
    public sealed record RoomCode
    {
        public string Value { get; private set; }

        private RoomCode(string value)
        {
            Value = value;
        }
        
        public static RoomCode Create(string? value = null)
        {
            if (value == null)
            {
                return new RoomCode(GenerateCode());
            }
            return new RoomCode(value);
        }

        private static string GenerateCode()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        }

        public static implicit operator RoomCode(string value) => Create(value);
        public static implicit operator string(RoomCode value) => value.Value;
    }
}