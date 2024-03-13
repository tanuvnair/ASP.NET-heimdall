<%@ Page Title="Attendance History" Language="C#" AutoEventWireup="true" CodeBehind="AttendanceHistory.aspx.cs" Inherits="ASP.NET_heimdall.AttendanceHistory" MasterPageFile="/Site.Master" %>

<asp:Content ContentPlaceHolderID="UserBodyContent" runat="server">
    <div class="m-5">
        <h1>Attendance History</h1>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PC %>" SelectCommand="SELECT [AttendanceDate], [PunchInTime], [PunchOutTime], [Status], [AttendanceRecordID] FROM [AttendanceRecords] WHERE ([UserID] = @UserID)">
            <SelectParameters>
                <asp:SessionParameter Name="UserID" SessionField="UserID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

        <div class="table-responsive mt-4" style="height: calc(100vh - 300px)">
            <asp:GridView OnRowDataBound="AttendanceHistoryGridView_RowDataBound" CssClass="table" ID="AttendanceHistoryGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="AttendanceRecordID" DataSourceID="SqlDataSource1" AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="AttendanceDate" HeaderText="Attendance Date" SortExpression="AttendanceDate" />
                    <asp:BoundField DataField="PunchInTime" HeaderText="Punch In Time" SortExpression="PunchInTime" />
                    <asp:BoundField DataField="PunchOutTime" HeaderText="Punch Out Time" SortExpression="PunchOutTime" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
