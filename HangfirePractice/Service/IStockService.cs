using HangfirePractice.Model;

namespace HangfirePractice.Service;

public interface IStockService
{
    public Task<bool> SaveETFInfo(List<ETFInfo> targets);

    public Task<ETFInfo> GetETFInfoCurrent(string key);

    public Task<IEnumerable<ETFInfo>> GetETFInfoPast(string key, string date);
}
