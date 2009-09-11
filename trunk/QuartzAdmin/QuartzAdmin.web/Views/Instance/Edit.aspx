<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<QuartzAdmin.web.Models.InstanceModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Edit</h2>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       {%>
    <fieldset>
        <legend>Fields</legend>
        <p>
            <label for="InstanceName">
                <%=Model.InstanceName %></label>
        </p>
        <label>
            Quartz Properties</label>
        <table>
            <tr>
                <th>
                    Prop Name
                </th>
                <th>
                    Prop Value
                </th>
            </tr>
            <%
                int cnt = 1;
                foreach (QuartzAdmin.web.Models.InstancePropertyModel prop in Model.InstanceProperties)
                {%>
            <tr>
                <td>
                    <%=Html.TextBox(string.Format("InstancePropertyKey-{0}", cnt), prop.PropertyName)%>
                </td>
                <td>
                    <%=Html.TextBox(string.Format("InstancePropertyValue-{0}", cnt), prop.PropertyValue) %>
                </td>
            </tr>
            <%
                cnt++;
                  } %>
        </table>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
    <% } %>
    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
</asp:Content>
