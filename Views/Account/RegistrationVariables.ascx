<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.CreateAccountModel>"%>


                                        <!-- overridden by Account Themes -->
                                        <%
                                            string ResellerID = ConfigData.AppSettings("AdminAccountID");
                                            if (MonnitSession.CurrentTheme != null)
                                                ResellerID = MonnitSession.CurrentTheme.AccountID.ToString();
                                                
                                            // if Reseller Assigned alway keep the exisitng ResellerID
                                            if (Model != null && !string.IsNullOrEmpty(Model.ResellerID))
                                                ResellerID = Model.ResellerID;
                                        %>
                                        <input type='hidden' value='<%:ResellerID%>' name='ResellerID' />
                                         