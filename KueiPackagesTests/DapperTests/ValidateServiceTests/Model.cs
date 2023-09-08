using System.ComponentModel;

namespace KueiPackagesTests.DapperTests;

public class StudentDto
{
    [DisplayName("學生編號")]
    public int? Id { get; set; }

    [DisplayName("學生姓名")]
    public string Name { get; set; }

    [DisplayName("學生地址")]
    public AddressDto Address { get; set; }

    [DisplayName("課程")]
    public CourseDto[] Courses { get; set; }
}

public class AddressDto
{
    [DisplayName("第一部份")]
    public string Part1 { get; set; }

    [DisplayName("第二部份")]
    public string Part2 { get; set; }

    [DisplayName("第三部份")]
    public string Part3 { get; set; }
}

public class CourseDto
{
    [DisplayName("編號")]
    public int? Id { get; set; }

    [DisplayName("名稱")]
    public string Name { get; set; }
}
