<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<div>
    <ul class="list-unstyled multi-steps rounded shadow-sm mt-4">
        <li class="<%:Request.Path.StartsWith("/Rule/CreateNew") ? "is-active" : " " %>"><a href="/Rule/CreateNew">What triggers your action?</a></li>
        <li class="<%:Request.Path.StartsWith("/Rule/Actions/") ? "is-active" : " " %>">Actions
        </li>
        <li class="<%:Request.Path.StartsWith("/Rule/Triggers/") ? "is-active" : " " %>">Action Name and Devices
        </li>
    </ul>
</div>
