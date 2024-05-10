namespace Forum.API.Domain.Response;

public record ResultResponse(int ReturnCode = 2000, string ReturnMessage = "", object ReturnData = null);
