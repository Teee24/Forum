namespace Forum.API.Domain.Response
{
    public record ResultResponse(int ReturnCode = 2000, string ReturnMessage = "", object ReturnData = null);
    //public record ResultResponse {
    //    public ResultResponse(int ReturnCode, string ReturnMessage,object ReturnData){
    //    this.ReturnCode = ReturnCode;
    //    this.ReturnMessage = ReturnMessage;
    //    this.ReturnData = ReturnData;
      
    //}
    //}
}
