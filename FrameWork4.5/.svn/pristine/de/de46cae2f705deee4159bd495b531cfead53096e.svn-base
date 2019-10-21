using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Steema.TeeChart;
using Steema.TeeChart.Styles;
using Steema.TeeChart.Web;
using Steema.TeeChart.Drawing;

namespace Sigbit.Framework.TiiChart
{
    /// <summary>
    /// 圆饼图类
    /// </summary>
    public class TiiChartPie:TiiChartBase
    {
        private TiiLimit _limit = new TiiLimit();
        /// <summary>
        /// 图表限制
        /// </summary>
        public TiiLimit Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TiiChartPie()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webchart">图表控件</param>
        public TiiChartPie(WebChart webchart)
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
                Chart chart = WebChartControl.Chart;
                Pie pie = new Pie();
                chart.Series.Add(pie);

                //==========1.应用风格==========
                ApplyStyle();

                //==========2.设置标题==========
                chart.Header.Text = Title.Text;

                //==========3.设置3D效果==========
                chart.Aspect.View3D = View3D.Is3D;
                chart.Aspect.Chart3DPercent = View3D.Percent3D;

                //==========4.设置标记显示内容=========
                switch(Mark.MarkDisplayContent)
                {
                    case TiiMarkDisplayContent.Name:
                        pie.Marks.Style = MarksStyles.Label;
                        break;
                    case TiiMarkDisplayContent.Percent:
                        pie.Marks.Style = MarksStyles.Percent;
                        break;
                    case TiiMarkDisplayContent.Value:
                        pie.Marks.Style = MarksStyles.Value;
                        break;
                    case TiiMarkDisplayContent.NamePercent:
                        pie.Marks.Style = MarksStyles.LabelPercent;
                        break;
                    case TiiMarkDisplayContent.NameValue:
                        pie.Marks.Style = MarksStyles.LabelValue;
                        break;
                }
                pie.Marks.Visible = Mark.IsShowMark;

                //==========5.设置圆饼图样式=========
                pie.Circled = true;
                pie.BevelPercent = 10;
                pie.EdgeStyle = EdgeStyles.Curved;
                pie.Pen.Visible = false;

                //==========6.设置是否启用限制=========
                if (Limit.IsLimit)
                {
                    pie.OtherSlice.Style = PieOtherStyles.BelowPercent;
                    pie.OtherSlice.Value = Limit.MinPercent;
                    pie.OtherSlice.Text = Limit.OtherName;
                }

                //==========7.绑定数据=========
                DataSet ds = DataSource.TiiChart__GetDataSet();
                if (ds != null)
                {
                    pie.DataSource = ds.Tables[0];
                    //==========7.1.绑定名称字段=========
                    if (DataSource.NameField == "")
                    {
                        pie.LabelMember = ds.Tables[0].Columns[0].ToString();
                    }
                    else
                    {
                        pie.LabelMember = ds.Tables[0].Columns[DataSource.NameField].ToString();
                    }
                    //==========7.2.绑定数值字段=========
                    if (DataSource.ValueFieldList.Count <= 0)
                    {
                        pie.YValues.DataMember = ds.Tables[0].Columns[1].ToString();
                    }
                    else
                    {
                        for(int i=0;i<DataSource.ValueFieldList.Count;i++)
                        {
                            pie.YValues.DataMember = ds.Tables[0].Columns[DataSource.ValueFieldList[i].ToString()].ToString();
                        }
                    }
                }
            }
        }
    }
}
