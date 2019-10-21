using System;
using System.Collections.Generic;
using System.Text;

using Steema.TeeChart;
using Steema.TeeChart.Web;
using Steema.TeeChart.Themes;
using System.Drawing;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 图表基类
    /// </summary>
    public abstract class TiiChartBase
    {
        private WebChart _webChartControl=null;
        /// <summary>
        /// 图表控件
        /// </summary>
        public WebChart WebChartControl
        {
            get { return _webChartControl; }
            set { _webChartControl = value; }
        }

        private TiiTitle _title = new TiiTitle();
        /// <summary>
        /// 图表标题
        /// </summary>
        public TiiTitle Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private TiiStyle _style = new TiiStyle();
        /// <summary>
        /// 图表风格
        /// </summary>
        public TiiStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        private TiiView3D _view3D=new TiiView3D();
        /// <summary>
        /// 图表3D设置
        /// </summary>
        public TiiView3D View3D
        {
            get { return _view3D; }
            set { _view3D = value; }
        }

        private TiiAxis _axis = new TiiAxis();
        /// <summary>
        /// 图表坐标
        /// </summary>
        public TiiAxis Axis
        {
            get { return _axis; }
            set { _axis = value; }
        }

        private TiiMark _mark = new TiiMark();
        /// <summary>
        /// 图表标记
        /// </summary>
        public TiiMark Mark
        {
            get { return _mark; }
            set { _mark = value; }
        }
        
        private TiiDataSource _dataSource = new TiiDataSource();
        /// <summary>
        /// 图表数据源
        /// </summary>
        public TiiDataSource DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        /// <summary>
        /// 颜色列表
        /// </summary>
        private Color[] ColorList = null;

        /// <summary>
        /// 应用风格
        /// </summary>
        protected void ApplyStyle()
        {
            if (WebChartControl != null)
            {
                Chart chart = WebChartControl.Chart;
                Theme theme;
                switch (Style.StyleSet)
                {
                    case TiiStyleSet.BlueSky:
                        theme = new BlueSkyTheme(chart);
                        Theme.ApplyChartTheme(theme, chart);
                        ColorList = Theme.MacOSPalette;
                        ColorPalettes.ApplyPalette(chart, ColorList);
                        chart.Legend.Visible = true;
                        chart.Legend.Shadow.Visible = false;
                        chart.Panel.Shadow.Visible = false;
                        break;
                    case TiiStyleSet.DarkSky:
                        theme = new BlackIsBackTheme(chart);
                        Theme.ApplyChartTheme(theme, chart);
                        ColorList = Theme.OnBlackPalette;
                        ColorPalettes.ApplyPalette(chart, ColorList);
                        chart.Legend.Visible = true;
                        chart.Legend.Shadow.Visible = false;
                        chart.Panel.Shadow.Visible = false;
                        break;
                    case TiiStyleSet.Classic:
                        theme = new OperaTheme(chart);
                        Theme.ApplyChartTheme(theme, chart);
                        ColorList = new Color[] { ColorTranslator.FromHtml("#FF0033"), ColorTranslator.FromHtml("#00CCFF"), 
                            ColorTranslator.FromHtml("#FF9900"),ColorTranslator.FromHtml("#00CC00"), ColorTranslator.FromHtml("#FFFF33"), 
                            ColorTranslator.FromHtml("#FF33FF"),ColorTranslator.FromHtml("#66FF33"), ColorTranslator.FromHtml("#996600"), 
                            ColorTranslator.FromHtml("#0066CC"), ColorTranslator.FromHtml("#CCCCCC")};
                        ColorPalettes.ApplyPalette(chart, ColorList);
                        chart.Legend.Visible = true;
                        chart.Legend.Shadow.Visible = false;
                        chart.Panel.Shadow.Visible = false;
                        //背景左和上边框设置
                        chart.Panel.Bevel.ColorOne = Color.Gold;
                        //背景右和下边框设置
                        chart.Panel.Bevel.ColorTwo = Color.Gold;
                        //背景设置
                        chart.Panel.Brush.Gradient.StartColor = Color.Silver;
                        chart.Panel.Brush.Gradient.MiddleColor = Color.WhiteSmoke;
                        chart.Panel.Brush.Gradient.EndColor = Color.SkyBlue;
                        chart.Panel.Brush.Gradient.Angle = 45;
                        break;
                }
            }
        }

        /// <summary>
        /// 获取循环的颜色
        /// </summary>
        /// <param name="index">颜色索引</param>
        /// <returns>颜色</returns>
        protected Color GetColor(int index)
        {
            if (index >= ColorList.Length)
            {
                index=index % ColorList.Length;
            }
            return ColorList[index];
        }

        /// <summary>
        /// 在控件上显示图表
        /// </summary>
        public abstract void DoShowChart();
    }
}
