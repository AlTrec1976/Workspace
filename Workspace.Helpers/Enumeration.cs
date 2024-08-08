using System.Reflection;

namespace Workspace.Helpers;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    //единожды при инициализации
    private int Value { get; init; }
    private string Name { get; init; }
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();
    
    //Enumerations хранит все известные перечисления, созданные на этапе
    //инициализации. Это позволяет избежать повторного создания одинаковых экземпляров перечислений
    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    //Позволяет получить перечисление по его числовому значению.
    public static TEnum? FromValue(int value)
    {
        return Enumerations.TryGetValue(value, out TEnum? enumeration) ? enumeration : default;
    }
    //Позволяет получить перечисление по его строковому названию
    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(x => x.Name == name);
    }
    //Реализует сравнение двух перечислений по значению и типу
    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other &&
               Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
    //происходит динамическое заполнение словаря Enumerations, где ключом является значение перечисления,
    //а значением - сам объект перечисления
    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        //Используется typeof для получения типа перечисления (TEnum).
        var enumerationType = typeof (TEnum);

        //1. Метод GetFields используется для получения всех полей перечисления с указанными
        //флагами доступа (BindingFlags.Public, BindingFlags.Static, BindingFlags.FlattenHierarchy).
        //2. Фильтрация выполняется с помощью LINQ, чтобы оставить только те поля, тип которых
        //может быть присвоен типу перечисления (enumerationType.IsAssignableFrom(field.FieldType)).
        //3. Значения этих полей извлекаются и приведены к типу
        //перечисления ((TEnum)field.GetValue(default)).
        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(FieldInfo => enumerationType.IsAssignableFrom(FieldInfo.FieldType))
            .Select(FieldInfo => (TEnum) FieldInfo.GetValue(default)!);
        return fieldsForType.ToDictionary(x => x.Value);
    }
}