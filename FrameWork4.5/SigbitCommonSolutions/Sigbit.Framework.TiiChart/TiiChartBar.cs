using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Steema.TeeChart;
using Steema.TeeChart.Styles;
using Steema.TeeChart.Web;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 直方图类
    /// </summary>
    public class TiiChartBar : TiiChartBase
    {
        private bool isHorizontal = false;
        /// <summary>
        /// 是否横向排列
        /// </summary>
        public bool IsHorizontal
        {
            get { return isHorizontal; }
            set { isHorizontal = value; }
        }

        private TiiBarAlignMethod _barAlignMethod = TiiBarAlignMethod.Side;
        /// <summary>
        /// 图表排列方式
        /// </summary>
        public TiiBarAlignMethod BarAlignMethod
        {
            get { return _barAlignMethod; }
            set { _barAlignMethod = value; }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        public TiiChartBar()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webchart">图表控件</param>
        public TiiChartBar(WebChart webchart)
        {
            WebChartControl = webchart;
        }

        /// <summary>
        /// 在控件上显示图表
        /// </summary>
        public override void DoShowChart()
        {
            if (WebChartControl != null)
            {
                if (isHorizontal)
                {
                    DoShowChartHorizBar();
                }
                else
                {
                    DoShowChartBar();
                }
            }
        }

        /// <summary>
        /// 在控件上显示垂直直方图
        /// </summary>
        private void DoShowChartBar()
        {
            Chart chart = WebChartControl.Chart;
            Bar bar = new Bar();
            chart.Series.Add(bar);

            //==========1.应用风格==========
            ApplyStyle();

            //==========2.设置标题==========
            chart.Header.Text = Title.Text;

            //==========3.设置3D效果==========
            chart.Aspect.View3D = View3D.Is3D;
            chart.Aspect.Chart3DPercent = View3D.Percent3D;

            //==========4.设置坐标==========
            if (Axis.IsSetOrigin)
            {
                chart.Axes.Left.AutomaticMinimum = false;
                chart.Axes.Left.Minimum = Axis.OriginValue;
            }
            if (Axis.IsSetMax)
            {
                chart.Axes.Left.AutomaticMaximum = false;
                chart.Axes.Left.Maximum = Axis.MaxValue;
            }

            //==========5.设置标记显示内容=========
            switch (Mark.MarkDisplayContent)
            {
                case TiiMarkDisplayContent.Name:
                    bar.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    bar.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    bar.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    bar.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    bar.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            bar.Marks.Visible = Mark.IsShowMark;

            //==========6.设置直方图样式=========
            bar.ColorEach = true;
            bar.Chart.Axes.Bottom.Grid.Visible = false;
            bar.BarStyle = BarStyles.RectGradient;
            bar.Pen.Visible = false;

            //==========7.设置排列方式=========
            switch (BarAlignMethod)
            {
                case TiiBarAlignMethod.Side:
                    bar.MultiBar = MultiBars.Side;
                    break;
                case TiiBarAlignMethod.Stacked:
                    bar.MultiBar = MultiBars.Stacked;
                    break;
            }

            //==========8.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                bar.DataSource = ds.Tables[0];
                //==========8.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    bar.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    bar.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========8.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    bar.YValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    bar.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            bar.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            bar.Title = DataSource.CaptionList[i].ToString();
                            bar.Color = GetColor(i);
                            bar.Gradient.StartColor = bar.Color;
                            bar.Gradient.EndColor = bar.Color;
                            bar.ColorEach = false;
                        }
                        else
                        {
                            Bar barNew = (Bar)bar.Clone();
                            chart.Series.Add(barNew);
                            barNew.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            barNew.Title = DataSource.CaptionList[i].ToString();
                            barNew.Color = GetColor(i);
                            barNew.Gradient.StartColor = barNew.Color;
                            barNew.Gradient.EndColor = barNew.Color;
                            barNew.Pen.Visible = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 在控件上显示水平直方图
        /// </summary>
        private void DoShowChartHorizBar()
        {
            Chart chart = WebChartControl.Chart;
            HorizBar bar = new HorizBar();
            chart.Series.Add(bar);

            //==========1.应用风格==========
            ApplyStyle();

            //==========2.设置标题==========
            chart.Header.Text = Title.Text;

            //==========3.设置3D效果==========
            chart.Aspect.View3D = View3D.Is3D;
            chart.Aspect.Chart3DPercent = View3D.Percent3D;

            //==========4.设置坐标==========
            if (Axis.IsSetOrigin)
            {
                chart.Axes.Bottom.AutomaticMinimum = false;
                chart.Axes.Bottom.Minimum = Axis.OriginValue;
            }
            if (Axis.IsSetMax)
            {
                chart.Axes.Bottom.AutomaticMaximum = false;
                chart.Axes.Bottom.Maximum = Axis.MaxValue;
            }

            //==========5.设置标记显示内容=========
            switch (Mark.MarkDisplayContent)
            {
                case TiiMarkDisplayContent.Name:
                    bar.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    bar.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    bar.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    bar.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    bar.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            bar.Marks.Visible = Mark.IsShowMark;

            //==========6.设置直方图样式=========
            bar.ColorEach = true;
            bar.Chart.Axes.Left.Grid.Visible = false;
            bar.BarStyle = BarStyles.RectGradient;
            bar.Pen.Visible = false;

            //==========7.设置排列方式=========
            switch (BarAlignMethod)
            {
                case TiiBarAlignMethod.Side:
                    bar.MultiBar = MultiBars.Side;
                    break;
                case TiiBarAlignMethod.Stacked:
                    bar.MultiBar = MultiBars.Stacked;
                    break;
            }

            //==========8.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                bar.DataSource = ds.Tables[0];
                //==========8.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    bar.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    bar.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========8.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    bar.XValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    bar.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            bar.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            bar.Title = DataSource.CaptionList[i].ToString();
                            bar.Color = GetColor(i);
                            bar.Gradient.StartColor = bar.Color;
                            bar.Gradient.EndColor = bar.Color;
                            bar.ColorEach = false;
                        }
                        else
                        {
                            HorizBar barNew = (HorizBar)bar.Clone();
                            chart.Series.Add(barNew);
                            barNew.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            barNew.Title = DataSource.CaptionList[i].ToString();
                            barNew.Color = GetColor(i);
                            barNew.Gradient.StartColor = barNew.Color;
                            barNew.Gradient.EndColor = barNew.Color;
                            barNew.Pen.Visible = false;
                        }
                    }
                }
            }
        }
    }
}
