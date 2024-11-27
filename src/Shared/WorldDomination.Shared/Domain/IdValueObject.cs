using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WorldDomination.Shared.Domain
{
    public record IdValueObject
    {
        public Guid Value { get; private set; }

        public IdValueObject(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyIdException();
            }

            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static string ListToString(List<IdValueObject> list)
        {
            if(list == null || !list.Any())
                return string.Empty;

            return string.Join(',', list.Select(id=>id.ToString()));
        }

        public static List<IdValueObject> StringToList(string listString)
        {
            var ids = listString.Trim().Split(',');
            return ids.Select(id=>new IdValueObject(new Guid(id))).ToList();
        }

        public static implicit operator IdValueObject(Guid id)
            => new(id);

        public static implicit operator Guid(IdValueObject id)
            => id.Value;

        public static bool operator ==(IdValueObject? idVO, Guid id) => idVO?.Value == id;

        public static bool operator !=(IdValueObject? idVO, Guid id) => idVO?.Value != id;
    }

    public static class ListExtensions
    {
        public static List<IdValueObject> ToIdValueObjects(this List<Guid> guids)
        {
            return guids.Select(g => new IdValueObject(g)).ToList();
        }
    }
}
