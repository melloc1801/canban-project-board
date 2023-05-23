using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CB.Application.Validations.Attributes;

public class IsEmailAddressAttribute: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        var match = regex.Match((string) value);

        return match.Success;
    }
}