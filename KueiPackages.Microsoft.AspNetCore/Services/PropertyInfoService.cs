namespace KueiPackages.Microsoft.AspNetCore.Services;

public class PropertyInfoService
{
    public PropertyInfoService()
    {
    }

    public Dictionary<string, PropertyInfoDto> GetProperties(Type type)
    {
        return GetPropertiesWithAction(type, dto => true);
    }

    public Dictionary<string, PropertyInfoDto> GetDisplayNameProperties(Type type)
    {
        return GetPropertiesWithAction(type, dto => dto.DisplayAttribute != null);
    }

    private Dictionary<string, PropertyInfoDto> GetPropertiesWithAction(Type type, Func<PropertyInfoDto, bool> whereAction)
    {
        var result = type.GetProperties()
                         .Select(p =>
                                 {
                                     var dto = new PropertyInfoDto();
                                     dto.PropertyName     = p.Name;
                                     dto.DisplayAttribute = p.GetCustomAttributes(typeof(DisplayAttribute), false)?.FirstOrDefault() as DisplayAttribute;
                                     dto.ValidationRules  = GetValidationRules(p);
                                     dto.PropertyInfo     = p;
                                     dto.ElementType = new[]
                                                       {
                                                           p.PropertyType.GetElementType(),
                                                           p.PropertyType.GetGenericArguments().FirstOrDefault(gt => gt.IsValueType == false)
                                                       }.FirstOrDefault(t => t != null);

                                     dto.IsClassObject = dto.IsCollection                          == false
                                                      && dto.PropertyInfo.PropertyType.IsValueType == false
                                                      && dto.PropertyInfo.PropertyType             != typeof(string);

                                     if (dto.IsClassObject)
                                     {
                                         dto.ObjectType = dto.PropertyInfo.PropertyType;
                                     }

                                     return dto;
                                 })
                         .Where(whereAction)
                         .ToDictionary(k => k.PropertyName,
                                       v => v);

        return result;
    }

    private Dictionary<string, object> GetValidationRules(PropertyInfo propertyInfo)
    {
        var result = new Dictionary<string, object>();

        var customAttributes = propertyInfo.GetCustomAttributes().ToArray();

        AssignValidationRuleRequired(result, customAttributes);
        AssignValidationRuleStringLength(result, customAttributes);
        AssignValidationRuleNumberRange(result, customAttributes);
        AssignValidationRuleEmailAddress(result, customAttributes);

        return result;
    }

    private void AssignValidationRuleRequired(Dictionary<string, object> validationRules,
                                              Attribute[]                customAttributes)
    {
        var requiredAttribute = customAttributes.OfType<RequiredAttribute>().FirstOrDefault();
        if (requiredAttribute == null)
        {
            return;
        }

        validationRules.Add("required", true);
    }

    private void AssignValidationRuleStringLength(Dictionary<string, object> validationRules,
                                                  Attribute[]                customAttributes)
    {
        var requiredAttribute = customAttributes.OfType<StringLengthAttribute>().FirstOrDefault();
        if (requiredAttribute == null)
        {
            return;
        }

        if (requiredAttribute.MaximumLength > 0)
        {
            validationRules.Add("maxlength", requiredAttribute.MaximumLength);
        }

        if (requiredAttribute.MinimumLength > 0)
        {
            validationRules.Add("minlength", requiredAttribute.MinimumLength);
        }
    }

    private void AssignValidationRuleNumberRange(Dictionary<string, object> validationRules,
                                                 Attribute[]                customAttributes)
    {
        var rangeAttribute = customAttributes.OfType<RangeAttribute>().FirstOrDefault();
        if (rangeAttribute == null)
        {
            return;
        }

        validationRules.Add("min", rangeAttribute.Minimum);
        validationRules.Add("max", rangeAttribute.Maximum);
    }

    private void AssignValidationRuleEmailAddress(Dictionary<string, object> validationRules,
                                                  Attribute[]                customAttributes)
    {
        var rangeAttribute = customAttributes.OfType<EmailAddressAttribute>().FirstOrDefault();
        if (rangeAttribute == null)
        {
            return;
        }

        validationRules.Add("email", true);
    }
}
