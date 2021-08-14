<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MockWebApp.Account.Register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">

    <div class="card br-0 border-0 ez-2 mt-5 w-75 mx-auto">
        <div class="card-header bg-transparent">
            <h5 class="my-2">Registration</h5>
        </div>
        <div class="card-body pb-0">
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBox_Email" CssClass="col-md-3 text-right">Email</asp:Label>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="TextBox_Email" CssClass="form-control" TextMode="Email" />
                </div>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBox_Password" CssClass="col-md-3 text-right">Password</asp:Label>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="TextBox_Password" TextMode="Password" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" AssociatedControlID="TextBox_ConfirmPassword" CssClass="col-md-3 text-right">Confirm Password</asp:Label>
                <div class="col-md-9">
                    <asp:TextBox runat="server" ID="TextBox_ConfirmPassword" TextMode="Password" CssClass="form-control" />
                </div>
            </div>
            <p class="text-danger mt-2 text-center">
                <asp:Literal runat="server" ID="ErrorMessage" />
            </p>
        </div>
        <div class="card-footer bg-transparent border-0">
            <div class="row">
                <div class="col">
                    <asp:HyperLink runat="server" ID="Link_Login" CssClass="btn btn-outline-primary" NavigateUrl="~/Account/Login.aspx" Text="Back to Login"></asp:HyperLink>
                </div>
                <div class="col d-flex justify-content-end">
                    <asp:Button runat="server" Text="Register" ID="Button_Register" CssClass="btn btn-primary" OnClick="Button_Register_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
