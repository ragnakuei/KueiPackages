namespace KueiPackages.Models;

public class InsertUpdateDeleteDto<T>
{
    public List<T> Insert { get; set; }
    public List<T> Update { get; set; }
    public List<T> Delete { get; set; }
}