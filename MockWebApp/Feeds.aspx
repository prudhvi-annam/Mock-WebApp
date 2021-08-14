<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Feeds.aspx.cs" Inherits="MockWebApp.Feeds" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_Main" runat="server">

    <div class="card br-0 border-0 ez-2 mt-2">
        <div class="card-header py-2 bg-warning d-flex justify-content-between align-items-center">
            <h5 class="mb-0">HackerNews</h5>            
        </div>
        <div class="card-body">            
           <div id="panelStories">
               <div class="d-flex justify-content-center">
                   <div class="loader"></div>
               </div>
           </div>          
        </div>
        <div class="card-footer bg-transparent border-0 d-none">
            <div class="d-flex justify-content-between">
                <div>
                    <button id="btn_Previous" class="btn btn-primary" type="button">Previous</button>
                </div>
                <div style="width: 350px">
                    <div class="input-group">
                        <input type="text" class="form-control" id="textbox_Keywords"  />
                        <div class="input-group-append">
                            <button class="btn btn-outline-secondary" type="button" id="btn_Search">Search</button>
                        </div>
                    </div>
                </div>
                <div>
                    <button id="btn_Next" class="btn btn-primary" type="button">Next</button>
                </div>
            </div>
        </div>
    </div>   
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_Scripts" runat="server">
    <script type="text/javascript">
        $(function () {
            var page = 0;
            renderStories(page);
            $(document).on('click', 'button#btn_Next', function (e) {
                e.preventDefault();
                renderStories(++page, $('input#textbox_Keywords').val().trim());
            }).on('click', 'button#btn_Previous', function (e) {
                e.preventDefault();
                renderStories(--page, $('input#textbox_Keywords').val().trim());
            }).on('click', 'button#btn_Search', function (e) {
                e.preventDefault();                
                page = 0;
                renderStories(page, $('input#textbox_Keywords').val().trim());
                $(this).blur();
            });
        });

        function renderStories(page, keywords) {
            
            $.ajax('<%=ResolveUrl("~/api/webapi/TopStories")%>', {
                data: { 'page': page, 'keywords': keywords || null },
                dataType: 'json',
                success: function (data, status, xhr) {
                    $('.card-footer').removeClass('d-none');
                    $('button#btn_Previous').prop('disabled', page <= 0);
                    $('button#btn_Next').prop('disabled', (data || []).length <= 0);
                    if ((data || []).length > 0) {
                        $('#panelStories').empty().append($.map(data, (v, i) => rowRow(v, (page * 10) + i + 1)))                                                
                    } else if ((keywords || '') != '' && page <= 0) {
                        $('#panelStories').empty().append($('<div class="d-flex d-flex justify-content-center">')
                            .append($('<div class="alert alert-danger">No results found matching the Keywords</div>')));
                        $('button#btn_Previous').prop('disabled', true);
                    }                    
                },
                error: function (jqXhr, textStatus, errorMessage) {
                    alert('Error: ' + errorMessage);
                }
            });

            var rowRow = function (model, index) {
                return $('<div class="row my-1 pb-1">').append([
                    $('<div class="text-right" style="width:40px;">').append([$('<span class="text-lightgrey mx-1" style="font-size:14px;">').text(index + '.')]),
                    $('<div class="col pl-1">').append([
                        $('<div>').append([
                            $('<a class="text-dark">').attr('href', '<%=ResolveUrl("~/Secured/FeedInfo.aspx")%>' + '?id=' + model.id).text(model.title),
                            $('<a class="text-lightgrey small ml-2" target="_blank">').attr('href', model.url).text('(' + model.domain + ')'),
                        ]),
                        $('<div class="small">').append([
                            $('<span class="text-lightgrey">').text(model.score + ' points by ' + model.by + ' ' + model.duration + ' ago'),
                            $('<span class="text-lightgrey mx-1">').text('|'),
                            $('<span class="text-lightgrey">').text((model.kids || []).length + ' comments')
                        ])
                    ])
                ]);
            }
        }


    </script>
</asp:Content>