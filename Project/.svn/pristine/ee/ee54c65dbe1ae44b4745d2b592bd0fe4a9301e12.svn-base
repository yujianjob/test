using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;

namespace LazyAtHome.Winform.FactoryPortal.UI.Test
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        TfrxReportClass report;
        FrxDataTable datatable;
        FrxDataView dataview;
        FrxDataSet dataset;
        FrxArrayList arraylist;

        private System.Random rnd;


        //
        // Fills the demonstration dataset with sample data
        //
        private void FillTableWithSampleData(FrxDataTable datatable)
        {
            datatable.Columns.Add("id", typeof(int));
            datatable.Columns.Add("name", typeof(string));
            datatable.Columns.Add("onemorename", typeof(string));
            // Add ten rows
            for (int id = 1; id <= 10; id++)
            {
                datatable.Rows.Add(
                    new object[] { id, string.Format("customer {0}", id), string.Format("address {0}", 10 - id) }
                    );
            }
            datatable.AcceptChanges();
        }

        public Form1()
        {
            InitializeComponent();


            rnd = new Random();

            // Create report object
            report = new TfrxReportClass();

            // This object must appear in designer
            arraylist = new FrxArrayList();

            // Create the FR compatible DataTable object
            datatable = new FrxDataTable("DataTableDemo");
            FillTableWithSampleData(datatable);

            // Create the FR compatible DataView object
            dataview = new FrxDataView(datatable, "DataViewDemo");
            dataview.Sort = "onemorename ASC";

            // We need following to make ongoing the report windows modal
            //report.MainWindowHandle = (int)this.Handle;

            // Load demmonstration report from file
            report.LoadReportFromFile("..//..//.NET Data Demo.fr3");

            report.ClearDatasets();
            // Asiign datasets to report one more time
            // beacuse theLoadReport... family functions breaks links between report and dataset
            datatable.AssignToReport(true, report);
            dataview.AssignToReport(true, report);

            // Assigns DataTable to DataBand
            datatable.AssignToDataBand("MasterData1", report);
            // Assigns DataView to DataBand
            dataview.AssignToDataBand("MasterData2", report);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TfrxReportClass report = new TfrxReportClass();//为报表指定模板文件

            //report.LoadReportFromFile(@"D:\.NET Data Demo.fr3");
            //report.PrepareReport(true);
            //report.LoadReportFromFile(@"D:\Picture.fr3");

            //TfrxDispatchableComponent c= report.FindObject("Memo29");

            //(c as IfrxCustomMemoView).Text = "This text set by application";

            //report.PreviewOptions.AllowEdit = false;
            

            //report.ShowReport();

        }





        private void button2_Click(object sender, EventArgs e)
        {
            report.DesignReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            report.ShowReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Change buttons state
            button3.Enabled = false;
            button4.Enabled = true;

            // Create DataSet and load XML
            dataset = new FrxDataSet();
            dataset.ReadXml(@"..\..\DemoDataSet.xml");

            // Find table in DataSet
            DataTable table = dataset.Tables["Product"];

            // Fill table with random data
            for (int id = 1; id <= 10; id++)
            {
                table.Rows.Add(
                    new object[] { id, string.Format("product {0}", id * 3 / 2), rnd.Next(), 10 - id }
                    );
            }
            table.AcceptChanges();

            // Make DataSet available in report
            dataset.BindToReport(report);


            dataset.BindTableToBand("Product", report, "MasterData3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Change buttons state
            button3.Enabled = true;
            button4.Enabled = false;

            dataset.BindTableToBand(null, report, "MasterData3");
            dataset.UnbindFromReport(report);

            System.GC.Collect();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            report.PrintReport();
        }

    }
}
