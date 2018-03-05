<%@ Page Language="C#" AutoEventWireup="true" CodeFile="openlayer.aspx.cs" Inherits="openlayer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <title>openlayer二维地图</title>
    <link href="css/ol.css" rel="stylesheet" type="text/css" />
    <script src="js/ol.js" type="text/javascript"></script>
    <style type="text/css">
        #mapCon
        {
            width: 100%;
            height: 95%;
            position: absolute;
        }
        /*=S 自定义鹰眼样式 */
        .ol-custom-overviewmap, .ol-custom-overviewmap.ol-uncollapsible
        {
            bottom: auto;
            left: auto; /* 右侧显示 */
            right: 0; /* 顶部显示 */
            top: 0;
        }
        /* 鹰眼控件展开时控件外框的样式 */
        .ol-custom-overviewmap:not(.ol-collapsed)
        {
            border: 1px solid black;
        }
        /* 鹰眼控件中地图容器样式 */
        .ol-custom-overviewmap .ol-overviewmap-map
        {
            border: none;
            width: 300px;
        }
        /* 鹰眼控件中显示当前窗口中主图区域的边框 */
        .ol-custom-overviewmap .ol-overviewmap-box
        {
            border: 2px solid red;
        }
        /* 鹰眼控件展开时其控件按钮图标的样式 */
        .ol-custom-overviewmap:not(.ol-collapsed) button
        {
            bottom: auto;
            left: auto;
            right: 1px;
            top: 1px;
        }
        /*=E 自定义鹰眼样式 */
    </style>
</head>
<body>
    <div id="mapCon">
    </div>
    <script type="text/javascript">
        //实例化比例尺控件（ScaleLine）
        var scaleLineControl = new ol.control.ScaleLine({
            //设置比例尺单位，degrees、imperial、us、nautical、metric（度量单位）
            units: "metric"
        });

        //实例化鹰眼控件（OverviewMap）,自定义样式的鹰眼控件
        var overviewMapControl = new ol.control.OverviewMap({
            //鹰眼控件样式（see in overviewmap-custom.html to see the custom CSS used）
            className: 'ol-overviewmap ol-custom-overviewmap',
            //鹰眼中加载同坐标系下不同数据源的图层
            layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM({
                    'url': 'http://{a-c}.tile.opencyclemap.org/cycle/{z}/{x}/{y}.png'
                })
            })
        ],
            //鹰眼控件展开时功能按钮上的标识（网页的JS的字符编码）
            collapseLabel: '\u00BB',
            //鹰眼控件折叠时功能按钮上的标识（网页的JS的字符编码）
            label: '\u00AB',
            //初始为展开显示方式
            collapsed: false
        });


        //实例化Map对象加载地图
        var map = new ol.Map({
            //地图容器div的ID
            target: 'mapCon',
            //地图容器中加载的图层
            layers: [
            //加载瓦片图层数据
            new ol.layer.Tile({
                //图层对应数据源，此为加载OpenStreetMap在线瓦片服务数据
                source: new ol.source.OSM()
            })
        ],
            //地图视图设置
            view: new ol.View({
                //地图初始中心点
                center: [0, 0],
                //地图初始显示级别
                zoom: 2
            }),
            //加载控件到地图容器中
            //加载比例尺控件
            controls: ol.control.defaults().extend([scaleLineControl, overviewMapControl])
        });
    </script>
</body>
</html>
