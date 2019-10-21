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
    /// 线形图类
    /// </summary>
    public class TiiChartLine : TiiChartBase
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public TiiChartLine()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webchart">图表控件</param>
        public TiiChartLine(WebChart webchart)
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
                    DoShowChartHorizLine();
                }
                else
                {
                    DoShowChartLine();
                }
            }
        }

        /// <summary>
        /// 在控件上显示垂直线形图
        /// </summary>
        private void DoShowChartLine()
        {
            Chart chart = WebChartControl.Chart;
            Line line = new Line();
            chart.Series.Add(line);

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
                    line.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    line.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    line.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    line.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    line.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            line.Marks.Visible = Mark.IsShowMark;

            //==========6.设置线形图样式=========
            line.Chart.Axes.Bottom.Grid.Visible = false;
            line.Pointer.Visible = true;
            line.Pointer.Style = PointerStyles.Circle;
            line.Pointer.VertSize = 2;
            line.Pointer.HorizSize = 2;
            
            //==========7.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                line.DataSource = ds.Tables[0];
                //==========7.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    line.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    line.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========7.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    line.YValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    line.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            line.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            line.Title = DataSource.CaptionList[i].ToString();
                            line.Color = GetColor(i);
                            line.Pointer.Color = GetColor(i);
                        }
                        else
                        {
                            Line lineNew = (Line)line.Clone();
                            chart.Series.Add(lineNew);
                            lineNew.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            lineNew.Title = DataSource.CaptionList[i].ToString();
                            lineNew.Color = GetColor(i);
                            lineNew.Pointer.Color = GetColor(i);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 在控件上显示水平线形图
        /// </summary>
        private void DoShowChartHorizLine()
        {
            Chart chart = WebChartControl.Chart;
            HorizLine line = new HorizLine();
            chart.Series.Add(line);

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
                    line.Marks.Style = MarksStyles.Label;
                    break;
                case TiiMarkDisplayContent.Percent:
                    line.Marks.Style = MarksStyles.Percent;
                    break;
                case TiiMarkDisplayContent.Value:
                    line.Marks.Style = MarksStyles.Value;
                    break;
                case TiiMarkDisplayContent.NamePercent:
                    line.Marks.Style = MarksStyles.LabelPercent;
                    break;
                case TiiMarkDisplayContent.NameValue:
                    line.Marks.Style = MarksStyles.LabelValue;
                    break;
            }
            line.Marks.Visible = Mark.IsShowMark;

            //==========6.设置线形图样式=========
            line.Chart.Axes.Left.Grid.Visible = false;
            line.Pointer.Visible = true;
            line.Pointer.Style = PointerStyles.Circle;
            line.Pointer.VertSize = 2;
            line.Pointer.HorizSize = 2;

            //==========7.绑定数据=========
            DataSet ds = DataSource.TiiChart__GetDataSet();
            if (ds != null)
            {
                line.DataSource = ds.Tables[0];
                //==========7.1.绑定名称字段=========
                if (DataSource.NameField == "")
                {
                    line.LabelMember = ds.Tables[0].Columns[0].ToString();
                }
                else
                {
                    line.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                }
                //==========7.2.绑定数值字段=========
                if (DataSource.ValueFieldList.Count <= 0)
                {
                    line.XValues.DataMember = ds.Tables[0].Columns[1].ToString();
                }
                else if (DataSource.ValueFieldList.Count == 1)
                {
                    line.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[0].ToString()].ToString();
                }
                else
                {
                    for (int i = 0; i < DataSource.ValueFieldList.Count; i++)
                    {
                        if (i == 0)
                        {
                            line.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            line.Title = DataSource.CaptionList[i].ToString();
                            line.Color = GetColor(i);
                            line.Pointer.Color = GetColor(i);
                        }
                        else
                        {
                            HorizLine lineNew = (HorizLine)line.Clone();
                            chart.Series.Add(lineNew);
                            lineNew.XValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                            lineNew.Title = DataSource.CaptionList[i].ToString();
                            lineNew.Color = GetColor(i);
                            lineNew.Pointer.Color = GetColor(i);
                        }
                    }
                }
            }
        }
    }
}
