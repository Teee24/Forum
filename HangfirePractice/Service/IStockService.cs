using HangfirePractice.Model;

namespace HangfirePractice.Service;

public interface IStockService
{
    public Task<bool> GetAndSaveETFInfo(string target);

    public Task<ETFInfo> GetETFInfo(string key);
}
