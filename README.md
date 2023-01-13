# ConvertEx
ConvertEx is a capable extension to `System.Convert`. It does its best to return a value instead of throwing exceptions.

## Features
- Supports `Nullable<T>` types.
- Out-of-box converters for non-primitive types e.g. converts a string or number to `TimeSpan` and vice versa.
- Supports custom converters to convert to and from any type.
- Falls back to `System.Convert`, meaning that everything that `System.Convert` can do, `ConvertEx` does too.

## <a href="https://www.nuget.org/packages/ConvertEx">NuGet Package</a>

```powershell
Install-Package ConvertEx -Version 1.0.5
```

## Usage Examples

### Change type using default converter

```csharp
var integer = ConvertEx.ChangeType("123", typeof(int)); // integer = 123
var uri = ConvertEx.ChangeType("https://github.com/", typeof(Uri)) // Works like a charm - No InvalidCastException!
```

### Convert nullables

```csharp
int? num = 5;
var dbl = ConvertEx.ChangeType<double?>(num); // dbl = 5d
```

### Create a custom converter

```csharp
var converter = new TypeConverter()
                .AddDigester<NullableDigester>()
                .AddConverter<NullableConverter>()
                .AddConverter<ToStringConverter>()
                .AddConverter<TimeSpanConverter>()
                .AddConverter<UriConverter>()
                .AddConverter<SystemConverter>();
```

**Note:** To write your own digesters or converters see source code for `ToStringConverter` and `NullableDigester`.
