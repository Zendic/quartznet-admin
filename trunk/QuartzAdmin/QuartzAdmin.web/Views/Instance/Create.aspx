<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<QuartzAdmin.web.Models.InstanceModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            <p>
                <label for="InstanceName">Name:</label>
                <%= Html.TextBox("InstanceName") %>
                <%= Html.ValidationMessage("InstanceName", "*") %>
            </p>
            <p>
                <label>Quartz Properties</label>
                <table>
                    <tr>
                        <th>Prop Name</th>
                        <th>Prop Value</th>
                    </tr>
                    <tr>
                        <td><%=Html.TextBox("InstancePropertyKey-1", "quartz.scheduler.instanceName")%></td>
                        <td><%=Html.TextBox("InstancePropertyValue-1", "SampleQuartzScheduler") %></td>
                    </tr>
                    <tr>
                        <td><%=Html.TextBox("InstancePropertyKey-2", "quartz.threadPool.type")%></td>
                        <td><%=Html.TextBox("InstancePropertyValue-2", "Quartz.Simpl.SimpleThreadPool, Quartz") %></td>
                    </tr>
                    <tr>
                        <td><%=Html.TextBox("InstancePropertyKey-3", "quartz.scheduler.proxy")%></td>
                        <td><%=Html.TextBox("InstancePropertyValue-3", "true") %></td>
                    </tr>
                    <tr>
                        <td><%=Html.TextBox("InstancePropertyKey-4", "quartz.scheduler.proxy.address")%></td>
                        <td><%=Html.TextBox("InstancePropertyValue-4", "tcp://localhost:555/QuartzScheduler") %></td>
                    </tr>
                </table>
            </p>
            <p>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

