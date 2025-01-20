using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WorldDomination.Shared.Domain
{
    public sealed record IdValueObject
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

        public static implicit operator IdValueObject(Guid id)
            => new(id);

        public static implicit operator Guid(IdValueObject id)
            => id.Value;

        public static bool operator ==(IdValueObject? idVO, Guid id) => idVO?.Value == id;
        public static bool operator !=(IdValueObject? idVO, Guid id) => idVO?.Value != id;
        public static bool operator ==(Guid id, IdValueObject? idVO) => id == idVO?.Value;
        public static bool operator !=(Guid id, IdValueObject? idVO) => id != idVO?.Value;
    }

    public static class Extensions
    {
        public static List<IdValueObject> GuidsToVO(this List<Guid> guids)
        {
            return guids.Select(g => new IdValueObject(g)).ToList();
        }

        public static Dictionary<IdValueObject, int> GuidsToVO(this Dictionary<Guid, int> dict)
        {
            return dict.Select(pair => new KeyValuePair<IdValueObject, int>(new IdValueObject(pair.Key), pair.Value))
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        // For List<> 
        public static string SerializeList(this List<IdValueObject> list)
        {
            if (list == null || !list.Any())
                return string.Empty;

            var guids = list.ConvertAll(vo => vo.Value);

            return JsonSerializer.Serialize(guids);
        }

        public static List<IdValueObject> DeserializeList(this string listString)
        {
            if (string.IsNullOrWhiteSpace(listString)) return new List<IdValueObject>();

            var guids = JsonSerializer.Deserialize<List<Guid>>(listString);
            
            var idValueObjects = new List<IdValueObject>();
            foreach (var guid in guids)
            {
                idValueObjects.Add(new IdValueObject(guid));
            }

            return idValueObjects;
        }

        // For Dictionary<,>
        public static string SerializeDictionary(this Dictionary<IdValueObject, int> dict)
        {
            if (dict == null || !dict.Any()) return string.Empty;

            var serializableDict = dict.ToDictionary(kvp => kvp.Key.Value.ToString(), kvp => kvp.Value);

            return JsonSerializer.Serialize(serializableDict);
        }

        public static string SerializeDictionaryForGuid(this Dictionary<Guid, int> dict)
        {
            if (dict == null || !dict.Any()) return string.Empty;

            var serializableDict = dict.ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value);

            return JsonSerializer.Serialize(serializableDict);
        }

        public static Dictionary<IdValueObject, int> DeserializeDictionary(this string dictString)
        {
            if (string.IsNullOrWhiteSpace(dictString)) return new Dictionary<IdValueObject, int>();

            var deserializedDict = JsonSerializer.Deserialize<Dictionary<Guid, int>>(dictString);

            var resultDict = new Dictionary<IdValueObject, int>();
            foreach (var kvp in deserializedDict)
            {
                var idValueObject = new IdValueObject(kvp.Key); 
                resultDict[idValueObject] = kvp.Value; 
            }

            return resultDict;
        }

        public static Dictionary<Guid, int> DeserializeDictionaryForGuid(this string dictString)
        {
            if (string.IsNullOrWhiteSpace(dictString)) return new Dictionary<Guid, int>();

            var deserializedDict = JsonSerializer.Deserialize<Dictionary<Guid, int>>(dictString);

            var resultDict = new Dictionary<Guid, int>();
            foreach (var kvp in deserializedDict)
            {
                var idValueObject = kvp.Key;
                resultDict[idValueObject] = kvp.Value;
            }

            return resultDict;
        }
    }
}
