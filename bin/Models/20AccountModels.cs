// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AccountValidation
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Security;

#nullable disable
namespace iMonnit.Models;

public static class AccountValidation
{
  public static string ErrorCodeToString(MembershipCreateStatus createStatus)
  {
    switch (createStatus)
    {
      case MembershipCreateStatus.InvalidUserName:
        return "The user name provided is invalid. Please check the value and try again.";
      case MembershipCreateStatus.InvalidPassword:
        return "The password provided is invalid. Please enter a valid password value.";
      case MembershipCreateStatus.InvalidQuestion:
        return "The password retrieval question provided is invalid. Please check the value and try again.";
      case MembershipCreateStatus.InvalidAnswer:
        return "The password retrieval answer provided is invalid. Please check the value and try again.";
      case MembershipCreateStatus.InvalidEmail:
        return "The e-mail address provided is invalid. Please check the value and try again.";
      case MembershipCreateStatus.DuplicateUserName:
        return "Username already exists. Please enter a different user name.";
      case MembershipCreateStatus.DuplicateEmail:
        return "A username for that e-mail address already exists. Please enter a different e-mail address.";
      case MembershipCreateStatus.UserRejected:
        return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      case MembershipCreateStatus.ProviderError:
        return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      default:
        return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
    }
  }
}
