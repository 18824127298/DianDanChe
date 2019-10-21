function getRef(id) {
return document.all[id];
}
function getSty(id) {
return getRef(id).style;
} 
// Hide timeout.
var popTimer = 0;
// Array showing highlighted menu items.
var litNow = new Array();
function popOver(menuNum, itemNum) {
clearTimeout(popTimer);
hideAllBut(menuNum);
litNow = getTree(menuNum, itemNum);
changeCol(litNow, true);
targetNum = menu[menuNum][itemNum].target;
if (targetNum > 0) {
thisX = parseInt(menu[menuNum][0].ref.left) + parseInt(menu[menuNum][itemNum].ref.left);
thisY = parseInt(menu[menuNum][0].ref.top) + parseInt(menu[menuNum][itemNum].ref.top);
with (menu[targetNum][0].ref) {
left = parseInt(thisX + menu[targetNum][0].x);
top = parseInt(thisY + menu[targetNum][0].y);
visibility = 'visible';
      }
   }
}
function popOut(menuNum, itemNum) {
if ((menuNum == 0) && !menu[menuNum][itemNum].target)
hideAllBut(0)
else
popTimer = setTimeout('hideAllBut(0)', 500);
}
function getTree(menuNum, itemNum) {

// Array index is the menu number. The contents are null (if that menu is not a parent)
// or the item number in that menu that is an ancestor (to light it up).
itemArray = new Array(menu.length);

while(1) {
itemArray[menuNum] = itemNum;
// If we've reached the top of the hierarchy, return.
if (menuNum == 0) return itemArray;
itemNum = menu[menuNum][0].parentItem;
menuNum = menu[menuNum][0].parentMenu;
   }
}

// Pass an array and a boolean to specify colour change, true = over colour.
function changeCol(changeArray, isOver) {
for (menuCount = 0; menuCount < changeArray.length; menuCount++) {
if (changeArray[menuCount]) {
newCol = isOver ? menu[menuCount][0].overColor : menu[menuCount][0].backColor;
// Change the colours of the div/layer background.
// with (menu[menuCount][changeArray[menuCount]].ref) {
// if (isNS4) bgColor = newCol;
// else backgroundColor = newCol;
//         }
      }
   }
}
function hideAllBut(menuNum) {
var keepMenus = getTree(menuNum, 1);
for (count = 0; count < menu.length; count++)
if (!keepMenus[count])
menu[count][0].ref.visibility = 'hidden';
changeCol(litNow, false);
}

// *** MENU CONSTRUCTION FUNCTIONS ***

function Menu(isVert, popInd, x, y, width, overColor, backColor, borderClass, textClass) {
// True or false - a vertical menu?
this.isVert = isVert;
// The popout indicator used (if any) for this menu.
this.popInd = popInd;
// Position and size settings.
this.x = x;
this.y = y;
this.width = width;
// Colours of menu and items.
this.overColor = overColor;
this.backColor = backColor;
// The stylesheet class used for item borders and the text within items.
this.borderClass = borderClass;
this.textClass = textClass;
// Parent menu and item numbers, indexed later.
this.parentMenu = null;
this.parentItem = null;
// Reference to the object's style properties (set later).
this.ref = null;
}

function Item(text, href, frame, length, spacing, target) {
this.text = text;
this.href = href;
this.frame = frame;
this.length = length;
this.spacing = spacing;
this.target = target;
// Reference to the object's style properties (set later).
this.ref = null;
}

function writeMenus(menuheight) {
var strClass=""
for (currMenu = 0; currMenu < menu.length; currMenu++) with (menu[currMenu][0]) {
// Variable for holding HTML for items and positions of next item.
var str = '', itemX = 0, itemY = 0;
var startleft=window.menuzone.offsetLeft;
var starttop=window.menuzone.offsetTop;
// Remember, items start from 1 in the array (0 is menu object itself, above).
// Also use properties of each item nested in the other with() for construction.
for (currItem = 1; currItem < menu[currMenu].length; currItem++) with (menu[currMenu][currItem]) {
var itemID = 'menu' + currMenu + 'item' + currItem;

// The width and height of the menu item - dependent on orientation!
var w = (isVert ? width : length);
var h = (isVert ? length : width);

// Create a div or layer text string with appropriate styles/properties.
// Thanks to Paul Maden (www.paulmaden.com) for helping debug this in IE4, apparently
// the width must be a miniumum of 3 for it to work in that browser.
if (currMenu==0) {
var strtemp=menu[0][currItem].text;
strClass =strClass+"'"+menu[0][currItem].target+"',";
w=(strtemp.length)*11;//菜单宽度
h=menuheight;
str += '<div id="' + itemID + '" style="position: absolute; left: ' + (startleft+itemX) + '; top: ' + (starttop+itemY) + '; width: ' + w + '; height: ' + h + '; visibility: inherit; ';
}
else
{
var strindexclass="'"+currMenu+"'";
	if (strClass.indexOf(strindexclass)>0)
		{
		str += '<div id="' + itemID + '" style="position: absolute; left: ' + (itemX - 0)  + '; top: ' + itemY + '; width: ' + w + '; height: ' + h + '; visibility: inherit; ';
		}
	else
		{
		str += '<div id="' + itemID + '" style="position: absolute; left: ' + itemX + 2  + '; top: ' + itemY + '; width: ' + w + '; height: ' + h + '; visibility: inherit; ';
		}
}
if (backColor) str += 'background: ' + backColor + '; ';
str += '" ';

if (borderClass) str += 'class="' + borderClass + '" ';

// Add mouseover handlers and finish div/layer.
str += 'onMouseOver="popOver(' + currMenu + ',' + currItem + ')" onMouseOut="popOut(' + currMenu + ',' + currItem + ')">';

// Add contents of item (default: table with link inside).
// In IE/NS6+, add padding if there's a border to emulate NS4's layer padding.
// If a target frame is specified, also add that to the <a> tag.
if (currMenu==0){
str += '<table align="center" class="menu" style="border:0px" width="' + (w-8) + '" border="0" cellspacing="0" cellpadding="' +  "0"  + '"><tr><td nowrap align="center" height="' + (h - 7) + '">';
str +='<div align="center"> ';
if(frame=="_dlg") strtemp='<a class="'+textClass+'" href=javascript:showdlg("'+href+'")>'+text+'</a>';
else strtemp='<a class="' + textClass + '" href="' + href + '"' + (frame ? ' target="' + frame + '">' : '>') + text + '</a>';
//href="showModalDialog('"+href+"')";
str += strtemp
if (currItem!=menu[currMenu].length - 1) str +='      |';//在后补空格
str +='</div> ';

}
else
{
str += '<table align="center" class="menu" width="' + w + '" border="0" cellspacing="0" cellpadding="' +  "0"  + '"><tr><td nowrap align="left" height="' + (h - 7) + '">';
str += '<a class="' + textClass + '" href="' + href + '"' + (frame ? ' target="' + frame + '">' : '>') + text + '</a>';
}


str +='</td>';
if (target > 0) {

// Set target's parents to this menu item.
menu[target][0].parentMenu = currMenu;
menu[target][0].parentItem = currItem;

// Add a popout indicator.
if (popInd) 
{
//str += '<td class="' + textClass + '" align="right">' + popInd ;
str += '<td  align="right"><span style="font-family:Marlett;font-weight:normal">' + 4 ;
str += '</span></td>'
}
}
str += '</tr></table>';

str +=  '</div>';
if (isVert) itemY += length + spacing;
else itemX +=w+16+spacing;//菜单宽度
}

// Insert a div tag to the end of the BODY with menu HTML in place for IE4.

document.body.insertAdjacentHTML('beforeEnd', '<div id="menu' + currMenu + 'div" ' + 'style="position: absolute; visibility: hidden">' + str + '</div>');
ref = getSty('menu' + currMenu + 'div');


for (currItem = 1; currItem < menu[currMenu].length; currItem++) {
itemName = 'menu' + currMenu + 'item' + currItem;
menu[currMenu][currItem].ref = getSty(itemName);
   }
}
with(menu[0][0]) {
ref.left = x;
ref.top = y;
ref.visibility = 'visible';
   }
}

function refresh()	
{

	for (var currMenu = 0; currMenu < menu.length; currMenu++) with (menu[currMenu][0]) {
	var itemX = 0, itemY = 0;
	var startleft=window.menuzone.offsetLeft;
	var starttop=window.menuzone.offsetTop;
	for (currItem = 1; currItem < menu[currMenu].length; currItem++) with (menu[currMenu][currItem]) {
		var itemID = 'menu' + currMenu + 'item' + currItem;


		var w = (isVert ? width : length);
		var h = (isVert ? length : width);

		if (currMenu==0)
		{
			var strtemp=menu[0][currItem].text;
			var strClass =strClass+"'"+menu[0][currItem].target+"',";
			w=(strtemp.length)*13;
			h=20;
			document.all.tags("DIV").item(itemID).style.left=startleft+itemX;
		}
		else
		{
			var strindexclass="'"+currMenu+"'";
			if (strClass.indexOf(strindexclass)>0)
			{
				
				document.all.tags("DIV").item(itemID).style.left=itemX-0;
			}
			else
			{
				document.all.tags("DIV").item(itemID).style.left=itemX+2;
			}
		}

	if (isVert) itemY += length + spacing;
	else itemX += w+20 + spacing;
}

// Insert a div tag to the end of the BODY with menu HTML in place for IE4.


	for (currItem = 1; currItem < menu[currMenu].length; currItem++)
	{
		itemName = 'menu' + currMenu + 'item' + currItem;
		menu[currMenu][currItem].ref = getSty(itemName);
   }
}
	with(menu[0][0])
	{
	ref.left = x;
	ref.top = y;
	ref.visibility = 'visible';
	}
}

	window.onresize=refresh;
