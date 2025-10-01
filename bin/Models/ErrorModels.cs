// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ErrorModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class ErrorModel
{
  public ErrorModel()
  {
  }

  public ErrorModel(
    string errorTitle,
    string errorTranslateTag,
    string errorMessage,
    string errorSubject,
    string errorLocation)
  {
    this.ErrorTitle = errorTitle;
    this.ErrorTranslateTag = errorTranslateTag;
    this.ErrorSubject = errorSubject;
    this.ErrorLocation = errorLocation;
    this.ErrorMessage = errorMessage;
  }

  public ErrorModel(string errorMessage)
  {
    this.ErrorTitle = "Your Request Failed";
    this.ErrorTranslateTag = "ErrorDisplay|";
    this.ErrorSubject = "Error: " + errorMessage;
    this.ErrorMessage = errorMessage;
  }

  [Required]
  public string ErrorTitle { get; set; }

  [Required]
  public string ErrorTranslateTag { get; set; }

  public string ErrorSubject { get; set; }

  public string ErrorLocation { get; set; }

  [Required]
  public string ErrorMessage { get; set; }
}
