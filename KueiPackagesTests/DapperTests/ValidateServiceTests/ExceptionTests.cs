﻿using System.ComponentModel;
using PropertyInfoService = KueiPackages.Dapper.Generator.PropertyInfoService;

namespace KueiPackagesTests.DapperTests;

public class ExceptionTests
{
    [Test]
    public void Array_為空()
    {
        var dto = new StudentDto
                  {
                      Id      = 1,
                      Name    = "A",
                      Courses = null,
                  };

        var actualException = Assert.Throws<ApiResponseException<string[]>>(() => new ValidateService(new PropertyInfoService())
                                                                                 .Validate(dto)
                                                                                 .Int(v => v.Id, required: true)
                                                                                 .String(v => v.Name, required: true, maxLength: 10)
                                                                                 .Array(v => v.Courses, required: true)
                                                                                 .ThrowExceptionWhenInvalid());
        Assert.AreEqual(false, actualException.IsFormValid);

        Assert.AreEqual("表單驗証失敗", actualException.AlertMessage);

        Assert.AreEqual(new[]
                        {
                            "123"
                        },
                        actualException.Data);
    }

    // [Test]
    // public void Array_為空_requiredMessage()
    // {
    //     var dto = new StudentDto
    //               {
    //                   Id      = 1,
    //                   Name    = "A",
    //                   Courses = null,
    //               };
    //
    //     var actualException = Assert.Throws<ApiResponseException>(() => new ValidateService(new PropertyInfoService())
    //                                                                    .Validate(dto)
    //                                                                    .Int(v => v.Id, required: true)
    //                                                                    .String(v => v.Name, required: true, maxLength: 10)
    //                                                                    .Array(v => v.Courses, required: true, requiredMessage: "課程s不能為空")
    //                                                                    .ThrowExceptionWhenInvalid());
    //     var expected = "表單驗証失敗：" + Environment.NewLine + "課程s不能為空";
    //
    //     Assert.AreEqual(expected, actualException.ExceptionDTO.Message);
    // }
    //
    // [Test]
    // public void Array_Item_為空()
    // {
    //     var dto = new StudentDto
    //               {
    //                   Id   = 1,
    //                   Name = "A",
    //                   Courses = new[]
    //                             {
    //                                 (CourseDto)null,
    //                             },
    //               };
    //
    //     var actualException = Assert.Throws<ApiResponseException>(() => new ValidateService(new PropertyInfoService())
    //                                                                    .Validate(dto)
    //                                                                    .Int(v => v.Id, required: true)
    //                                                                    .String(v => v.Name, required: true, maxLength: 10)
    //                                                                    .Array(v => v.Courses, required: true, itemErrorMessagePrefix: "陣列")
    //                                                                    .ArrayItem(validator => validator.Int(v => v.Id, required: true)
    //                                                                                                     .String(v => v.Name, required: true, maxLength: 10))
    //                                                                    .ThrowExceptionWhenInvalid());
    //     var expected = "表單驗証失敗：" + Environment.NewLine + "陣列 第 1 筆資料 不能為空";
    //
    //     Assert.AreEqual(expected, actualException.ExceptionDTO.Message);
    // }
}
