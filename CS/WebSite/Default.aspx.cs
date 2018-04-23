using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            NorthwindDataContext dataContext = new NorthwindDataContext();

             //productsQuery will have a IQueryable<Category> type
            var productsQuery = from category in dataContext.Categories
                                where category.CategoryID == 1
                                join product in dataContext.Products on category equals product.Category
                                select new { Category = category.CategoryName, Product = product.ProductName, Price = product.UnitPrice };

            DevExpress.XtraCharts.Series series = new DevExpress.XtraCharts.Series("Series1", DevExpress.XtraCharts.ViewType.RadarArea);

            series.DataSource = productsQuery;
            series.ArgumentDataMember = "Product";
            series.ValueDataMembers.AddRange(new string[] { "Price" });

            WebChartControl1.Series.Add(series);
            WebChartControl1.DataBind();
            ((DevExpress.XtraCharts.RadarDiagram)WebChartControl1.Diagram).AxisY.Range.MaxValue= 50;
        }

    }

}
