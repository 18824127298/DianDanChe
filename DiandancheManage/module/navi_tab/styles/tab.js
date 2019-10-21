var TabScroll;

function TabScrollLeft()
{
    var DivNaviTab=document.getElementById("DivNaviTab");
    DivNaviTab.scrollLeft=DivNaviTab.scrollLeft-5;
}

function StartScrollLeft()
{
    TabScroll=setInterval(TabScrollLeft,10);
}

function TabScrollRight()
{
    var DivNaviTab=document.getElementById("DivNaviTab");
   DivNaviTab.scrollLeft=DivNaviTab.scrollLeft+5;
}

function StartScrollRight()
{
    TabScroll=setInterval(TabScrollRight,10);
}

function EndTabScroll()
{
    clearInterval(TabScroll);
}

function ShowTabScroll(index)
{
    var DivNaviTab=document.getElementById("DivNaviTab");
    var TableNaviTab=document.getElementById("TableNaviTab");
    var TDTabLeft=document.getElementById("TDTabLeft");
    var TDTabRight=document.getElementById("TDTabRight");
    if(TableNaviTab.scrollWidth<DivNaviTab.scrollWidth)
    {
        TDTabLeft.style.display="none";
        TDTabRight.style.display="none";
    }
    else
    {
        TDTabLeft.style.display="inline";
        TDTabRight.style.display="inline";
        if(index!=null)
        {
            var nLeft=TableNaviTab.rows[0].cells[index].offsetLeft+TableNaviTab.rows[0].cells[index].offsetWidth;
            var nRight=TableNaviTab.rows[0].cells[index].offsetLeft+2*TableNaviTab.rows[0].cells[index].offsetWidth;
            var nWidth=DivNaviTab.offsetWidth;
            if(nRight-nLeft<0||nRight>nWidth)
            {
                if(nLeft>nWidth)
                {
                    DivNaviTab.scrollLeft+=nLeft;
                }
                else if(nRight>nWidth)
                {
                    DivNaviTab.scrollLeft=nRight-nWidth;
                }
            }
        }
    }
}