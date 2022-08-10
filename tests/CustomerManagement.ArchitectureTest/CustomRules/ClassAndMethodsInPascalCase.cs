namespace CustomerManagement.ArchitectureTest.CustomRules;

using Mono.Cecil;
using NetArchTest.Rules;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

[ExcludeFromCodeCoverage]
internal class ClassAndMethodsInPascalCase : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        var regex = @"(_)";

        if (char.IsLower(type.Name[0]) || Regex.Match(type.Name, regex, RegexOptions.IgnoreCase).Success)
        {
            return false;
        }

        foreach (var method in type.Methods)
        {
            if ((char.IsLower(method.Name[0]) || Regex.Match(method.Name, regex, RegexOptions.IgnoreCase).Success) &&
                !method.IsGetter &&
                !method.IsSetter &&
                !Regex.Match(method.Name, @"(<)", RegexOptions.IgnoreCase).Success
                )
            {
                return false;
            }
        }

        return true;
    }
}