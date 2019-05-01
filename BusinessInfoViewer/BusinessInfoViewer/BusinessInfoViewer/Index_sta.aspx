<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index_sta.aspx.cs" Inherits="Index_sta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="zh-cn">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>商机抓取系统</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="js/html5shiv.js"></script>
    <script type="text/javascript" src="js/respond.min.js"></script>
    <link href="css/index.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link href="js/dataTables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="js/dataTables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#businessTable").DataTable({
                "language": {
                    "lengthMenu": "每页 _MENU_ 条记录",
                    "zeroRecords": "没有找到记录",
                    "info": "第 _PAGE_ 页 ( 总共 _PAGES_ 页 )",
                    "infoEmpty": "无记录",
                    "infoFiltered": "(从 _MAX_ 条记录过滤)",
                    "search": "查询"
                },
                "ajax": "BusinessData/GetBusinessData.ashx",
                "display": "row-border",
                "pageLength": "20",
                "columns": [
                    { "data": "title", "width": "55%" },
                    { "data": "degree", "width": "5%" },
                    { "data": "com", "width": "15%" },
                    { "data": "date", "width": "5%" },
                    { "data": "source", "width": "10%" },
                    { "data": "url", "width": "10%" }
                ],
                "order": [1, 'desc'],
                "columnDefs": [{
                    "targets": 5,
                    "data": "url",
                    "render": function (data, type, row, meta) {
                        return '<a href="' + data + '">查看招标详情</a>';
                    }
                }]
            });
        });
    </script>
</head>
<body>
    <!-- todo 响应式导航条-->
    <nav id="nav" class="navbar navbar-default" role="navigation">
      <!-- todo 响应式收起时-->
      <div class="navbar-header">
        <!--todo logo名称-->
        <a href="" class="navbar-brand">
            <img src=""  style="margin-top: -10px"/>
        </a>
        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#collapsed_humbeger">
          <span class="sr-only">Toggle navigation</span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
        </button>
      </div>
      <!-- todo 响应式展开时-->
      <div class="collapse navbar-collapse" id="collapsed_humbeger">
        <!-- todo 响应式套航条按钮-->
        <ul class="nav navbar-nav" id="nav_col">
          <li class="active"><a href="Index_sta.aspx">商机抓取数据</a></li>
        </ul>
        <!-- todo 搜索表单-->
        <form class="navbar-form navbar-right" runat="server" role="search">
          <div class="form-group has-feedback">
            <input type="text" class="form-control" placeholder="Search">
            <span class="glyphicon glyphicon-search form-control-feedback"></span>
          </div>
          <button type="submit" class="btn btn-default">提交查询</button>
          <input type="button" runat="server" class="btn btn-default" OnServerClick="Export" name="导出Excel" title="导出Excel"/>
        </form>
      </div>
    </nav>
    <!--todo 主体-->
    <div class="container-fluid">
        <!-- todo 主体内容部分-->
        <div class="row">
            <!-- todo主体左侧面板无用已删除 代码参照其他二级页面 -->
            <div class="col-xs-12">
                <div class="panel" id="panel">
                    <!-- todo 面板主体-->
                    <div class="panel-body row" id="panel_body_row">
                        <!-- todo 导航内容 -->
                        <div class="col-xs-12" style="height: 630px;">
                            <div class="tab-content" id="main_content">
                                <div role="tabpanel" class="tab-pane active" id="sta_data">
                                    <table id="businessTable" class="row-border" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    招标名称
                                                </th>
                                                <th>
                                                    相关度
                                                </th>
                                                <th>
                                                    发布单位
                                                </th>
                                                <th>
                                                    发布时间
                                                </th>
                                                <th>
                                                    信息来源
                                                </th>
                                                <th>
                                                    详细地址
                                                </th>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th>
                                                    招标名称
                                                </th>
                                                <th>
                                                    相关度
                                                </th>
                                                <th>
                                                    发布单位
                                                </th>
                                                <th>
                                                    发布时间
                                                </th>
                                                <th>
                                                    信息来源
                                                </th>
                                                <th>
                                                    详细地址
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="footer">
                    <span>联系我们</span>
                </div>
            </div>
        </div>
    </div>
    <script src="js/index.js" type="text/javascript"></script>
</body>
</html>
