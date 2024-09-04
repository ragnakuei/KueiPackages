namespace KueiPackagesTests.KueiServices;

/// <summary>
/// BaseValidateService 資料驗証邏輯測試
/// </summary>
public class BaseValidateService_Tests
{
    [Test]
    public void Validate_正常執行()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();

        // Act
        target.Validate(dto);

        // Assert
        Assert.Pass();
    }

    #region ValidateCommon

    [Test]
    public void ValidateIsNotNull_參數為null_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        Dto dto    = null;

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage("參數錯誤");
    }

    [Test]
    public void ValidateIsNotEqual_Id為0_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Id = 0;

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage("Id 不可為 0");
    }

    [Test]
    public void ValidateIsEqual_type不為Y_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Type = "N";

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage("Type 錯誤");
    }

    #endregion

    #region AddError

    [Test]
    public void AddErrorForIsNotEmpty_統一驗證_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();

        dto.Name    = null;
        dto.Age     = null;
        dto.Courses = null;

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "Name 不可為空",
                                                                      "Age 不可為空", "Courses 不可為空"
                                                                  });
    }

    [Test]
    public void AddErrorForMax_統一驗證_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Name           = "0".PadRight(11, '0');
        dto.Age            = 101;
        dto.BirthDayString = "2022-00-00";

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "Name 長度不可超過 10",
                                                                      "Age 不可超過 100", "BirthDayString 日期格式錯誤"
                                                                  });
    }

    [Test]
    public void AddErrorForMin_驗證_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Age = -1;

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗", new List<string> { "Age 不可小於 0" });
    }

    [Test]
    public void AddErrorForEqual_驗證_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Sex = "x";

        // Act
        Action act = () => target.Validate(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗", new List<string> { "Sex 錯誤" });
    }

    #endregion

    #region For

    [Test]
    public void ValidateFor_正常執行()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();

        // Act
        target.ValidateFor(dto);

        // Assert
        Assert.Pass();
    }

    [Test]
    public void ValidateFor_各property為null_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Name           = null;
        dto.Sex            = null;
        dto.Age            = null;
        dto.BirthDayString = null;
        dto.Courses        = null;

        // Act
        Action act = () => target.ValidateFor(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "請填寫 Name",
                                                                      "請填寫 Sex",
                                                                      "請填寫 Age",
                                                                      "請填寫 BirthDayString",
                                                                      "請填寫 Courses",
                                                                  });
    }

    [Test]
    public void ValidateFor_各property超出值範圍_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Name           = "0".PadRight(11, '0');
        dto.Sex            = "x";
        dto.Age            = -1;
        dto.BirthDayString = "2022-00-00";
        dto.Courses        = Array.Empty<Course>();

        // Act
        Action act = () => target.ValidateFor(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "Name 長度不可超過 10 個字元",
                                                                      "請填寫正確的 Sex",
                                                                      "Age 不可小於 0",
                                                                      "BirthDayString 日期格式錯誤",
                                                                      "請填寫 Courses",
                                                                  });
    }

    [Test]
    public void ValidateFor_Array各property為null_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Courses = new[]
                      {
                          new Course { Name = null, Score = null },
                          new Course { Name = null, Score = null },
                      };

        // Act
        Action act = () => target.ValidateFor(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "Courses",
                                                                      "第 1 筆",
                                                                      "請填寫 Name",
                                                                      "請填寫 Score",
                                                                      "第 2 筆",
                                                                      "請填寫 Name",
                                                                      "請填寫 Score",
                                                                  });
    }

    [Test]
    public void ValidateFor_Array各property超出值範圍_拋出提醒例外()
    {
        // Arrange
        var target = GetTarget();
        var dto    = GetDto();
        dto.Courses = new[]
                      {
                          new Course { Name = "0".PadRight(11, '0'), Score = 50 },
                          new Course { Name = "0000", Score                = -1 },
                      };

        // Act
        Action act = () => target.ValidateFor(dto);

        // Assert
        act.ActThrowApiResponseExceptionWithMessage<List<string>>("表單驗証失敗",
                                                                  new List<string>
                                                                  {
                                                                      "Courses",
                                                                      "第 1 筆",
                                                                      "Name 長度不可超過 10 個字元",
                                                                      "第 2 筆",
                                                                      "Score 不可小於 0"
                                                                  });
    }

    #endregion

    private TestValidateService GetTarget()
    {
        return new();
    }

    private static Dto GetDto()
    {
        return new Dto
               {
                   Id             = 1,
                   Name           = "A",
                   Sex            = "M",
                   Type           = "Y",
                   Age            = 18,
                   BirthDayString = "2022-01-01",
                   Courses = new[]
                             {
                                 new Course { Name = "C1", Score = 100 },
                             },
               };
    }
}

public class TestValidateService : BaseValidateService
{
    public void Validate(Dto dto)
    {
        ValidateCommon(dto);

        AddErrorForRequired(dto.Name, "Name 不可為空");
        AddErrorForMax(dto.Name, 10, "Name 長度不可超過 10");

        AddErrorForRequired(dto.Sex, "Sex 不可為空");
        AddErrorForIn(dto.Sex, new[] { "M", "F" }, "Sex 錯誤");

        AddErrorForRequired(dto.Age, "Age 不可為空");
        AddErrorForMin(dto.Age, 0, "Age 不可小於 0");
        AddErrorForMax(dto.Age, 100, "Age 不可超過 100");

        AddErrorForDateFormat(dto.BirthDayString, "yyyy-MM-dd", "BirthDayString 日期格式錯誤");

        AddErrorForRequired(dto.Courses, "Courses 不可為空");

        Validate();
    }

    private void ValidateCommon(Dto dto)
    {
        ValidateRequired(dto,    "參數錯誤");
        ValidateRequired(dto.Id, "Id 不可為空");
        ValidateEqual(dto.Id, 0, "Id 不可為 0");
        ValidateNotEqual(dto.Type, "Y", "Type 錯誤");
    }

    public void ValidateFor(Dto dto)
    {
        ValidateCommon(dto);

        For(dto.Name,           "Name").Required().Max(10);
        For(dto.Sex,            "Sex").Required().In(new[] { "M", "F" });
        For(dto.Age,            "Age").Required().Min(0).Max(100);
        For(dto.BirthDayString, "BirthDayString").Required().DateFormat("yyyy-MM-dd");
        For(dto.Courses, "Courses")
           .Required()
           .Item((v, item, index) =>
                 {
                     v.For(item.Name,  "Name",  index).Required().Max(10);
                     v.For(item.Score, "Score", index).Required().Min(0).Max(100);
                 })
           .End();

        Validate();
    }
}

public class Dto
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Sex { get; set; }

    public string? BirthDayString { get; set; }

    public int? Age { get; set; }

    public Course[] Courses { get; set; }
}

public class Course
{
    public long? Id { get; set; }

    public string? Name { get; set; }

    public int? Score { get; set; }
}