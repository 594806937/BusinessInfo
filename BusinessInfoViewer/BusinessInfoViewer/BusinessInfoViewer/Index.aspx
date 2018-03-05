<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="zh-cn">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>中地数码前端框架</title>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="css/zi_qipao.css">
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/MapGISEarth.css">
    <link rel="stylesheet" type="text/css" href="Mapgis/MapgisPlugin/Widgets/widgets.css">
    <script src="Mapgis/MapgisPlugin/MapgisPlugin.min.js"></script>
    <script src="Mapgis/MapGISEarth.min.js"></script>
    <!--<script src="js/CesiumWidget/CesiumWidget.js" type="text/javascript"></script>-->
    <script src="js/gps.js"></script>
    <link href="css/index.css" rel="stylesheet">
    <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <!-- todo 响应式导航条-->
    <nav id="nav" class="navbar navbar-default" role="navigation">
      <!-- todo 响应式收起时-->
      <div class="navbar-header">
        <!--todo logo名称-->
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
      <!-- todo 响应式展开时-->
      <div class="collapse navbar-collapse" id="collapsed_humbeger">
        <!-- todo 响应式套航条按钮-->
        <ul class="nav navbar-nav" id="nav_col">
          <li class="active"><a href="Index.aspx">主页</a></li>
          <li><a href="Index.aspx" >三维地球</a></li>
          <li><a href="Index_2d.aspx">二维地图</a></li>
          <li><a href="Index_sta.aspx">统计数据</a></li>
        </ul>

        <!-- todo 登陆注册-->
        <ul class="nav navbar-nav navbar-right">
          <li><a href="#login_model" data-toggle="modal">登陆</a></li>
          <li><a href="#register_model" data-toggle="modal">注册</a></li>
        </ul>
        <!-- todo 搜索表单-->
          <form class="navbar-form navbar-right" role="search"/>
          <div class="form-group has-feedback">
            <input type="text" class="form-control" placeholder="Search">
            <span class="glyphicon glyphicon-search form-control-feedback"></span>
          </div>
          <button type="submit" class="btn btn-default">Submit</button>
        
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
                                <div role="tabpanel" class="tab-pane active" id="glb_3d">
                                    <!-- todo 三维球 start ？问题 全屏后坐标不显示 以及切换后不显示-->
                                    <!-- globe视图-->
                                    <div id="GlobeView">
                                    </div>
                                    <!-- 坐标  x -->
                                    <div id="coordinateDiv" class="coordinateClass container">
                                        <label id="coordinate_location">
                                            坐标</label>
                                    </div>
                                    <!-- 组件功能按钮-->
                                    <div id="toolbar2">
                                        <div class="select">
                                            <select id="selectTiles" onchange="selectTiles()">
                                                <option>请选择</option>
                                                <option value="removeTerrain">移除地形和标注</option>
                                                <option value="loadSingle">本地贴图</option>
                                                <option value="loadLabelIcon">加载图片文字标签</option>
                                                <option value="removeLabelIcon">移除图片文字标签</option>
                                                <option value="addTile">加载瓦片服务</option>
                                                <option value="addTileByDoc">加载文档瓦片</option>
                                                <option value="removeTile">移除加载的瓦片</option>
                                                <option value="loadModel">加载单个模型</option>
                                                <option value="removeModel">移除单个模型</option>
                                                <option value="loadModelsByFile">通过文件批量加载模型</option>
                                                <option value="loadModelsByString">通过文件组织批量加载模型</option>
                                                <option value="removeModels">移除批量添加的模型</option>
                                                <option value="drawPolygonByPoints">通过给定点画区</option>
                                                <option value="startDrawCustomArea">开始手动绘区</option>
                                                <option value="stopDrawCustomArea">停止手动绘区</option>
                                                <option value="removePolygon">移除绘制的区域</option>
                                                <option value="drawLineByPoints">绘制线路</option>
                                                <option value="removeLineByPoints">移除绘制的线路</option>
                                                <option value="registerMouseEvent">注册左键点击事件</option>
                                                <option value="UnRegisterMouseEvent">注销左键点击事件</option>
                                                <option value="outImage">输出图片</option>
                                            </select>
                                            <button class="btn btn-success" onclick="startMeasure()">
                                                开始测量</button>
                                            <button class="btn btn-danger" onclick="stopMeasure()">
                                                结束测量</button>
                                        </div>
                                    </div>
                                    <!-- 左侧遮罩层 默认隐藏-->
                                    <div id="lt_panel" class="lt_panel">
                                        <!-- 相关按钮-->
                                        <div id="Zoomtoolbar_left" class="Zoomtoolbar_left">
                                            <div class="button">
                                                <img class="S3D" src="img/3D.png" alt="3D" style="width: 32px; height: 32px;" onclick="changeSceneMode('3D')">
                                            </div>
                                            <div class="button">
                                                <img class="SClu" src="img/Clu.png" alt="平面模式" style="width: 32px; height: 32px;"
                                                    onclick="changeSceneMode  ('COLUMBUS_VIEW')">
                                            </div>
                                            <div class="button">
                                                <img class="zoomin" src="img/zoomin.png" alt="汽车" style="width: 32px; height: 32px;"
                                                    onclick="zoomIn()">
                                            </div>
                                            <div class="button">
                                                <img class="zoomout" src="img/zoomout.png" alt="汽车" style="width: 32px; height: 32px;"
                                                    onclick="zoomOut()">
                                            </div>
                                            <div class="button">
                                                <img class="layer" src="img/layer.png" alt="3D" style="width: 32px; height: 32px;"
                                                    onclick="showDialog()">
                                            </div>
                                            <div class="button">
                                                <img class="full" src="img/full_hov.png" alt="fullScreen" style="width: 32px; height: 32px;"
                                                    id="fullScreen" />
                                            </div>
                                            <div class="button">
                                                <img class="back" src="img/scale.png" alt="exitScreen" style="width: 32px; height: 32px;"
                                                    id="exitScreen" onclick="exitFull()">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- 弹出菜单 默认隐藏-->
                                    <div class="ui-dialog" id="dialog">
                                        <div class="ui-dialog-title" id="dialogTitle">
                                            图层列表 <a href="javascript:hideDialog()" class="ui-dialog-closebutton"></a><a href="javascript:void(0)">
                                                <span id="fix" class="glyphicon glyphicon-pushpin" style="left: 40px;"></span>
                                            </a>
                                        </div>
                                        <ul id="overlayer">
                                            <!--动态生成数据-->
                                        </ul>
                                    </div>
                                    <!-- 设置图层查询列表-->
                                    <div id="search_layer" class="search_layer">
                                        <div class="layer_title">
                                            图层列表 <span id="icon_list" class="glyphicon glyphicon-align-justify"></span>
                                        </div>
                                        <!--todo 此处填写动态加载的数据-->
                                        <div id="for_data">
                                            <!--<div class="layer_item">-->
                                            <!--<p>name</p>-->
                                            <!--<p>position</p>-->
                                            <!--</div>-->
                                        </div>
                                    </div>
                                    <!--设置切换后的图层查询列表-->
                                    <div id="after_search_layer" class="search_layer">
                                        <!--<div class="after_layer_item">-->
                                        <!--<div class="after_name">-->
                                        <!--灵水村-->
                                        <!--<a href="javascript:hideDialog()" class="ui-dialog-closebutton"></a>-->
                                        <!--</div>-->
                                        <!--</div>-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="footer">
                    <span>联系我们：©北京中地数码有限公司 公司地址：北京海淀区上地三街9号嘉华大厦C座1201室</span>
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
                                <input type="text" class="form-control" id="log_una" placeholder="请输入用户名" />
                                <span class="glyphicon glyphicon-user form-control-feedback"></span>
                            </div>
                        </div>
                        <div class="form-group has-feedback">
                            <div class="col-sm-offset-1 col-sm-10">
                                <input type="password" class="form-control" id="log_psd" placeholder="请输入密码" />
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
                            <span aria-hidden="true" id="mod_cls">&times;</span> <span class="sr-only">Close</span>
                        </button>
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
    <script src="js/cesium_mapgis.js"></script>
    <script src="js/index.js"></script>
</body>
</html>
