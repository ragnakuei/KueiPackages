using System.Collections.Concurrent;

namespace KueiPackages.Microsoft.AspNetCore.Services;

/// <summary>
/// Need Singleton LifeTime
/// </summary>
public class QueueService<T> where T : class
{
    public QueueService(ILogger<QueueService<T>> logger)
    {
        Logger = logger;
    }

    private ILogger<QueueService<T>> Logger { get; }

    /// <summary>
    /// 進行 Queue 的處理
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="getGroupKey">
    /// 分流條件
    /// <remarks>會做為 HandlingGroupDtos 的 Key</remarks>
    /// </param>
    /// <param name="action"></param>
    /// <param name="retryCount"></param>
    public async Task QueueActionAsync(T               dto,
                                       Func<T, string> getGroupKey,
                                       Action<T>       action,
                                       int             retryCount        = 30,
                                       int             delayMilliseconds = 10)
    {
        try
        {
            await WaitIfSatisfiedGroupCondition(dto, getGroupKey, retryCount, delayMilliseconds);

            Logger?.LogDebug("Invoke T");

            action.Invoke(dto);
        }
        finally
        {
            Remove(dto, getGroupKey);
        }
    }

    /// <summary>
    /// 進行 分流 & Queue 的處理
    /// </summary>
    private async Task WaitIfSatisfiedGroupCondition(T               dto,
                                                     Func<T, string> getKey,
                                                     int             retryMaxCount,
                                                     int             delayMilliseconds = 1)
    {
        var groupKey = getKey.Invoke(dto);
        Logger.LogDebug($"queue groupKey:{groupKey}");

        var queue = HandlingGroupDtos.GetOrAdd(groupKey, new ConcurrentQueue<T>());
        queue.Enqueue(dto);
        Logger.LogDebug($"queue count:{queue.Count}");

        var retryCount = 0;

        while (retryCount++ < retryMaxCount)
        {
            queue.TryPeek(out var peek);
            if (dto == peek)
            {
                return;
            }

            await Task.Delay(delayMilliseconds);
        }

        throw new Exception($"到達寫入重試上限{retryMaxCount}次，請聯絡系統開發人員 !");
    }

    /// <summary>
    /// 正在處理清單
    /// <remarks>分流後的物件，會放在這個清單中，清單中的物件等於是正在處理的</remarks>
    /// </summary>
    private readonly ConcurrentDictionary<string, ConcurrentQueue<T>> HandlingGroupDtos = new ConcurrentDictionary<string, ConcurrentQueue<T>>();

    /// <summary>
    /// 將 Dto 從正在處理清單中移除
    /// </summary>
    private void Remove(T dto, Func<T, string> getGroupKey)
    {
        var groupKey = getGroupKey.Invoke(dto);

        var queue = HandlingGroupDtos.GetOrAdd(groupKey, x => new ConcurrentQueue<T>());
        if (queue.TryDequeue(out _) == false)
        {
            Logger?.LogError("Queue remove T failed");
        }
        else
        {
            Logger?.LogDebug("Queue remove T successfully");
        }
    }
}
