<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FeedInfo.aspx.cs" Inherits="MockWebApp.Secured.FeedInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">
    <div class="card br-0 border-0 ez-2 mt-2">
        <div class="card-header py-2 bg-warning d-flex justify-content-between align-items-center">
            <h5 class="mb-0">HackerNews Story Info</h5>            
        </div>
        <div class="card-body">

            <div class="form-group row">
                <label for="staticEmail" class="col-sm-2 col-form-label text-right">ID</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="TextBox_ID" CssClass="form-control" disabled ></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label text-right">Title</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="TextBox_Title" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label text-right">By</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="TextBox_By" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>
             <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label text-right">Url</label>
                <div class="col-sm-10">
                    <asp:HyperLink runat="server" ID="Link_URL"></asp:HyperLink>
                </div>
            </div>
             <div class="form-group row">
                <label for="inputPassword" class="col-sm-2 col-form-label text-right">Score</label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="TextBox_Score" CssClass="form-control" disabled></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="card-footer d-flex justify-content-center">
            <a class="btn btn-primary" href="../Feeds.aspx">Back to Feeds</a>
        </div>
    </div>   

</asp:Content>

