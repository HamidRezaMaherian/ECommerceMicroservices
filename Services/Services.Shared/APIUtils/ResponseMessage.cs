namespace Services.Shared.APIUtils
{
  public static class ResponseMessage
  {
    public const string NOT_FOUND = "The {0} Not Found";
    public const string SUCCEDED = "{0} Successfully";
    public const string FAILED = "{0} Failed";
    public const string UserCredsNotValid = "UserName Or Password Is Wrong";
    public const string UserHasBeenBlocked = "User Has Been Blocked";
    public const string EmailNeedConfirmation = "Email Need To Be Confirmed";
    public const string RequestNotValid = "Request Not Valid";
  }
  public static class ResponseMessageRegex
  {
    public const string NOT_FOUND = "The .* Not Found";
    public const string SUCCEDED = ".* Successfully";
    public const string FAILED = ".* Failed";
  }
}
