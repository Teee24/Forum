namespace Forum.API.Domain.Response
{
   // public record ResultResponse(ReturnCodeEnum ReturnCode = ReturnCodeEnum.Success, string ReturnMessage = "", object ReturnData = null);
    public record ResultResponse(string ReturnMessage = "", object ReturnData = null);
}
