namespace KueiPackages.Dapper.Generator
{
    public enum SqlScriptType
    {
        None   = 0,
        Select = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
        MergeWithDeleteFlag  = 5,
        MergeWithDeleteKey  = 6,
    }
}
