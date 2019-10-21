using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class module_WebPageControl : System.Web.UI.UserControl
{
    public delegate void PageIndexChangedHandler(int nNewPageIndex);
    public PageIndexChangedHandler PageIndexChanged;

    private DataGrid _dataGridControl;
    public DataGrid DataGridControl
    {
        get { return _dataGridControl; }
        set { _dataGridControl = value; }
    }

    public void ShowPageInfo()
    {
        if (_dataGridControl == null)
            return;

        //=========== 1. 当前页 ===========
        int nCurrentPageIndex;
        nCurrentPageIndex = _dataGridControl.CurrentPageIndex;
        lblCurrentIndex.Text = Convert.ToString(nCurrentPageIndex + 1);

        //======== 2. 转到页 ==============
        edtGoPage.Text = lblCurrentIndex.Text;

        //========= 3. 每页行数 ==========
        int nPageSize;
        nPageSize = _dataGridControl.PageSize;
        edtPageSize.Text = nPageSize.ToString();

        //========== 4. 记录总数 ===========
        if (_dataGridControl.DataSource != null)
        {
            int nRecordCount;
            nRecordCount = ((DataSet)_dataGridControl.DataSource).Tables[0].Rows.Count;
            lblRecordCount.Text = nRecordCount.ToString();
        }

        //========= 5. 页数 =============
        int nPageCount;
        nPageCount = _dataGridControl.PageCount;
        lblPageCount.Text = nPageCount.ToString();

        //============== 6. 翻页控制 ===========
        if (lblCurrentIndex.Text == "1")
        {
            btnFirstPage.Enabled = false;
            btnPrevPage.Enabled = false;
        }
        else
        {
            btnFirstPage.Enabled = true;
            btnPrevPage.Enabled = true;
        }

        if (lblCurrentIndex.Text == lblPageCount.Text)
        {
            btnLastPage.Enabled = false;
            btnNextPage.Enabled = false;
        }
        else
        {
            btnLastPage.Enabled = true;
            btnNextPage.Enabled = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFirstPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = 0;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindDataGrid(nNowPageIndex);
    }

    protected void btnPrevPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblCurrentIndex.Text) - 1;
        if (nNowPageIndex > 0 && Int32.Parse(lblPageCount.Text) > 1)
            nNowPageIndex--;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindDataGrid(nNowPageIndex);
    }

    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblCurrentIndex.Text) - 1;
        if (Int32.Parse(lblPageCount.Text) - 1 > nNowPageIndex)
            nNowPageIndex++;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindDataGrid(nNowPageIndex);
    }

    protected void btnLastPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblPageCount.Text) - 1;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindDataGrid(nNowPageIndex);
    }

    private void BindDataGrid(int nNowPageIndex)
    {
        if (DataGridControl == null)
            return;

        int nPageSize;
        try
        {
            nPageSize = Convert.ToInt32(edtPageSize.Text);
        }
        catch
        {
            nPageSize = 10;
        }
        if (nPageSize <= 0)
            nPageSize = 10;
        _dataGridControl.PageSize = nPageSize;

        _dataGridControl.CurrentPageIndex = nNowPageIndex;
        PageIndexChanged(nNowPageIndex);

        ShowPageInfo();
    }

    protected void btnGoPage_Click(object sender, EventArgs e)
    {
        if (_dataGridControl == null)
            return;

        int nPageIndex;
        try
        {
            nPageIndex = Convert.ToInt32(edtGoPage.Text) - 1;
        }
        catch
        {
            nPageIndex = 0;
        }

        if (nPageIndex < 0)
            nPageIndex = 0;

        if (nPageIndex >= Convert.ToInt32(lblPageCount.Text))
            nPageIndex = Convert.ToInt32(lblPageCount.Text) - 1;

        lblCurrentIndex.Text = (nPageIndex + 1).ToString();
        BindDataGrid(nPageIndex);
        edtGoPage.Text = (nPageIndex + 1).ToString();
    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        if (_dataGridControl.AllowPaging)
        {
            btnShowAll.Text = "分页";
            btnShowAll.ToolTip = "分页显示";
            tdGoPage.Visible = false;
            tdPageCount.Visible = false;
            _dataGridControl.AllowPaging = false;
            btnFirstPage.Enabled = false;
            btnPrevPage.Enabled = false;
            btnLastPage.Enabled = false;
            btnNextPage.Enabled = false;
        }
        else
        {
            btnShowAll.Text = "全部";
            btnShowAll.ToolTip = "不分页显示，将符合条件的记录全部列出";
            tdPageCount.Visible = true;
            tdGoPage.Visible = true;
            _dataGridControl.AllowPaging = true;
            btnFirstPage.Enabled = true;
            btnPrevPage.Enabled = true;
            btnLastPage.Enabled = true;
            btnNextPage.Enabled = true;
        }
        lblCurrentIndex.Text = "1";
        BindDataGrid(0);
    }
}
