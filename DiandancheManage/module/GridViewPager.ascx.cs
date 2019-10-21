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


public partial class module_WebUserControl : System.Web.UI.UserControl
{
    public delegate void PageIndexChangedHandler(int nNewPageIndex);
    public PageIndexChangedHandler PageIndexChanged;

    private GridView _gridViewControl;
    public GridView GridViewControl
    {
        get { return _gridViewControl; }
        set { _gridViewControl = value; }
    }

    public void ShowPageInfo()
    {
        if (_gridViewControl == null)
            return;

        //=========== 1. 当前页 ===========
        int nCurrentPageIndex;
        nCurrentPageIndex = _gridViewControl.PageIndex;
        lblCurrentIndex.Text = Convert.ToString(nCurrentPageIndex + 1);

        //======== 2. 转到页 ==============
        edtGoPage.Text = lblCurrentIndex.Text;

        //========= 3. 每页行数 ==========
        int nPageSize;
        nPageSize = _gridViewControl.PageSize;
        edtPageSize.Text = nPageSize.ToString();

        //========== 4. 记录总数 ===========
        int nRecordCount = 0;
        if (_gridViewControl.DataSource != null)
        {
            nRecordCount = ((DataSet)_gridViewControl.DataSource).Tables[0].Rows.Count;
            lblRecordCount.Text = nRecordCount.ToString();
        }

        //========= 5. 页数 =============
        int nPageCount;
        nPageCount = _gridViewControl.PageCount;
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

        //=========== 7. 对于记录数为0的判断 ==========
        if (nRecordCount == 0)
        {
            btnLastPage.Enabled = false;
            btnNextPage.Enabled = false;
            lblCurrentIndex.Text = "0";
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
        BindGridView(nNowPageIndex);
    }

    protected void btnPrevPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblCurrentIndex.Text) - 1;
        if (nNowPageIndex > 0 && Int32.Parse(lblPageCount.Text) > 1)
            nNowPageIndex--;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindGridView(nNowPageIndex);
    }

    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblCurrentIndex.Text) - 1;
        if (Int32.Parse(lblPageCount.Text) - 1 > nNowPageIndex)
            nNowPageIndex++;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindGridView(nNowPageIndex);
    }

    protected void btnLastPage_Click(object sender, EventArgs e)
    {
        int nNowPageIndex;
        nNowPageIndex = Convert.ToInt32(lblPageCount.Text) - 1;
        lblCurrentIndex.Text = (nNowPageIndex + 1).ToString();
        BindGridView(nNowPageIndex);
    }

    private void BindGridView(int nNowPageIndex)
    {
        if (GridViewControl == null)
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
        _gridViewControl.PageSize = nPageSize;

        if (nNowPageIndex < 0)
            nNowPageIndex = 0;
        _gridViewControl.PageIndex = nNowPageIndex;
        PageIndexChanged(nNowPageIndex);

        ShowPageInfo();
    }

    protected void btnGoPage_Click(object sender, EventArgs e)
    {
        if (_gridViewControl == null)
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
        BindGridView(nPageIndex);
        edtGoPage.Text = (nPageIndex + 1).ToString();
    }

    public void RefreshGridView()
    {
        if (btnShowAll.Text == "分页")
        {
            btnShowAll.Text = "全部";
            btnShowAll_Click(null, null);
        }
        else
            btnGoPage_Click(null, null);
    }

    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        if (btnShowAll.Text == "全部")
        {
            btnShowAll.Text = "分页";
            btnShowAll.ToolTip = "分页显示";
            tdGoPage.Visible = false;
            tdPageCount.Visible = false;
            _gridViewControl.AllowPaging = false;
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
            _gridViewControl.AllowPaging = true;
            btnFirstPage.Enabled = true;
            btnPrevPage.Enabled = true;
            btnLastPage.Enabled = true;
            btnNextPage.Enabled = true;
        }
        lblCurrentIndex.Text = "1";
        BindGridView(0);
    }
}
