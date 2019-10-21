//================= ToolsControl 这是一个地图工具栏控件 =======================
//**** 可以显示，添加一些按钮，和使用当前控件的一些全局函数 ******************* 
//=============================================================================

//============ 1.0 创建这个控件 ================
function ToolsControl() {}
ToolsControl.prototype = new GControl();
 
//============ 2.0 给这个控件添加元素 ================
//============ 并将其返回，添加到地图容器中 ==========
ToolsControl.prototype.initialize = function(map) {
            
    //============ 2.1 给这个控件添加一个容器 ================
    this.container = document.createElement("div");
    this.container.style.backgroundColor = "yellow";
    this.container.style.padding = "2px";    
    this.container.innerHTML = "请点击您要设置的第一点！";    
    this.container.style.marginTop = "25px";         //距上
    this.container.style.color = "#C60501";          //字体颜色
    this.container.style.marginLeft = "80px";      //距左  
    this.container.style.fontSize = "14";            //字体大小
          
    //============ 2.2 给这个控件添加一些属性 ================   
                    
	//============ 2.2 绘制围栏信息 ==============
    this.polygonControl = new DrawingPolygonControl();
    this.drawing = this.polygonControl.initiDrawing(GlobalMap,null,true); 
    this.polygonControl.parentControl = this;
    this.polygonControl.parentVessel = this.container;
    this.setButtonStyle_(this.drawing);
    //container.appendChild(drawing);  
                                     
    //debugger;
    //============= 2.3 清除围栏信息 ==============        
    this.clear = this.polygonControl.initiClear(GlobalMap,null); 
    this.setButtonStyle_(this.clear);
    //container.appendChild(clear);
    
    //=============== 获取地点信息 ================ 
    this.findDescription = new FindDescriptionControl();
    this.findImgDiv = this.findDescription.initi(map); 
    this.setButtonStyle_(this.findImgDiv);
    //container.appendChild(findImgDiv);       

	//============= 添加到地图上 ==============
    GlobalMap.getContainer().appendChild(this.container); 
    return this.container;
}

//============= 3.0 默认情况下，该控件将在地图的右上角显示，边距为 4 像素 =========== 
ToolsControl.prototype.getDefaultPosition = function() {
    return new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(4, 4));
}

//============= 4.0 给这个控件上的子按钮添加 CSS =========== 
ToolsControl.prototype.setButtonStyle_ = function(button) {
    button.style.textAlign = "center";
    button.style.width = "20px";
    button.style.height = "21px";
    button.style.padding = "2px";
    button.style.marginBottom = "3px";
    button.style.textAlign = "center"; 
    button.style.cursor = "pointer"; //pointer  crosshair
}
 



