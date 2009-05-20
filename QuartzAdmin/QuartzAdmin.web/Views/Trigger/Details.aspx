<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<QuartzAdmin.web.Models.TriggerFireTimesModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Name:
            <%= Html.Encode(Model.Trigger.Name) %>
        </p>
        <p>
            Group:
            <%= Html.Encode(Model.Trigger.Group) %>
        </p>
        <p>
            JobName:
            <%= Html.Encode(Model.Trigger.JobName) %>
        </p>
        <p>
            JobGroup:
            <%= Html.Encode(Model.Trigger.JobGroup) %>
        </p>
        <p>
            FullName:
            <%= Html.Encode(Model.Trigger.FullName) %>
        </p>
        <p>
            FullJobName:
            <%= Html.Encode(Model.Trigger.FullJobName) %>
        </p>
        <p>
            Description:
            <%= Html.Encode(Model.Trigger.Description) %>
        </p>
        <p>
            CalendarName:
            <%=Model.Trigger.CalendarName!=null && Model.Trigger.CalendarName.Length > 0 ? Html.ActionLink(Model.Trigger.CalendarName, "Details", "Calendar", new { id = Html.Encode(Model.Trigger.CalendarName) }, null) : "None"%>
        </p>
        <p>
            Volatile:
            <%= Html.Encode(Model.Trigger.Volatile) %>
        </p>
        <p>
            FinalFireTimeUtc:
            <%= Html.Encode(String.Format("{0:g}", Model.Trigger.FinalFireTimeUtc)) %>
        </p>
        <p>
            MisfireInstruction:
            <%= Html.Encode(Model.Trigger.MisfireInstruction) %>
        </p>
        <p>
            FireInstanceId:
            <%= Html.Encode(Model.Trigger.FireInstanceId) %>
        </p>
        <p>
            EndTimeUtc:
            <%= Html.Encode(String.Format("{0:g}", Model.Trigger.EndTimeUtc)) %>
        </p>
        <p>
            StartTimeUtc:
            <%= Html.Encode(String.Format("{0:g}", Model.Trigger.StartTimeUtc)) %>
        </p>
        <p>
            HasMillisecondPrecision:
            <%= Html.Encode(Model.Trigger.HasMillisecondPrecision) %>
        </p>
        <p>
            Priority:
            <%= Html.Encode(Model.Trigger.Priority) %>
        </p>
        <p>
            HasAdditionalProperties:
            <%= Html.Encode(Model.Trigger.HasAdditionalProperties) %>
        </p>
    </fieldset>
    <p>
        <%=Html.ActionLink("Back to List", "Details", "Group", new { id = ViewData["groupName"] }, null)%>
        <%=Html.ActionLink("Fire Times", "FireTimes", new { groupName = Model.Trigger.Group, itemName = Model.Trigger.Name })%>
    </p>
    
<div id="panelFireTimes">
	<div class="hd">Fire Times for triggger</div>
	<div class="bd">
	    <%Html.RenderPartial("FireTimesPartial", Model); %>
	</div>
	<div class="ft"></div>
</div>    

</asp:Content>

