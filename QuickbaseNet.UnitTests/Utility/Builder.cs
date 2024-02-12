namespace QuickbaseNet.UnitTests.Utility;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using AutoBogus;

using Bogus;

[ExcludeFromCodeCoverage]
public class Builder
{
    public T Build<T>() where T : class
    {
        var binder = new AutoBinder();

        Faker<T> model = new AutoFaker<T>(binder)
            .RuleForType(typeof(uint), rule => rule.Random.UInt(1, int.MaxValue))
            .RuleForType(typeof(uint?), rule => (uint?)rule.Random.UInt(1, int.MaxValue))
            .RuleForType(typeof(int), rule => rule.Random.Int())
            .RuleForType(typeof(int?), rule => (int?)rule.Random.Int())
            .RuleForType(typeof(DateTime), rule =>
            {
                DateTime date = rule.Date.Recent();
                return new DateTime(
                    date.Year,
                    date.Month,
                    date.Day
                );
            })
            .RuleForType(typeof(DateTime?), rule =>
            {
                DateTime date = rule.Date.Recent();
                return (DateTime?)new DateTime(
                    date.Year,
                    date.Month,
                    date.Day
                );
            })
            .RuleForType(typeof(DateTimeOffset?), rule =>
            {
                DateTimeOffset date = rule.Date.Recent();
                return (DateTimeOffset?)new DateTime(
                    date.Year,
                    date.Month,
                    date.Day
                );
            })
            .RuleForType(typeof(byte), rule => rule.Random.Byte())
            .RuleForType(typeof(byte?), rule => (byte?)rule.Random.Byte())
            .RuleForType(typeof(sbyte), rule => rule.Random.SByte())
            .RuleForType(typeof(string), rule => rule.Random.AlphaNumeric(10))
            .RuleForType(typeof(decimal), rule => rule.Finance.Amount(min: 0.01M, max: 99999.99M, decimals: 2))
            .RuleForType(typeof(decimal?), rule => (decimal?)rule.Finance.Amount(min: 0.01M, max: 99999.99M, decimals: 2))
            .RuleForType(typeof(ushort), rule => rule.Random.UShort())
            .RuleForType(typeof(short), rule => rule.Random.Short())
            .RuleForType(typeof(long), rule => rule.Random.Long())
            .RuleForType(typeof(ulong), rule => rule.Random.ULong())
            .RuleForType(typeof(bool), rule => rule.Random.Bool())
            .RuleForType(typeof(bool?), rule => (bool?)rule.Random.Bool());

        model = AddEnumRules(model, binder);

        //int seed = DateTime.UtcNow.Millisecond;
        //return model.UseSeed(seed).Generate();
        return model.Generate();
    }

    /// <summary>
    /// Adds the ability to generate random valid enum values
    /// </summary>
    /// <remarks>
    /// Bogus currently doesn't appear to have a way to create rules for types that aren't well known at compile time. To generate random values for enum properties
    /// we have to find properties on <typeparamref name="T"/> that are enum and get a list of valid values for that enum and pick one of those randomly.
    /// 
    /// Bogus appears to do similar things when creating rules for well known types too, so this is at least similar.
    /// 
    /// This also excludes enum members whose name is "None" when it is the first member of the enum as that is usually not a valid value.
    /// This follows the convention for other nubmer types not generating zeroes
    /// </remarks>
    private Faker<T> AddEnumRules<T>(Faker<T> faker, IBinder binder) where T : class
    {
        // find enum properties using the binder
        IEnumerable<PropertyInfo> enumProperties = binder.GetMembers(typeof(T))
            .Select(item => item.Value)
            .OfType<PropertyInfo>()
            .Where(item => item.PropertyType.IsEnum);

        Faker<T> result = faker;
        if (enumProperties.Any())
        {
            result = result.FinishWith((fk, target) =>
            {
                foreach (PropertyInfo enumProperty in enumProperties)
                {
                    // None = 0 is typically not a valid value, so for the sake of generating sane values it will be skipped
                    var minIndex = 0;
                    string[] enumNames = Enum.GetNames(enumProperty.PropertyType);
                    if (enumNames[0].ToLower() == "none")
                    {
                        minIndex = 1;
                    }

                    Array enumValues = Enum.GetValues(enumProperty.PropertyType);
                    var randomIndex = fk.Random.Int(minIndex, enumValues.Length - 1);
                    object randomEnumValue = enumValues.GetValue(randomIndex);

                    enumProperty.SetValue(target, randomEnumValue);
                }
            });
        }

        return result;
    }

    public IEnumerable<T> Build<T>(int howMany) where T : class
    {
        var models = new List<T>();
        for (int i = 0; i < howMany; i++)
        {
            T model = Build<T>();
            models.Add(model);
        }

        return models;
    }
}