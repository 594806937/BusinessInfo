/**
 * Created by admin on 2017/1/13.
 */
//todo 修改三维球切换时的高度问题
var top_height=70;
var client_height=document.body.clientHeight;
var navtop_height=$("nav").height();
$("#GlobeView").css("height",client_height-navtop_height-top_height);

//todo 修改二维地图的高度问题
$("#glb_2d > iframe").css("height",client_height-navtop_height-top_height);


//todo 导航栏鼠标移入和点击当前高亮其余不显示
$("#nav_col").on('mouseover','li',function(){
    $(this).addClass('active').siblings('.active').removeClass('active');
});

//todo 按钮组的切换高亮显示
$("#nav_bar").on('click','button',function(){
    $(this).addClass('active').siblings('.active').removeClass('active');
});

//todo 注册用户名时的逻辑
//定义验证用户名的正则表达式      4-8位字母
var user_reg = new RegExp('^[a-zA-Z]{4,8}$','ig');
//鼠标移入焦点，提示用户名格式
$("#reg_una").on("focus",function(){
    $("#ver_una").removeClass("ver_err").addClass("ver_info");
    $("#ver_una").html("用户名由4-8位字母组成");
    $("#from_group1").removeClass("has-error has-success");
    $("#reg_una + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-user");
});
//鼠标移出焦点，进行验证
$("#reg_una").on("blur",function(){
    //todo 如果开始输入的不正确 执行以下代码
    if(this.value == null || this.value == "") {
        $("#ver_una").addClass("ver_err");
        $("#ver_una").html("用户名不能为空");
        $("#from_group1").addClass("has-error");
        $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
    }else{
        if (!user_reg.test(this.value)) {
            $("#ver_una").addClass("ver_err");
            $("#ver_una").html("用户名格式不对");
            $("#from_group1").addClass("has-error");
            $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
        }else{
            $("#ver_una").removeClass("ver_err");
            $("#from_group1").addClass("has-success");
            $("#reg_una + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
            $("#ver_una").html("");
        }
    }

    //todo 如果一开始就输入正确 则执行以下代码
    if(user_reg.test(this.value)) {
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
$("#reg_psd").on("focus",function(){
    $("#ver_psd").removeClass("ver_err").addClass("ver_info");
    $("#ver_psd").html("密码由6-8位字母组成");
    $("#from_group2").removeClass("has-error has-success");
    $("#reg_psd + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");
});
//鼠标移出焦点，进行验证
$("#reg_psd").on("blur",function(){
    //todo 如果开始输入的不正确 执行以下代码
    if(this.value == null || this.value == "") {
        $("#ver_psd").addClass("ver_err");
        $("#ver_psd").html("密码不能为空");
        $("#from_group2").addClass("has-error");
        $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
    }else{
        if (!user_reg.test(this.value)) {
            $("#ver_psd").addClass("ver_err");
            $("#ver_psd").html("密码格式不对");
            $("#from_group2").addClass("has-error");
            $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-remove");
        }else{
            $("#ver_psd").removeClass("ver_err");
            $("#from_group2").addClass("has-success");
            $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
            $("#ver_psd").html("");
        }
    }

    //todo 如果一开始就输入正确 则执行以下代码
    if(user_reg.test(this.value)) {
        $("#ver_psd").removeClass("ver_err");
        $("#from_group2").addClass("has-success");
        $("#reg_psd + span").removeClass("glyphicon-user").addClass("glyphicon-ok");
        $("#ver_psd").html("");
    };
});

//todo 重新输入密码验证时的逻辑
//鼠标移入焦点，提示输入格式格式
$("#reg_psd_again").on("focus",function(){
    $("#ver_psd_again").removeClass("ver_err").addClass("ver_info");
    $("#ver_psd_again").html("请输入和上面相同的密码");
    $("#from_group3").removeClass("has-error has-success");
    $("#reg_psd_again + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");
});
//鼠标移出焦点，进行验证 表单内容应与上面表单内容相同  判断正确时应该先判断是否为空，如果不为空在判断是否正确
$("#reg_psd_again").on("blur",function() {
    //todo 如果开始输入的不正确 执行以下代码
    if(this.value == null || this.value == "") {
        $("#ver_psd_again").addClass("ver_err");
        $("#ver_psd_again").html("验证密码不能为空");
        $("#from_group3").addClass("has-error");
        $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-remove");
        //todo 如果不为空且输入正确 则执行以下代码
    }else if($("#reg_psd").val() == $("#reg_psd_again").val()){
        $("#ver_psd_again").removeClass("ver_err");
        $("#from_group3").addClass("has-success");
        $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-ok");
        $("#ver_psd_again").html("");
    }else{
        if($("#reg_psd").val() !== $("#reg_psd_again").val()){
            $("#ver_psd_again").addClass("ver_err");
            $("#ver_psd_again").html("和上面密码不一致");
            $("#from_group3").addClass("has-error");
            $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-remove");
        }else{
            $("#ver_psd_again").removeClass("ver_err");
            $("#from_group3").addClass("has-success");
            $("#reg_psd_again + span").removeClass("glyphicon-cloud").addClass("glyphicon-ok");
            $("#ver_psd_again").html("");
        }
    }
});

//todo 模态框关闭时，点击关闭按钮，重置class和内容
$("#mod_cls").on("click",function() {
    $("#reg_una,#reg_psd,#reg_psd_again").val("");
    $("#ver_una,#ver_psd,#ver_psd_again").removeClass("ver_err").removeClass("ver_info").addClass("ver_default");
    $("#from_group1,#from_group2,#from_group3").removeClass("has-error").removeClass("has-success");
    $("#reg_una + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-user");
    $("#reg_psd + span,#reg_psd_again + span").removeClass("glyphicon-remove").removeClass("glyphicon-ok").addClass("glyphicon-cloud");

});


//todo 点击工具菜单调用功能隐藏和显示  应注意动画排队的问题
//右侧菜单默认是隐藏的
$("#Zoomtoolbar").hide();

//右侧菜单默认是隐藏的
$("#Zoomtoolbar_left").hide();

// //todo 右侧
// //鼠标移入右侧面板显示右侧菜单
// $("#rt_panel").on('mouseover',function(e){
//     e.stopPropagation();
//     $("#Zoomtoolbar").fadeIn();
//
// });
//
// //鼠标移出右侧面板隐藏右侧菜单
// $("#rt_panel").on('mouseleave',function(e){
//     e.stopPropagation();
//     //应该判断移出事件发生之后再触发新的事件
//     $('#Zoomtoolbar').fadeOut(function(){
//         $(this).stop(true);
//     });
// });

//todo 左侧
//鼠标移入右侧面板显示左侧菜单
$("#lt_panel").on('mouseenter',function(e){
    e.stopPropagation();
    $("#Zoomtoolbar_left").fadeIn();
});

//鼠标移出左侧面板隐藏左侧菜单
$("#lt_panel").on('mouseleave',function(e){
    e.stopPropagation();
    //应该判断移出事件发生之后再触发新的事件
    $('#Zoomtoolbar_left').fadeOut(function(){
        $(this).stop(true);
    });
});


//todo 实现拖拽功能 仿写慕课网 demo
//获取元素对象
function g(id){return document.getElementById(id);}

var mouseOffsetX = 0;   //X的偏移
var mouseOffsetY = 0;   //Y的偏移

var isDraging= false;   //是否可拖拽的标记
var isFixed= false;     //是否可固定图标

//三个鼠标事件
//鼠标事件1  - 在标题栏上按下 （要计算鼠标相对拖拽元素的左上角的坐标，并且标记元素为可拖拽）
$('#dialogTitle').on('mousedown',function(e){
    var e = e || window.event;  //兼容IE8
    e.stopPropagation();
    e.preventDefault();
    mouseOffsetX = e.pageX - g('dialog').offsetLeft ;  //鼠标事件当前按下去的X的坐标 - 登陆浮层相对于页面左边的位置
    mouseOffsetY = e.pageY - g('dialog').offsetTop  ;  //鼠标事件当前按下去的Y的坐标 - 登陆浮层相对于页面上面的位置
    //先判断是否可固定，在判断是否拖拽
    if(isFixed === false){
        isDraging=true;
    }else{
        isDraging=false;
    }
});


//鼠标事件2  - 鼠标移动时（要检测，元素是否可标记为移动，如果是，则更新元素的位置，到当前鼠标的位置
// 【ps:要减去第一步中获得的偏移】）
document.onmousemove = function(){
    var e = e || window.event;
    e.stopPropagation();

    var mouseX = e.pageX;    //鼠标当前的X坐标
    var mouseY = e.pageY;    //鼠标当前的Y坐标

    var moveX = 0;    //鼠标移动事件发生后浮层元素的新的X位置
    var moveY = 0;    //鼠标移动事件发生后浮层元素的新的Y位置

    if(isDraging && $(this).nodeName !== 'SPAN' ){
        moveX = mouseX - mouseOffsetX;
        moveY = mouseY - mouseOffsetY;

        //范围限定 ， moveX > 0 并且 moveX < (GlobeView容器最大宽度 - 浮层自身的宽度)
        //范围限定 ， moveY > 0 并且 movey < (GlobeView容器最大高度 - 浮层自身的高度)

        //获得GlobeView视图的宽度和高度    问题所在可能由于边框位置设置的不对的缘故
        var GlobeView_width = g('GlobeView').offsetWidth;
        var GlobeView_height = g('GlobeView').offsetHeight;

        //获得图层的宽度和高度
        var dialogWidth = g('dialog').offsetWidth;
        var dialogHeight =g('dialog').offsetHeight;

        //计算moveX与moveY的最大值
        var maxX = GlobeView_width  - dialogWidth;
        var maxY = GlobeView_height - dialogHeight;

        //固定范围 学习math方法的使用
        //moveX = Math.max(0,moveX);   设置，moveX最小值为0的方法
        moveX = Math.min(maxX,Math.max(0,moveX));
        moveY = Math.min(maxY,Math.max(0,moveY));

        g('dialog').style.left = moveX + 'px';
        g('dialog').style.top  = moveY + 'px';
    }
}

//鼠标事件3  - 鼠标松开的时候（标记元素为不可拖动即可）
document.onmouseup = function(){
    isDraging = false;
}

//展示图层
function showDialog(){
    g('dialog').style.display = 'block';
}

//隐藏图层
function hideDialog(){
    g('dialog').style.display = 'none';
}

//固定图层
function fixDialog(){
    isDraging= false;
}


//todo 修改为固定  更改图标
$('#dialogTitle').on('mousedown','a:nth-child(2) > span',function(e){
    e.stopPropagation();
    e.preventDefault();
    //todo 写入逻辑应为 判断如果不可拖拽，改为固定 更改class 如果可拖拽 改为不固定 更改class
    if($(e.target).hasClass('glyphicon-pushpin')){
        $(e.target).removeClass("glyphicon-pushpin").addClass('glyphicon-link');
        isDraging= false;
        isFixed = true;
    }else{
        $(e.target).removeClass("glyphicon-link").addClass('glyphicon-pushpin');
        isDraging= true;
        isFixed = false;
    }
});


//todo 编写JSON数据，进行简单测试
var overlayer = [
    {
        province:"广东",
        city:[
            "广州","佛山","汕头"
        ]
    },
    {
        province:"山东",
        city:[
            "济南","烟台","青岛"
        ]
    },
    {
        province:"四川",
        city:[
            "成都","佛山","攀枝花"
        ]
    }];

var json = JSON.stringify(overlayer);
//    console.log(json);
var jsonparse = JSON.parse(json);
//console.log(jsonparse);



var html='';
//在外层先遍历得到省的数据，在内层进行二次遍历，遍历省下的城市，将含有省的li添加给html,最后添加到页面元素
for(var i = 0;i<overlayer.length;i++){
    html= html+'<li><span class="glyphicon glyphicon-plus"></span><i><input type="checkbox" style="margin-top:-1px " '+'id='+ i + '>'+overlayer[i].province+'</i><ul class="overlayer_city">';
    for(var j = 0;j<overlayer[i].city.length;j++){
        html=html+'<li><input type="checkbox" value=' + i + '>'+overlayer[i].city[j]+'</li>';
    }
    html =html+'</ul></li>';
}

$('#overlayer').html(html);



//todo 图层默认为隐藏
$('.overlayer_city').slideUp();

//todo 图层的点击加号的展开收起功能
$("#overlayer li").on('click','span',function(){
    var hasaddlogo = $(this).hasClass('glyphicon-plus');
    if(hasaddlogo){
        $(this).removeClass('glyphicon-plus').addClass('glyphicon-minus');
        $(this).next().next().slideDown('500');
    }else{
        $(this).removeClass('glyphicon-minus').addClass('glyphicon-plus');
        $(this).next().next().slideUp('500');
    }
});



//todo 写的不好需该？
//todo 图层选中状态默认为选中
$("#overlayer li input").prop("checked",true);

//todo 点击父元素图层选中后子元素全选功能
$("#0").on('click',function(){
    if(this.checked){
        $("input[value=0]").prop("checked",true);
    }else{
        $("input[value=0]").prop('checked',false);
    }
});

$("#1").on('click',function(){
    if(this.checked){
        $("input[value=1]").prop("checked",true);
    }else{
        $("input[value=1]").prop('checked',false);
    }
});

$("#2").on('click',function(){
    if(this.checked){
        $("input[value=2]").prop("checked",true);
    }else{
        $("input[value=2]").prop('checked',false);
    }
});


//在外层先遍历得到省的数据，在内层进行二次遍历，遍历省下的城市，将含有省的li添加给html,最后添加到页面元素
//ES6标准下实现AJAX的二级联动加载，如何更好地动态获得id或者class
// var html =``;
// for(var i = 0;i<overlayer.length;i++){
//     var province = overlayer[i].province;
//     html = html + `
//        <li>
//          <span class="glyphicon glyphicon-plus"></span>
//          <i>
//            <input type="checkbox" style="margin-top:-1px" id="par_layer1">${province}
//          </i>
//            <ul id="overlayer1">
//        `
//
//             for(var j = 0;j<overlayer[i].city.length;j++){
//                 var city= overlayer[i].city[j];
//                 html =html + `
//                     <li><input type="checkbox" value="1">${city}</li>
//                 `;
//             }
//
//        html = html + `
//            </ul>
//        </li>
//     `;
// }
// //将拼接后的动态生成的html添加到页面元素上;
// $('#overlayer').html(html);


//todo 实现全图功能
$("#fullScreen").on('click',function(){
    //todo 使三维地球全屏
    var elem = document.getElementById("GlobeView");
    requestFullScreen(elem);
   // requestFullScreen(document.documentElement);// 整个网页
});

//实现全屏功能方法
function requestFullScreen(element) {
    // 判断各种浏览器，找到正确的方法
    var requestMethod = element.requestFullScreen || //W3C
        element.webkitRequestFullScreen ||    //Chrome等
        element.mozRequestFullScreen || //FireFox
        element.msRequestFullScreen; //IE11
    if (requestMethod) {
        requestMethod.call(element);
    }
    else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
        var wscript = new ActiveXObject("WScript.Shell");
        if (wscript !== null) {
            wscript.SendKeys("{F11}");
        }
    }

    //todo 使三维地球全屏
    var top_height=70;
    var client_height=document.body.clientHeight;
    var navtop_height=$("nav").height();
    $("#GlobeView").css("height",client_height-navtop_height-top_height);

}

//退出全屏 判断浏览器种类方法
function exitFull() {
    // 判断各种浏览器，找到正确的方法
    var exitMethod = document.exitFullscreen || //W3C
        document.mozCancelFullScreen ||    //Chrome等
        document.webkitExitFullscreen || //FireFox
        document.webkitExitFullscreen; //IE11
    if (exitMethod) {
        exitMethod.call(document);
    }
    else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
        var wscript = new ActiveXObject("WScript.Shell");
        if (wscript !== null) {
            wscript.SendKeys("{F11}");
        }
    }
    var top_height2=70;
    var client_height2=document.body.clientHeight;
    var navtop_height2=$("nav").height();
    $("#GlobeView").css("height",client_height2-navtop_height2-top_height2);
}


//todo 判断键盘上对应的ascll码?  Esc == 27
$(document).keydown(function(event){
    if(event.keyCode == 27){
        exitFull();
    }
})

//todo 点击三维地球后，切换三维与二维地图
$('g3d').on('click',function(){
    $('.tab-content').html('');
})


// //todo 动态加载三维地球和二维地图的内容（如有后台服务器和数据库利用 ajax 调用后台数据）
// //动态加载三维地球的数据
// function load3D(){
//     var  html = '';
//     html = `
//         <div role="tabpanel" class="tab-pane active" id="glb_3d">
//             <!-- globe视图-->
//             <div id="GlobeView">
//             </div>
//             <!-- 坐标  x -->
//             <div id="coordinateDiv" class="coordinateClass container">
//                 <label id="coordinate_location">
//                 坐标
//                 </label>
//             </div>
//             <!-- 组件功能按钮-->
//             <div id="toolbar2">
//                 <div class="select">
//                     <select id="selectTiles" onchange="selectTiles()">
//                         <option>请选择</option>
//                         <option value="removeTerrain">移除地形和标注</option>
//                         <option value="loadSingle">本地贴图</option>
//                         <option value="loadLabelIcon">加载图片文字标签</option>
//                         <option value="removeLabelIcon">移除图片文字标签</option>
//                         <option value="addTile">加载瓦片服务</option>
//                         <option value="addTileByDoc">加载文档瓦片</option>
//                         <option value="removeTile">移除加载的瓦片</option>
//                         <option value="loadModel">加载单个模型</option>
//                         <option value="removeModel">移除单个模型</option>
//                         <option value="loadModelsByFile">通过文件批量加载模型</option>
//                         <option value="loadModelsByString">通过文件组织批量加载模型</option>
//                         <option value="removeModels">移除批量添加的模型</option>
//                         <option value="drawPolygonByPoints">通过给定点画区</option>
//                         <option value="startDrawCustomArea">开始手动绘区</option>
//                         <option value="stopDrawCustomArea">停止手动绘区</option>
//                         <option value="removePolygon">移除绘制的区域</option>
//                         <option value="drawLineByPoints">绘制线路</option>
//                         <option value="removeLineByPoints">移除绘制的线路</option>
//                         <option value="registerMouseEvent">注册左键点击事件</option>
//                         <option value="UnRegisterMouseEvent">注销左键点击事件</option>
//                         <option value="outImage">输出图片</option>
//                     </select>
//                     <button class="btn btn-success" onclick="startMeasure()">
//                         开始测量</button>
//                     <button class="btn btn-danger" onclick="stopMeasure()">
//                         结束测量</button>
//                 </div>
//             </div>
//             <!-- 左侧遮罩层 默认隐藏-->
//             <div id="lt_panel" class="lt_panel">
//                 <!-- 相关按钮-->
//                 <div id="Zoomtoolbar_left" class="Zoomtoolbar_left">
//                     <div class="button">
//                         <img class="S3D" src="img/3D.png" alt="3D" style="width: 32px; height: 32px;" onclick="changeSceneMode('3D')">
//                     </div>
//                     <div class="button">
//                         <img class="SClu" src="img/Clu.png" alt="平面模式" style="width: 32px; height: 32px;"
//                             onclick="changeSceneMode  ('COLUMBUS_VIEW')">
//                     </div>
//                     <div class="button">
//                         <img class="zoomin" src="img/zoomin.png" alt="汽车" style="width: 32px; height: 32px;"
//                             onclick="zoomIn()">
//                     </div>
//                     <div class="button">
//                         <img class="zoomout" src="img/zoomout.png" alt="汽车" style="width: 32px; height: 32px;" onclick="zoomOut()">
//                     </div>
//                     <div class="button">
//                         <img class="layer" src="img/layer.png" alt="3D" style="width: 32px; height: 32px;"
//                             onclick="showDialog()">
//                     </div>
//                     <div class="button">
//                         <img class="full" src="img/full_hov.png" alt="fullScreen" style="width: 32px; height: 32px;" id="fullScreen" />
//                     </div>
//                     <div class="button">
//                         <img class="back" src="img/scale.png" alt="exitScreen" style="width: 32px; height: 32px;" id="exitScreen" onclick="exitFull()">
//                     </div>
//                 </div>
//             </div>
//             <!-- 弹出菜单 默认隐藏-->
//             <div class="ui-dialog" id="dialog">
//                 <div class="ui-dialog-title" id="dialogTitle">
//                     图层列表 <a href="javascript:hideDialog()" class="ui-dialog-closebutton"></a>
//                   <a href="javascript:void(0)">
//                         <span id="fix" class="glyphicon glyphicon-pushpin" style="left: 40px;"></span>
//                     </a>
//                 </div>
//                 <ul id="overlayer">
//                     <!--动态生成数据-->
//                 </ul>
//             </div>
//             <!-- 设置图层查询列表-->
//             <div id="search_layer" class="search_layer">
//               <div class="layer_title" >
//                 图层列表
//                 <span id="icon_list" class="glyphicon glyphicon-align-justify" ></span>
//               </div>
//               <!--todo 此处填写动态加载的数据-->
//               <div id="for_data">
//                 <!--<div class="layer_item">-->
//                   <!--<p>name</p>-->
//                   <!--<p>position</p>-->
//                 <!--</div>-->
//               </div>
//             </div>
//             <!--设置切换后的图层查询列表-->
//             <div id="after_search_layer" class="search_layer">
//             </div>
//         </div>
//     `
//     $("#main_content").html(html);
// }
//
// //todo 页面一加载就显示三维地图
// $().ready(load3D());



