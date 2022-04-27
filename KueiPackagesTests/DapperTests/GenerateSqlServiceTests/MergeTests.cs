namespace KueiPackagesTests.DapperTests.GenerateSqlServiceTests
{
    public class MergeTests
    {
        [Test]
        public void MergeWithDeleteFlag()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassC>(SqlScriptType.MergeWithDeleteFlag);

            var expected = @"MERGE [dbo].[ClassC] [t]
USING @ClassC [s]
ON [t].[Guid] = [s].[Guid]
    AND [t].[DataStatusId] = @ActiveDataStatusId
WHEN MATCHED
    THEN
    UPDATE
    SET [t].[ParentGuid] = [s].[ParentGuid],
        [t].[Name] = [s].[Name],
        [t].[Age] = [s].[Age],
        [t].[DataStatusId] = [s].[DataStatusId]
WHEN NOT MATCHED BY TARGET
    THEN
    INSERT ([Guid],
            [ParentGuid],
            [Name],
            [Age],
            [DataStatusId])
    VALUES ([s].[Guid],
            [s].[ParentGuid],
            [s].[Name],
            [s].[Age],
            [s].[DataStatusId])
WHEN NOT MATCHED BY SOURCE 
    AND [t].[ParentGuid] = @DeleteParentGuid
    THEN
    UPDATE
    SET [t].[DataStatusId] = @DataStatusId;";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MergeWithDeleteKey()
        {
            var target = new GenerateSqlService();
            var actual = target.Generate<ClassC>(SqlScriptType.MergeWithDeleteKey);

            var expected = @"MERGE [dbo].[ClassC] [t]
USING @ClassC [s]
ON [t].[Guid] = [s].[Guid]
WHEN MATCHED
    THEN
    UPDATE
    SET [t].[ParentGuid] = [s].[ParentGuid],
        [t].[Name] = [s].[Name],
        [t].[Age] = [s].[Age],
        [t].[DataStatusId] = [s].[DataStatusId]
WHEN NOT MATCHED BY TARGET
    THEN
    INSERT ([Guid],
            [ParentGuid],
            [Name],
            [Age],
            [DataStatusId])
    VALUES ([s].[Guid],
            [s].[ParentGuid],
            [s].[Name],
            [s].[Age],
            [s].[DataStatusId])
WHEN NOT MATCHED BY SOURCE 
    AND [t].[ParentGuid] = @DeleteParentGuid
    THEN
    DELETE;";

            Assert.AreEqual(expected, actual);
        }
    }
}
