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
    var container = document.createElement("div");
    container.style.backgroundColor = "#B5D29C";
    container.style.padding = "2px";    
    container.style.marginTop = "30px";
          
    //============ 2.2 给这个控件添加一些属性 ================ 
    this.polygonControl = new DrawingPolygonControl();   
    
//	//============ 2.2 绘制围栏信息 ==============   
//    var drawing = this.polygonControl.initiDrawing(GlobalMap,null); 
//    this.setButtonStyle_(drawing);
//    container.appendChild(drawing);  

//    //============= 2.3 清除围栏信息 ==============        
//    var clear = this.polygonControl.initiClear(GlobalMap,null); 
//    this.setButtonStyle_(clear);
//    container.appendChild(clear);
                    
    //============= 2.4 返回原点 ==============        
    var return_ = this.polygonControl.initiReturn(GlobalMap,null); 
    this.setButtonStyle_(return_);
    container.appendChild(return_);
                                
//    //=============== 获取地点信息 ================ 
//    this.findDescription = new FindDescriptionControl();
//    var findImgDiv = this.findDescription.initi(map); 
//    this.setButtonStyle_(findImgDiv);
//    container.appendChild(findImgDiv);       

	//============= 添加到地图上 ==============
    GlobalMap.getContainer().appendChild(container); 
    return container;
}

//============= 3.0 默认情况下，该控件将在地图的右上角显示，边距为 4 像素 =========== 
ToolsControl.prototype.getDefaultPosition = function() {
    return new GControlPosition(G_ANCHOR_TOP_RIGHT, new GSize(4, 4));
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
 



