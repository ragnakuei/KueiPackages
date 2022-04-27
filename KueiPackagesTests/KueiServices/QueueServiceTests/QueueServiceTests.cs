namespace KueiPackagesTests.KueiServices.QueueServiceTests;

public class QueueServiceTests
{
    [Test]
    public async Task Case01()
    {
        var dtos = new List<TestDto>
                   {
                       new() { Id = 0, Name = "A", Count = 0 },
                       new() { Id = 0, Name = "A", Count = 1 },
                       new() { Id = 0, Name = "A", Count = 2 },
                       new() { Id = 1, Name = "B", Count = 3 },
                       new() { Id = 1, Name = "B", Count = 4 },
                       new() { Id = 2, Name = "C", Count = 5 },
                       new() { Id = 2, Name = "C", Count = 6 },
                       new() { Id = 2, Name = "C", Count = 7 },
                       new() { Id = 2, Name = "C", Count = 8 },
                       new() { Id = 2, Name = "C", Count = 9 },
                   };

        using var loggerFactory = LoggerFactory.Create(builder =>
                                                       {
                                                           builder.AddConsole();
                                                           builder.AddFilter("*", LogLevel.Trace);
                                                       });
        var logger = loggerFactory.CreateLogger<QueueService<TestDto>>();

        var target = new QueueService<TestDto>(logger);

        var actual = new ConcurrentBag<TestDto>();

        var tasks = new Task[dtos.Count];

        Parallel.For(0,
                     dtos.Count,
                     i =>
                     {
                         tasks[i] = target.QueueActionAsync(dtos[i],
                                                            dto => dto.Id.ToString() + dto.Name,
                                                            (dto) =>
                                                            {
                                                                Thread.Sleep(1);
                                                                actual.Add(dto);
                                                            },
                                                            30,
                                                            100);
                     });

        await Task.WhenAll(tasks);

        var expected = new[]
                       {
                           new { Id = 0, Name = "A", Count = 0 },
                           new { Id = 0, Name = "A", Count = 1 },
                           new { Id = 0, Name = "A", Count = 2 },
                           new { Id = 1, Name = "B", Count = 3 },
                           new { Id = 1, Name = "B", Count = 4 },
                           new { Id = 2, Name = "C", Count = 5 },
                           new { Id = 2, Name = "C", Count = 6 },
                           new { Id = 2, Name = "C", Count = 7 },
                           new { Id = 2, Name = "C", Count = 8 },
                           new { Id = 2, Name = "C", Count = 9 },
                       };

        actual.Should().BeEquivalentTo(expected);
    }
}
