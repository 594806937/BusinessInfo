<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nav.aspx.cs" Inherits="nav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="zh-cn">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>中地数码前端框架</title>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <script src="http://cdn.bootcss.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="http://cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <!--todo 三维球的 CSS 和 JS-->
    <!--<link rel="stylesheet" type="text/css" href="css/MapGISEarth.css">-->
    <!--<link rel="stylesheet" type="text/css" href="Mapgis/MapgisPlugin/Widgets/widgets.css">-->
    <!--<script src="Mapgis/MapgisPlugin/MapgisPlugin.min.js"></script>-->
    <!--<script src="Mapgis/MapGISEarth.min.js"></script>-->
    <style>
        body
        {
            background: #285e8e;
        }
        #fun_nav > .list-group-item > a
        {
            text-decoration: none;
        }
        .nav_background
        {
            background: #f7e1b5;
        }
        .modal-content
        {
            position: relative;
            top: 8rem;
        }
        #footer
        {
            width: 100%;
            position: fixed;
            bottom: 0px;
            padding: 4px;
            font-weight: bold;
            font-size: 12px;
            margin: 0 auto;
            background: #8d8888;
        }
        #footer span
        {
            display: block;
            width: 50rem;
            margin: 0 auto;
        }
        .ver_default
        {
            height: 10px;
            padding-top: 3px;
            font-size: 12px;
            color: #ac2925;
            visibility: hidden;
        }
        .ver_info
        {
            color: #B87D00;
            visibility: visible;
        }
        .ver_err
        {
            color: #ac2925;
            visibility: visible;
        }
    </style>
</head>
<body>
    <!-- todo 响应式导航条-->
    <nav id="nav" class="navbar navbar-default" role="navigation">
      <!-- todo 相应收起时-->
      <div class="navbar-header">
        <a href="Index.aspx" class="navbar-brand">
          <img src="img/logo.png" alt="BRAND" style="margin-top: -10px">
        </a>
        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#collapsed_humbeger">
          <span class="sr-only">Toggle navigation</span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
        </button>
      </div>
      <!-- todo 展开时-->
      <div class="collapse navbar-collapse" id="collapsed_humbeger">
        <!-- todo 一组按钮-->
        <ul class="nav navbar-nav" id="nav_col">
          <li><a href="Index.aspx">主页</a></li>
          <li class="dropdown active">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              功能导航
              <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
              <li><a href="#">单体预警估计介绍</a></li>
              <li><a href="#">场景性预警估计介绍</a></li>
              <li><a href="#">预警评估介绍</a></li>
              <li><a href="#">关于本系统</a></li>
            </ul>
          </li>
          <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              地灾预警
              <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
              <li><a href="#">时间类型下拉菜单</a></li>
              <li><a href="#">灾害类型下拉菜单</a></li>
            </ul>
          </li>
          <li class="dropdown">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
              地灾监测
              <span class="caret"></span>
            </a>
            <ul class="dropdown-menu" role="menu">
              <li><a href="#">站点监测</a></li>
              <li><a href="#">站点管理</a></li>
            </ul>
          </li>
        </ul>
        <!-- todo 登陆注册-->
        <ul class="nav navbar-nav navbar-right">
          <li><a href="#login_model" data-toggle="modal">登陆</a></li>
          <li><a href="#register_model" data-toggle="modal">注册</a></li>
        </ul>
        <!-- todo 搜索表单-->
        <form class="navbar-form navbar-right" role="search">
          <div class="form-group has-feedback">
            <input type="text" class="form-control" placeholder="Search">
            <span class="glyphicon glyphicon-search form-control-feedback"></span>
          </div>
          <button type="submit" class="btn btn-default">Submit</button>
        </form>
      </div>
    </nav>
    <!--todo 主体-->
    <div class="container-fluid">
        <!-- todo 主体内容 -->
        <div class="row">
            <!-- todo 主体左侧面板-->
            <div class="col-xs-2">
                <div class="panel panel-default">
                    <div class="panel-heading" data-toggle="collapse" data-target="#fun_nav">
                        <span class="glyphicon glyphicon-dashboard"></span>功能导航
                    </div>
                    <ul class="list-group collapse in" id="fun_nav">
                        <li class="list-group-item nav_background"><a href="#">单体预警估计介绍</a></li>
                        <li class="list-group-item"><a href="#">场景性预警估计介绍</a></li>
                        <li class="list-group-item"><a href="#">预警评估介绍</a></li>
                        <li class="list-group-item"><a href="#">关于本系统</a></li>
                    </ul>
                </div>
            </div>
            <!-- todo 主体右侧面板-->
            <div class="col-xs-10">
                <div class="panel panel-default">
                    <!-- todo 面板主体-->
                    <div class="panel-body">
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Accusantium amet architecto
                        consectetur, delectus, dolor doloremque error ex harum illo laudantium natus necessitatibus
                        non perspiciatis quam quos repellendus sint sunt suscipit. Lorem ipsum dolor sit
                        amet, consectetur adipisicing elit. Accusantium aliquam autem beatae cumque doloremque
                        exercitationem harum id ipsum laudantium minus molestias, nobis, perspiciatis placeat
                        quia similique, sit velit vero. Corporis! Lorem ipsum dolor sit amet, consectetur
                        adipisicing elit. Explicabo iste iure officia quis quos repudiandae, sunt? Accusantium,
                        consectetur cum debitis eum fuga, iure molestiae nemo omnis quasi quis recusandae
                        repellendus? Lorem ipsum dolor sit amet, consectetur adipisicing elit. Accusamus
                        alias, aperiam doloremque esse et, fugiat hic iste libero minima modi, mollitia
                        neque nihil odio quia quidem quos reprehenderit temporibus veniam?
                    </div>
                </div>
            </div>
        </div>
        <!-- todo 登陆模态框 -->
        <div class="modal fade bs-example-modal-sm" id="login_model" tabindex="-1" role="dialog"
            aria-labelledby="login" aria-hidden="true">
            <!--半透明背景层-->
            <div class="modal-dialog modal-sm">
                <!--背景/边框/阴影-->
                <div class="modal-content">
                    <!-- 头部-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="login">
                            登陆</h4>
                    </div>
                    <!-- 主体-->
                    <div class="modal-body">
                        <!-- 添加水平表单-->
                        <form class="form-horizontal" role="form">
                        <div class="form-group has-feedback">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="text" class="form-control" id="log_una" placeholder="请输入用户名">
                                <span class="glyphicon glyphicon-user form-control-feedback"></span>
                            </div>
                        </div>
                        <div class="form-group has-feedback">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="password" class="form-control" id="log_psd" placeholder="请输入密码">
                                <span class="glyphicon glyphicon-cloud form-control-feedback left"></span>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-1 col-sm-10">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox">
                                        记住密码
                                    </label>
                                </div>
                            </div>
                        </div>
                        </form>
                    </div>
                    <!-- 尾部-->
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary btn-block">
                            登陆</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- todo 注册模态框 -->
        <div class="modal fade bs-example-modal-sm" id="register_model" tabindex="-1" role="dialog"
            aria-labelledby="register" aria-hidden="true">
            <!--半透明背景层-->
            <div class="modal-dialog modal-sm">
                <!--背景/边框/阴影-->
                <div class="modal-content">
                    <!-- 头部-->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="register">
                            注册</h4>
                    </div>
                    <!-- 主体-->
                    <div class="modal-body">
                        <!-- 添加水平表单-->
                        <form class="form-horizontal" role="form">
                        <div class="form-group has-feedback" id="from_group1">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="text" class="form-control" id="reg_una" placeholder="请输入用户名">
                                <span class="glyphicon glyphicon-user form-control-feedback"></span>
                            </div>
                            <div class="col-sm-offset-1 col-sm-10 ver_default" id="ver_una">
                                用户名为空，请重新输入
                            </div>
                        </div>
                        <div class="form-group has-feedback" id="from_group2">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="password" class="form-control" id="reg_psd" placeholder="请输入密码">
                                <span class="glyphicon glyphicon-cloud form-control-feedback left"></span>
                            </div>
                            <div class="col-sm-offset-1 col-sm-10 ver_default" id="ver_psd">
                                密码为空，请重新输入
                            </div>
                        </div>
                        <div class="form-group has-feedback" id="from_group3">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="password" class="form-control" id="reg_psd_again" placeholder="请再次输入密码">
                                <span class="glyphicon glyphicon-cloud form-control-feedback left"></span>
                            </div>
                            <div class="col-sm-offset-1 col-sm-10 ver_default" id="ver_psd_again">
                                验证密码为空，请重新输入
                            </div>
                        </div>
                        </form>
                    </div>
                    <!-- 尾部-->
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary btn-block">
                            登陆</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--todo 页脚-->
    <p id="footer">
        <span>联系我们：©北京中地数码有限公司 公司地址：北京海淀区上地三街9号嘉华大厦C栋1201 100085</span>
    </p>
    <script src="http://cdn.bootcss.com/jquery/1.11.1/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <!--<script src="js/cesium_mapgis.js"></script>-->
    <script>
        //运用事件代理机制，取消a的默认事件
        $("#fun_nav > .list-group-item").on('click', 'a', function (e) {
            e.preventDefault();
        });


        //给左侧导航栏动态添加背景颜色
        $("#fun_nav").on('mouseover', 'li', function () {
            $(this).addClass('nav_background').siblings('.nav_background').removeClass('nav_background');
        });

        //鼠标移入和点击当前高亮其余不显示
        $("#nav_col").on('mouseover', 'li', function () {
            $(this).addClass('active').siblings('.active').removeClass('active');
        });

        /*todo 验证时的逻辑
        todo 验证成功，添加.has-success属性，图标改为对勾图标
        todo 否则   ，添加.has-error属性  ，图标改为错误图标
        todo 需改 进行封装 编写 ？
        */

        //todo 注册用户名时的逻辑
        //定义验证用户名的正则表达式      4-8位字母
        var user_reg = new RegExp('^[a-zA-Z]{4,8}$', 'ig');
        //鼠标移入焦点，提示用户名格式
        $("#reg_una").on("focus", function () {
            $("#ver_una").removeClass("ver_err").addClass("ver_info");
            $("#ver_una").html("用户名由4-8位字母组成");
            $("#from_group1").removeClass("has-error has-success");
            $("#reg_una + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-user");
        });
        //鼠标移出焦点，进行验证
        $("#reg_una").on("blur", function () {
            //todo 如果开始输入的不正确 执行以下代码
            if (this.value == null || this.value == "") {
                $("#ver_una").addClass("ver_err");
                $("#ver_una").html("用户名不能为空");
                $("#from_group1").addClass("has-error");
                $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
            } else {
                if (!user_reg.test(this.value)) {
                    $("#ver_una").addClass("ver_err");
                    $("#ver_una").html("用户名格式不对");
                    $("#from_group1").addClass("has-error");
                    $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
                } else {
                    $("#ver_una").removeClass("ver_err");
                    $("#from_group1").addClass("has-success");
                    $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
                    $("#ver_una").html("");
                }
            }

            //todo 如果一开始就输入正确 则执行以下代码
            if (user_reg.test(this.value)) {
                $("#ver_una").removeClass("ver_err");
                $("#from_group1").addClass("has-success");
                $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
                $("#ver_una").html("");
            };
        });


        //todo 注册密码时的逻辑
        //定义验证密码的正则表达式         6-8位字母数字
        var psd_reg = new RegExp('^[0-9A-Za-z]{6,8}');
        //鼠标移入焦点，提示密码格式
        $("#reg_psd").on("focus", function () {
            $("#ver_psd").removeClass("ver_err").addClass("ver_info");
            $("#ver_psd").html("密码由6-8位字母组成");
            $("#from_group2").removeClass("has-error has-success");
            $("#reg_psd + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");
        });
        //鼠标移出焦点，进行验证
        $("#reg_psd").on("blur", function () {
            //todo 如果开始输入的不正确 执行以下代码
            if (this.value == null || this.value == "") {
                $("#ver_psd").addClass("ver_err");
                $("#ver_psd").html("密码不能为空");
                $("#from_group2").addClass("has-error");
                $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
            } else {
                if (!user_reg.test(this.value)) {
                    $("#ver_psd").addClass("ver_err");
                    $("#ver_psd").html("密码格式不对");
                    $("#from_group2").addClass("has-error");
                    $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
                } else {
                    $("#ver_psd").removeClass("ver_err");
                    $("#from_group2").addClass("has-success");
                    $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
                    $("#ver_psd").html("");
                }
            }

            //todo 如果一开始就输入正确 则执行以下代码
            if (user_reg.test(this.value)) {
                $("#ver_psd").removeClass("ver_err");
                $("#from_group2").addClass("has-success");
                $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
                $("#ver_psd").html("");
            };
        });


        //todo 重新输入密码验证时的逻辑
        //鼠标移入焦点，提示输入格式格式
        $("#reg_psd_again").on("focus", function () {
            $("#ver_psd_again").removeClass("ver_err").addClass("ver_info");
            $("#ver_psd_again").html("请输入和上面相同的密码");
            $("#from_group3").removeClass("has-error has-success");
            $("#reg_psd_again + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");
        });
        //鼠标移出焦点，进行验证 表单内容应与上面表单内容相同  判断正确时应该先判断是否为空，如果不为空在判断是否正确
        $("#reg_psd_again").on("blur", function () {
            //todo 如果开始输入的不正确 执行以下代码
            if (this.value == null || this.value == "") {
                $("#ver_psd_again").addClass("ver_err");
                $("#ver_psd_again").html("验证密码不能为空");
                $("#from_group3").addClass("has-error");
                $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-remove");
                //todo 如果不为空且输入正确 则执行以下代码
            } else if ($("#reg_psd").val() == $("#reg_psd_again").val()) {
                $("#ver_psd_again").removeClass("ver_err");
                $("#from_group3").addClass("has-success");
                $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-ok");
                $("#ver_psd_again").html("");
            } else {
                if ($("#reg_psd").val() !== $("#reg_psd_again").val()) {
                    $("#ver_psd_again").addClass("ver_err");
                    $("#ver_psd_again").html("和上面密码不一致");
                    $("#from_group3").addClass("has-error");
                    $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-remove");
                } else {
                    $("#ver_psd_again").removeClass("ver_err");
                    $("#from_group3").addClass("has-success");
                    $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-ok");
                    $("#ver_psd_again").html("");
                }
            }
        });


        //todo 模态框关闭时，点击关闭按钮，重置class和内容
        $("#mod_cls").on("click", function () {
            $("#reg_una,#reg_psd,#reg_psd_again").val("");
            $("#ver_una,#ver_psd,#ver_psd_again").removeClass("ver_err").removeClass("ver_info").addClass("ver_default");
            $("#from_group1,#from_group2,#from_group3").removeClass("has-error").removeClass("has-success");
            $("#reg_una + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-user");
            $("#reg_psd + span,#reg_psd_again + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");

        });
    </script>
</body>
</html>
