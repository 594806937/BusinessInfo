/**
* Created by luke on 2016/12/20.
*/
var webSS = new webSceneControl('GlobeView'); //,{showInfo:true}

//todo 开启图层展示功能，默认图层展示为禁用
//var webSS = new webSceneControl('GlobeView',{showInfo:true});

var viewer = webSS.viewer;
var webRoot = webSS.root;
var layers = webSS.layers;

webSS.appendGoogleMap('lyrs=s@130');

//显示底部状态栏
webSS.showPosition('coordinate_location');
//切换场景模式
function changeSceneMode(mode) {
    webSS.changeSceneMode(mode);
}

function zoomOut() {
    webSS.zoomOut();
}

function zoomIn() {
    webSS.zoomIn();
}
var measure;
function startMeasure() {
    measure = new Measure(webSS, 1);
    measure.startMeasure();
}
function stopMeasure() {
    if (measure) {
        measure.stopMeasure();
    }
}
//地形深度检测
//viewer.scene.globe.depthTestAgainstTerrain = true
//鼠标左键点击事件回调函数
function leftClickCallBack(event) {
    //屏幕坐标作为地理坐标（注意返回的坐标单位是弧度）
    var position = webSS.screenPositionToCartographic(event.position);

    if (webRoot.defined(position)) {

        height = viewer.scene.globe.getHeight(position);
        //获取高程接口
        //height = webSS.getHeightFromDegrees(121,35);

        //计算距离示例
        // var positions = [webRoot.Cartographic.fromDegrees(78,31,20), webRoot.Cartographic.fromDegrees(78,32,20)];
        // measure= new Measure(webSS);
        // var distance = measure.calcDistanceFromDegrees(positions);
        //弧度转为度
        //经度
        longitudeString = webRoot.Math.toDegrees(position.longitude);
        //纬度
        latitudeString = webRoot.Math.toDegrees(position.latitude);
        alert('点击处经纬度为：' + longitudeString.toFixed(4) + ',' + latitudeString.toFixed(4) + '高度为：' + height + '米');
    }
}

var mapGisDocTile;
var mapGisTile;

var polygonByPoints;
var LineByPoints;
var model1;
var modelsByString;
var modelsByFile;
var labelIcon;
var label;

var polyline;
var customPolygon;
var drawHandler;
var color = webSS.getColor(1, 0, 0, 0.5); //webRoot.Color.fromRandom({alpha : 1.0});

//todo 设置默认隐藏（图层）方法
function layerhide() {
    $("#search_layer").hide();
}
//todo 设置隐藏切换后图层的方法
function afterlayerhide() {
    $("#after_search_layer").hide();
}

//todo 设置点击叉号退会切换前图层列表
function hideafterlayer() {
    $("#after_search_layer").hide();
    $("#search_layer").show();
}


//todo 定义鼠标移入高亮移除不高亮方法
function highlight() {
    $(".layer_item").on('mouseenter', function () {
        $(this).css("background", '#f3f3f3');
    });
    $(".layer_item").on('mouseleave', function () {
        $(this).css("background", "#9d9d9d");
    });
}

//todo 默认加载地形和标注功能
function loadterrin_pin() {

    //todo 默认隐藏
    layerhide();
    afterlayerhide();


    //todo 添加标注的地图图层
    var url = 'http://{s}.tianditu.com/img_w/wmts?service=WMTS&version=1.0.0&request=GetTile&tilematrix={TileMatrix}&layer=img&style={style}&tilerow={TileRow}&tilecol={TileCol}&tilematrixset={TileMatrixSet}&format=tiles';
    var labelImageryProvider = new webRoot.WebMapTileServiceImageryProvider({
        url: url,
        layer: 'img',
        style: 'default',
        format: 'tiles',
        tileMatrixSetID: 'w',
        credit: new webRoot.Credit('天地图全球影像服务'),
        subdomains: ['t0', 't1', 't2', 't3', 't4', 't5', 't6', 't7'],
        maximumLevel: 18
    });

    var imageryLayers = viewer.imageryLayers;
    var url2 = 'http://{s}.tianditu.com/cia_w/wmts?service=WMTS&version=1.0.0&request=GetTile&tilematrix={TileMatrix}&layer=cia&style={style}&tilerow={TileRow}&tilecol={TileCol}&tilematrixset={TileMatrixSet}&format=tiles';
    var labelImagery = new webRoot.WebMapTileServiceImageryProvider({
        url: url2,
        layer: 'cia',
        style: 'default',
        format: 'tiles',
        tileMatrixSetID: 'w',
        credit: new webRoot.Credit('天地图全球影像中文注记服务'),
        subdomains: ['t0', 't1', 't2', 't3', 't4', 't5', 't6', 't7']
    });

    imageryLayers.addImageryProvider(labelImageryProvider);
    imageryLayers.addImageryProvider(labelImagery);


    //todo 加载带地形图
    // var terrainProvider = new webRoot.CesiumTerrainProvider({
    //     url: '//assets.agi.com/stk-terrain/world'
    // });
    // viewer.terrainProvider = terrainProvider;

    // var terrainProvider = new webRoot.VRTheWorldTerrainProvider({
    //     url : 'https://www.vr-theworld.com/vr-theworld/tiles1.0.0/73/'
    // });
    // viewer.terrainProvider = terrainProvider;

    //todo 加载从Esri映像服务请求的高度映射生成地形，DTM  问题？服务器不成功
    // var terrainProvider = new webRoot.ArcGisImageServerTerrainProvider({
    //     url : 'https://elevation.arcgisonline.com/ArcGIS/rest/services/WorldElevation/DTMEllipsoidal/ImageServer',
    //     token : 'KED1aF_I4UzXOHy3BnhwyBHU4l5oY6rO6walkmHoYqGp4XyIWUd5YZUC1ZrLAzvV40pR6gBXQayh0eFA8m6vPg..',
    //     proxy : new webRoot.DefaultProxy('/terrain/')
    // });
    // viewer.terrainProvider = terrainProvider;


    function cancelGeocode(viewModel) {
        viewModel._isSearchInProgress = false;
        if (webRoot.defined(viewModel._geocodeInProgress)) {
            viewModel._geocodeInProgress.cancel = true;
            viewModel._geocodeInProgress = undefined;
        }
    }

    function updateCamera(viewModel, destination) {
        viewModel._scene.camera.flyTo({
            destination: destination,
            complete: function () {
                viewModel._complete.raiseEvent();
            },
            duration: viewModel._flightDuration,
            endTransform: webRoot.Matrix4.IDENTITY,
            maximumHeight: 30
        });
    }

    function geocode(viewModel) {
        var query = viewModel.searchText;

        if (/^\s*$/.test(query)) {
            //whitespace string
            return;
        }

        // If the user entered (longitude, latitude, [height]) in degrees/meters,
        // fly without calling the geocoder.
        var height = 5000;
        var splitQuery = query.match(/[^\s,\n]+/g);
        if ((splitQuery.length === 2) || (splitQuery.length === 3)) {
            var longitude = +splitQuery[0];
            var latitude = +splitQuery[1];

            var obj = GPS.gcj_decrypt_exact(latitude, longitude);
            height = (splitQuery.length === 3) ? +splitQuery[2] : 5000.0;


            if (!isNaN(longitude) && !isNaN(latitude) && !isNaN(height)) {
                updateCamera(viewModel, webRoot.Cartesian3.fromDegrees(obj.lon, obj.lat, height));
                return;
            }
        }
        viewModel._isSearchInProgress = true;

        var smPOI = 'http://www.supermapol.com/iserver/services/localsearch/rest/searchdatas/China/poiinfos.jsonp';
        var promise = webRoot.loadJsonp(smPOI, {
            parameters: {
                keywords: query,
                city: "北京市",
                location: '',
                radius: '',
                leftLocation: '',
                rightLocation: '',
                pageSize: 50,
                pageNum: 1,
                key: 'fvV2osxwuZWlY0wJb8FEb2i5'
            },
            callbackParameterName: 'callback',
            jsonpName: 'callBack'
        });

        //todo result为查询的内容
        var geocodeInProgress = viewModel._geocodeInProgress = webRoot.when(promise, function (result) {
            if (geocodeInProgress.cancel) {
                return;
            }
            viewModel._isSearchInProgress = false;

            if (result.length === 0 || result.totalHints === 0) {
                viewModel.searchText = viewModel._searchText + ' (not found)';
                return;
            }
            if (webRoot.defined(viewModel.entities)) {
                for (var i = 0; i < viewModel.entities.length; i++) {
                    viewer.entities.remove(viewModel.entities[i]);
                }
            }
            viewModel.entities = [];

            //todo 遍历查询结果 数据
            var obj;
            var html = '';
            for (var i = 0; i < result.poiInfos.length; i++) {
                var resource = result.poiInfos[i];
                viewModel._searchText = resource.name;
                var location = resource.location;

                obj = GPS.gcj_decrypt_exact(location.y, location.x);

                //todo entity为实际的点的对象  //todo 尝试加载带有标注的图标
                var entity = {
                    id: resource.name + i,
                    position: webRoot.Cartesian3.fromDegrees(obj.lon, obj.lat),
                    // point: {
                    //     show: true, // default
                    //     color: webRoot.Color.SKYBLUE, // default: WHITE
                    //     pixelSize: 10, // default: 1
                    //     outlineColor: webRoot.Color.YELLOW, // default: BLACK
                    //     outlineWidth: 3 // default: 0
                    // },
                    billboard: {
                        image: './img/ZZBlue.png',
                        show: true,
                        scale: 1.0,
                        color: webRoot.Color.SKYBLUE,
                        verticalOrigin: webRoot.VerticalOrigin.BOTTOM
                    }
                };

                // //todo 将标注加入图层中
                // webRoot.when.all([entity], function(pins){
                //     viewer.zoomTo(pins);
                // });
                entity.description = new webRoot.ConstantProperty(resource.name);

                viewModel.entities.push(entity);
                viewer.entities.add(entity);

                //todo 此处应写加载图层列表的逻辑的代码 考虑到兼容性的问题应改
                html = html + '<div class="layer_item"' + 'id=' + resource.name + '>' + '<p>' + resource.name + '</p>' + '<p>' + '</p>' + entity.position + '</div>';

                $('#for_data').html(html);
                $("#search_layer").show();
                //todo 调用hightlight()方法
                highlight();
            }


            //todo 设置图层点击item后方法原图层隐藏点击图层显示       问题? 应拿到当前的名字 现在并不是
            $(".layer_item").on("click", function () {
                afterclicklayer(this.id);
            });

            //封装点击列表后的样式
            function afterclicklayer(str) {   //function中的id为window todo 考虑到兼容性的问题应改
                layerhide();

                html = '<div class="after_layer_item">' + '<div class="after_name">' + str + '<a href="javascript:hideafterlayer()" class="ui-dialog-closebutton"></a>' + '</div>' + '</div>'


                $("#after_search_layer").show();
                $('#after_search_layer').html(html);

                //todo 打印出event事件 为undefined
                //console.log(e);
            }

            //todo 此处应写相关事件
            //todo ？设置点击事件，应判断是否为可点击的点，如果是可点击的点则执行操作，反之不执行
            //todo 问题？将事件封装后进行，则名称为underfind
            this.viewer.canvas.onclick = function () {
                //afterclicklayer();
            };


            updateCamera(viewModel, webRoot.Cartesian3.fromDegrees(obj.lon, obj.lat, height));
        }, function () {
            if (geocodeInProgress.cancel) {
                return;
            }

            viewModel._isSearchInProgress = false;
            viewModel.searchText = viewModel._searchText + ' (error)';
        });
    }

    var geocoder = viewer.geocoder.viewModel;

    geocoder._searchCommand = webRoot.createCommand(function () {
        if (geocoder.isSearchInProgress) {
            cancelGeocode(geocoder);
        } else {
            geocode(geocoder);
        }
    });

}

loadterrin_pin();

//todo 选择功能加载功能
function selectTiles() {
    var selectKey = document.getElementById('selectTiles').value;
    if (selectKey == 'loadSingle') {
        var earthface = webSS.appendImageByUrl('Mapgis/MapgisPlugin/Widgets/Images/earthface.jpg', -180.0, -90, 180.0, 90);
    } else if (selectKey == 'loadLabelIcon') {

        //添加文本图标
        // labelIcon  = webSS.appendLabelIcon('注记文本',110,33,0,'14pt 黑体',webSS.getColor(0.96,0.96,0.19,1),'./img/glyphicons_242_google_maps.png',32,32,10000000,1,'top','这是属性信息查询时可以看到');
        //说明：options类型的参数并不需要每个参数
        var options = {
            font: '14pt 黑体',
            fillColor: webSS.getColor(0.96, 0.96, 0.19, 1),
            outlineWidth: 1,
            verticalOrigin: webRoot.VerticalOrigin.CENTER, //垂直方向 标签的位置CENTER TOP  BOTTOM
            horizontalOrigin: webRoot.HorizontalOrigin.CENTER, //水平方向标签位置 CENTER LEFT RTGHT
            pixelOffset: new webRoot.Cartesian2(1.0, 0.0), //相对于原点的偏移量  第一个参数是x方向→ 第二个为y方向 ↓
            //随距离的缩放比
            scaleByDistance: new webRoot.NearFarScalar(1.5e3, 1.0, 1.5e4, 0.5), //两对参数 没对第一个为相机距离，第二个为显示比例
            //随远近透明度
            transparentByDistance: new webRoot.NearFarScalar(1.5e5, 1.0, 1.5e7, 0.0)
        }
        //添加文本
        labelIcon = webSS.appendLabel(110.111, 33.111, 0, '文字', options);
        webSS.flyTo(110.111, 33.111, 5000, 1);
        // webSS.flyToEx(110.111,33.111,{
        //           height:50000,
        //          heading:0, //0 //绕垂直于地心的轴旋转 ,相当于头部左右转
        //          pitch:-45, ///-90  //绕经度线旋转， 相当于头部上下
        //          roll: 0 //0         //绕纬度线旋转 ，面对的一面瞬时针转
        // });

    } else if (selectKey == 'removeLabelIcon') {
        if (label) {
            webSS.removeEntity(labelIcon);
        }
        if (true) { }
        webSS.removeEntity(labelIcon);
    } else if (selectKey == 'addTileByDoc') {
        //MapGIS以文档
        var otherOptions = {
            tileRange: webRoot.Rectangle.fromDegrees(72.9998611111111, 9.7218626686719958, 139.249771965239, 53.5800002118608),
            colNum: 3,
            rowNum: 2,
            maxLevel: 10
        };

        mapGisDocTile = webSS.appendDocTile('http://54.222.218.173:6163/igs/rest/g3d/goldwind', 0, 11, otherOptions);
        //mapGisTile = webSS.appendMapGISTile('http://127.0.0.1:6163/igs/rest/mrms/tile/DT',otherOptions2);

    } else if (selectKey == 'addTile') {
        //如果裁瓦片的时候是按照经纬度裁剪的瓦片则只设置最大级数即可
        // var otherOptions ={
        //  maxLevel:10
        //};
        var otherOptions = {
            tileRange: webRoot.Rectangle.fromDegrees(73.4625656504558, 9.7218626686719958, 139.249771965239, 53.5800002118608),
            colNum: 3,
            rowNum: 2,
            maxLevel: 10
        };

        var otherOptions2 = {
            tileRange: webRoot.Rectangle.fromDegrees(-180, -90, 180, 90),
            colNum: 2,
            rowNum: 1,
            maxLevel: 18
        };

        //webSS.appendMapGISTile('http://54.222.218.173:6163/igs/rest/mrms/tile/LCMAP_TILE',otherOptions);
        //mapGisTile =  webSS.appendMapGISTile('http://54.222.218.173:6163/igs/rest/mrms/tile/YX_TILE',otherOptions);
        //webSS.appendMapGISTile('http://54.222.218.173:6163/igs/rest/mrms/tile/WSPD',otherOptions);
        mapGisTile = webSS.appendMapGISTile('http://127.0.0.1:6163/igs/rest/mrms/tile/DT', otherOptions2);
        // mapGisTile.show= false;
        //layers.remove(mapGisTile);

    } else if (selectKey == 'removeTile') {
        if (mapGisTile) {
            layers.remove(mapGisTile);
            mapGisTile = null;
        }
        if (mapGisDocTile) {
            layers.remove(mapGisDocTile);
        }
    } else if (selectKey == 'loadModel') {
        model1 = webSS.appendModel('model', 'SampleData/models/fengji/donghua.gltf', 102.9524, 29.7829, 1420.66827, 10);
        var model2 = webSS.appendModel('model2', 'SampleData/models/fengji/donghua.gltf', 102.9236, 29.7364, 2536.3702, 10);

        webSS.flyTo(102.9236, 29.7364, 9890);


    } else if (selectKey == 'loadModelsByFile') {
        modelsByFile = webSS.appendModelsByFile('SampleData/fengji2.models');
        webSS.flyTo(117.9298, 40.3828, 1000);
    } else if (selectKey == 'loadModelsByString') {
        var models = [
            {
                "id": "document",
                "name": "Models",
                "version": "1.0"
            },
            {
                "id": "aerogenerator1",
                "name": "风机1",
                "position": {
                    "cartographicDegrees": [117.9298, 40.3828, 0]
                },
                "model": {
                    "gltf": "SampleData/models/fengji/donghua.gltf",
                    "scale": 10
                },
                "description": "这是1号风机"
            },
            {
                "id": "aerogenerator2",
                "name": "风机2",
                "position": {
                    "cartographicDegrees": [117.9263, 40.3831, 0]
                },
                "model": {
                    "gltf": "SampleData/models/fengji/donghua.gltf",
                    "scale": 10
                },
                "description": "这是2号风机风机型号为。。。"
            },
            {
                "id": "aerogenerator3",
                "name": "风机3",
                "position": {
                    "cartographicDegrees": [117.9243, 40.3830, 0]
                },
                "model": {
                    "gltf": "SampleData/models/fengji/donghua.gltf",
                    "scale": 10
                },
                "description": "这是3号风机风机型号为。。。"
            }
        ];
        //添加判断防止重复添加
        if (modelsByString == undefined) {
            modelsByString = webSS.appendModels(models);
            webSS.flyTo(117.9298, 40.3828, 1000);
        }
    } else if (selectKey == 'removeModel') {
        if (model1 != undefined) {
            webSS.removeModel(model1);
        }
    } else if (selectKey == 'removeModels') {
        if (modelsByString != undefined) {
            webSS.removeModels(modelsByString);
            modelsByString = undefined;
        }
        if (modelsByFile != undefined) {
            webSS.removeModels(modelsByFile);
            modelsByFile = undefined;
        }
    }
    else if (selectKey == 'drawPolygonByPoints') {
        var arrayp = [108.0, 25.0,
            100.0, 25.0,
            100.0, 30.0,
            108.0, 30.0];
        if (polygonByPoints == undefined) {
            polygonByPoints = webSS.drawPolygon('1', arrayp, webSS.getColor(1, 0, 0, 0.5), webSS.getColor(0, 0, 1, 1));
            webSS.flyTo(108.0, 27.4, 1400000);
        }

    } else if (selectKey == 'startDrawCustomArea') {
        //自己定义的回调函数
        function info(positions) {
            //将坐标转为经纬度
            var outPositions = webSS.cartesiansToCartographics(positions);
            alert(outPositions.toString());
        }

        drawHandler = new DrawElement(viewer);
        drawHandler.startDrawElement('polygon', info);

    } else if (selectKey == 'stopDrawCustomArea') {
        drawHandler.clearDrawElement();
    } else if (selectKey == 'removePolygon') {
        if (polygonByPoints != undefined) {
            webSS.removeEntity(polygonByPoints);
            polygonByPoints = undefined;
        }
        if (customPolygon != undefined) {
            var position = customPolygon.getPolygonPoints();
            webSS.removeEntity(customPolygon.polygon);
        }
    } else if (selectKey == 'drawLineByPoints') {
        var arrayp = [104.0, 28.0,
            106.0, 27.0,
            107.0, 28.0,
            108.0, 29.0];
        if (LineByPoints == undefined) {
            LineByPoints = webSS.drawLine('1', arrayp, 2);
        }
    } else if (selectKey == 'removeLineByPoints') {
        if (LineByPoints != undefined) {
            webSS.removeEntity(LineByPoints);
        }
    } else if (selectKey == 'outImage') {
        webSS.outImage();
    } else if (selectKey == 'removeTerrain') {
        //移除地形和标注
        //webSS.registerMouseEvent('LEFT_CLICK', leftClickCallBack);
        viewer.imageryLayers.addImageryProvider(new webRoot.WebMapTileServiceImageryProvider({
            url: 'http://{s}.tianditu.com/img_w/wmts?service=WMTS&version=1.0.0&request=GetTile&tilematrix={TileMatrix}&layer=img&style={style}&tilerow={TileRow}&tilecol={TileCol}&tilematrixset={TileMatrixSet}&format=tiles',
            layer: 'img',
            style: 'default',
            format: 'tiles',
            tileMatrixSetID: 'w',
            credit: new webRoot.Credit('天地图全球影像服务'),
            subdomains: ['t0', 't1', 't2', 't3', 't4', 't5', 't6', 't7'],
            maximumLevel: 18
        }));
    }

    else if (selectKey == 'registerMouseEvent') {
        //注册点击事件
        webSS.registerMouseEvent('LEFT_CLICK', leftClickCallBack);

    }

    else if (selectKey == 'UnRegisterMouseEvent') {
        //注销点击事件
        webSS.unRegisterMouseEvent('LEFT_CLICK');
    }

}

//todo 设置点击图层列表的按钮时，展开和收起方法
function list() {
    if ($("#icon_list").hasClass('glyphicon-align-justify')) {
        $('#for_data').slideUp();
        $("#icon_list").removeClass("glyphicon-align-justify").addClass('glyphicon-align-right');
    } else {
        $('#for_data').slideDown();
        $("#icon_list").removeClass("glyphicon-align-right").addClass('glyphicon-align-justify');
    }
}

$("#icon_list").on('click', function () {
    list();
});

//todo 定义自定义气泡框的构造函数
function SelectionIndicator(container, scene) {
    if (!webRoot.defined(container)) {
        throw new DeveloperError('container is required.');
    }

    container = getElement(container);

    this._container = container;

    var el = document.createElement('div');
    el.className = 'cesium-selection-wrapper';
    el.setAttribute('data-bind', '\
style: { "top" : _screenPositionY, "left" : _screenPositionX },\
css: { "cesium-selection-wrapper-visible" : isVisible }');
    container.appendChild(el);
    this._element = el;

    var svgNS = 'http://www.w3.org/2000/svg';
    var path = 'M -34 -34 L -34 -11.25 L -30 -15.25 L -30 -30 L -15.25 -30 L -11.25 -34 L -34 -34 z M 11.25 -34 L 15.25 -30 L 30 -30 L 30 -15.25 L 34 -11.25 L 34 -34 L 11.25 -34 z M -34 11.25 L -34 34 L -11.25 34 L -15.25 30 L -30 30 L -30 15.25 L -34 11.25 z M 34 11.25 L 30 15.25 L 30 30 L 15.25 30 L 11.25 34 L 34 34 L 34 11.25 z';

    var svg = document.createElementNS(svgNS, 'svg:svg');
    svg.setAttribute('width', 160);
    svg.setAttribute('height', 160);
    svg.setAttribute('viewBox', '0 0 160 160');

    var group = document.createElementNS(svgNS, 'g');
    group.setAttribute('transform', 'translate(80,80)');
    svg.appendChild(group);

    var pathElement = document.createElementNS(svgNS, 'path');
    pathElement.setAttribute('data-bind', 'attr: { transform: _transform }');
    pathElement.setAttribute('d', path);
    group.appendChild(pathElement);

    el.appendChild(svg);

    var viewModel = new SelectionIndicatorViewModel(scene, this._element, this._container);
    this._viewModel = viewModel;

    knockout.applyBindings(this._viewModel, this._element);

    //todo 下面的代码为自定义的添加代码 实现了一个空div气泡窗口
    //自定义部分
    var infowin = document.createElement('div');
    infowin.className = 'infowin3D';
    el.appendChild(infowin);
    //箭头
    var arrow = document.createElement('div');
    arrow.className = 'arrow';
    infowin.appendChild(arrow);
    var em = document.createElement("em");
    var span = document.createElement("span");
    arrow.appendChild(em);
    arrow.appendChild(span);
    //气泡窗口内容div
    var content = document.createElement('div');
    content.setAttribute('id', 'infowin3DContent');
    content.className = 'infowin3DContent';
    infowin.appendChild(content);
    //自定义部分结束

    //todo 封装了一个调用气泡窗口的函数
    /*
    * 弹出气泡窗口
    * @method infoWindow
    * @param  obj{position(必填):屏幕坐标,destination(必填):跳转目的点,content(必填):气泡窗口内容,css(可填):设置css的width,height}
    * @return 返回选中的模型Entity
    */
    //todo 源代码为
    //infoWindow : function()
    function infoWindow(obj) {
        var picked = this.cesiumViewer.scene.pick(obj.position);
        if (webRoot.defined(picked)) {
            var id = webRoot.defaultValue(picked.id, picked.primitive.id);
            if (id instanceof webRoot.Entity) {
                if (obj.destination) {
                    this.cesiumViewer.camera.flyTo({//初始化跳转某个地方
                        destination: obj.destination
                    });
                }
                //填充内容
                $(".cesium-selection-wrapper").show();
                $("#infowin3DContent").empty();
                $("#infowin3DContent").append(obj.content);
                //css存在情况下,设置css
                if (obj.css && obj.css.width)
                    $(".infowin3D").css("width", obj.css.width);
                if (obj.css && obj.css.height)
                    $(".infowin3D").css("height", obj.css.height);
                $(".infowin3D").css("margin-top", -($(".infowin3D").height() - 30));
                $(".arrow").css("left", ($(".infowin3D").width() / 2 - 20));
                $(".infowin3D").css("margin-left", -($(".infowin3D").width() / 2 - 76));
                $(".infowin3D").show();
                $("#infoClose3D").click(function () {
                    $(".cesium-selection-wrapper").hide();
                    $(".infowin3D").hide();
                });
                return id;
            }
        }
    }

    //调用接口-气泡窗口
    var handler3D = new webRoot.ScreenSpaceEventHandler(webRoot.cesiumViewer.scene.canvas);
    handler3D.setInputAction(function (movement) {
        //点击弹出气泡窗口
        var pick = webRoot.cesiumViewer.scene.pick(movement.position);
        if (pick && pick.id) {//选中某模型
            var cartographic = webRoot.Cartographic.fromCartesian(pick.id._position._value); //世界坐标转地理坐标（弧度）
            var point = [cartographic.longitude / Math.PI * 180, cartographic.latitude / Math.PI * 180]; //地理坐标（弧度）转经纬度坐标
            var destination = webRoot.Cartesian3.fromDegrees(point[0], point[1], 3000.0);
            var content = "<div  style='border-bottom: 1px solid #C6CBCE;'>" +
                "<span style='margin-left: 5px;'>测试测试1</span>" +
                "<div id='infoClose3D' class='closeButton' style='margin-right: 4px;'></div>" +
                "</div>" +
                "<div>" +
                "<label>测试1:</label><label>测试1</label></br>" +
                "<label>测试2:</label><label>测试2</label></br>" +
                "<label>测试3:</label><label>测试3</label></br>" +
                "</div>";
            var obj = { position: movement.position, destination: destination, content: content };
            webRoot.infoWindow(obj);
        }
    }, webRoot.ScreenSpaceEventType.LEFT_CLICK);
}


$().ready(function () {
    SelectionIndicator();
})
