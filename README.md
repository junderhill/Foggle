# Foggle 
[![Build status](https://ci.appveyor.com/api/projects/status/uuthgll2ji9q2h1n/branch/master?svg=true)](https://ci.appveyor.com/project/junderhill/foggle/branch/master)
[![Build status](https://ci.appveyor.com/api/projects/status/uuthgll2ji9q2h1n/branch/develop?svg=true)](https://ci.appveyor.com/project/junderhill/foggle/branch/develop)



Stupidly small and lightweight .NET Feature Toggle

## Using Foggle

### Adding to your project
Foggle can be added to a project quickly using NuGet. It's available in the NuGet gallery [here](https://www.nuget.org/packages/JasonUnderhill.Foggle/).

Run the following the package manager console:
```
Install-Package JasonUnderhill.Foggle 
```

### Creating a feature
Then create an empty class for the feature you wish to toggle, this must inherit from `FoggleFeature`. 
For example:

```
public class NewFeature : FoggleFeature
{
}
```

Within your configuration file (Web.Config or App.Config) you need to add a setting for your new feature. It should have the key as follows:
```
Foggle.<Name of your class>
```

For example, to add our `NewFeature` as shown above we would add `Foggle.NewFeature`
The value for our newly added setting to be enabled should be `true` or anything that when parsed to a Boolean in C# would be true. If no setting is found by foggle matching the class name then the feature will default to being disabled.

```
<appSettings>
    <add key="Foggle.NewFeature" value="true" />
</appSettings>
```

### Toggling your feature code
Wherever you want to disable some code you would wrap your code with an `if` statement as follows:

```
if (Feature.IsEnabled<NewFeature>())
{
    //Your feature code here                                
}
```

### Removing your toggle
When you come to remove your toggle, you would delete the class for the feature, in the case of our example `NewFeature`. This would cause any usages of the class to be flagged as compile errors allowing you to find them quickly and remove the `if` statements also.

You could, alternatively, use 'Find Usages' to find all the places where it is used and delete each in turn.
