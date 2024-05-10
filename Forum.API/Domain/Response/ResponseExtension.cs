namespace Forum.API.Domain.Response;

public class ResponseExtension
{
    public static class Query
    {
        public static ResultResponse QuerySuccess(object data) => new ResultResponse(ReturnMessage: "查詢成功", ReturnData: data);
    }
}
