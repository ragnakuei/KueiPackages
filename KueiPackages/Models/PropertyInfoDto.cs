namespace KueiPackages.Models;

public class PropertyInfoDto
{
    public string PropertyName { get; set; }

    public DisplayAttribute DisplayAttribute { get; set; }

    public string DisplayAttributeName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(DisplayAttribute?.Name))
            {
                return PropertyName;
            }

            return DisplayAttribute.Name;
        }
    }

    public PropertyInfo PropertyInfo { get; set; }

    public bool IsCollection => ElementType != null;

    /// <summary>
    /// 用於 IsCollection = true 時，取出 IEnumerable of T 的 typeof(T)
    /// </summary>
    public Type ElementType { get; set; }

    public bool IsClassObject { get; set; }

    /// <summary>
    /// 用於 IsObject = true 時，取出 Object 的 Type
    /// </summary>
    public Type ObjectType { get; set; }

    /// <summary>
    /// 取出驗証用的 Attributes
    /// </summary>
    public Dictionary<string, object> ValidationRules { get; set; }
}
