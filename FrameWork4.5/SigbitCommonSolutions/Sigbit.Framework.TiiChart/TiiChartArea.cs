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
    /// 面积图类
    /// </summary>
    public class TiiChartArea : TiiChartBase
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

        private TiiAreaAlignMethod _areaAlignMethod = TiiAreaAlignMethod.Default;
        /// <summary>
        /// 图表排列方式
        /// </summary>
        public TiiAreaAlignMethod AreaAlignMethod
        {
            get { return _areaAlignMethod; }
            set { _areaAlignMethod = value; }
        }

        private int _transparency = 45;
        /// <summary>
        /// 透明度，0~100
        /// </summary>
        public int Transparency
        {
            get { return _transparency; }
            set 
            {
                if (_transparency > 100)
                {
                    value = 100;
                }
                if (_transparency < 0)
                {
                    value = 0;
                }
                _transparency = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TiiChartArea()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webchart">图表控件</param>
        public TiiChartArea(WebChart webchart)
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
                    DoShowChartHorizArea();
                }
                else
                {
                    DoShowChartArea();
                }
            }
        }

        /// <summary>
        /// 在控件上显示垂直面积图
        /// </summary>
        private void DoShowChartArea()
        {
            Chart chart = WebChartControl.Chart;
            Area area = new Area();
            chart.Series.Add(area);

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
                    area.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    area.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    area.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    area.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    area.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            area.Marks.Visible =Mark.IsShowMark;

            //==========6.设置面积图样式=========
            area.Chart.Axes.Bottom.Grid.Visible = false;
            area.Transparency = Transparency;
            area.AreaLines.Color = area.Color;
            area.LinePen.Visible = false;

            //==========7.设置排列方式=========
            if (AreaAlignMethod == TiiAreaAlignMethod.Stacked)
            {
                area.Stacked = CustomStack.Stack;
            }
            
            //==========8.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                area.DataSource = ds.Tables[0];
                //==========8.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    area.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    area.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========8.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    area.YValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    area.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            area.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            area.Title = DataSource.CaptionList[i].ToString();
                            area.Color = GetColor(i);
                            area.Transparency = Transparency;
                        }
                        else
                        {
                            Area areaNew = (Area)area.Clone();
                            chart.Series.Add(areaNew);
                            areaNew.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            areaNew.Title = DataSource.CaptionList[i].ToString();
                            areaNew.Color = GetColor(i);
                            areaNew.AreaLines.Color = areaNew.Color;
                            areaNew.Transparency = Transparency;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 在控件上显示水平面积图
        /// </summary>
        private void DoShowChartHorizArea()
        {
            Chart chart = WebChartControl.Chart;
            HorizArea area = new HorizArea();
            chart.Series.Add(area);

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
                    area.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    area.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    area.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    area.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    area.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            area.Marks.Visible = Mark.IsShowMark;

            //==========6.设置面积图样式=========
            area.Chart.Axes.Left.Grid.Visible = false;
            area.Transparency = Transparency;
            area.AreaLines.Color = area.Color;
            area.LinePen.Visible = false;

            //==========7.设置排列方式=========
            if (AreaAlignMethod == TiiAreaAlignMethod.Stacked)
            {
                area.Stacked = CustomStack.Stack;
            }

            //==========8.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                area.DataSource = ds.Tables[0];
                //==========8.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    area.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    area.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========8.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    area.XValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    area.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            area.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            area.Title = DataSource.CaptionList[i].ToString();
                            area.Color = GetColor(i);
                            area.Transparency = Transparency;
                        }
                        else
                        {
                            HorizArea areaNew = (HorizArea)area.Clone();
                            chart.Series.Add(areaNew);
                            areaNew.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            areaNew.Title = DataSource.CaptionList[i].ToString();
                            areaNew.Color = GetColor(i);
                            areaNew.AreaLines.Color = areaNew.Color;
                            areaNew.Transparency = Transparency;
                        }
                    }
                }
            }
        }
    }
}
