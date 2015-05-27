using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyAtHome.Winform.FactoryPortal.WashOrderService;

namespace LazyAtHome.Winform.FactoryPortal.UI.WareHouse
{
    public partial class frmQFOrderNumInput : Form
    {
        //业务逻辑
        private Business.WashOrder business = null;

        //订单号
        public string OrderNumber = string.Empty;

        //订单实体
        Order_OrderDC entity = null;


        public frmQFOrderNumInput()
        {
            InitializeComponent();

            if (business == null)
            {
                business = new Business.WashOrder();
            }
        }

        /// <summary>
        /// 调用 全峰下单 和 订单号写入订单系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtQFOrderNum.Text))
            {
                MessageBox.Show("请扫描全峰快递单上的快递单号");
                return;
            }

            if (DataSave())
            {
                MessageBox.Show("全峰快递单号写入成功！");
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <returns></returns>
        private bool DataSave()
        {
            bool rtn = true;

            //获取订单信息
            entity = business.GetOrder(OrderNumber);
            if (entity != null)
            {

                //收件人信息
                Order_ConsigneeAddressDC s = null;
                //获取收件人信息
                //s = entity.ConsigneeAddressList.FirstOrDefault(p => p.Type == 2);
                s = entity.SendAddress;

                string Code = System.DateTime.Now.ToString("HHmmss");

                //物品信息
                int count = entity.ProductList == null ? 0 : entity.ProductList.Count();
                Dictionary<string, int> iItemList = new Dictionary<string, int>();
                iItemList.Add("衣物", count);

                if (entity != null)
                {
                    //判断订单系统是否已经写入物流单号
                    if (entity.ExpressList == null || entity.ExpressList.Count(p => p.Type == 2) == 0)
                    {

                        //没有的话
                        //把物流单号写入订单系统
                        if (business.OrderExpressADD(entity.ID, entity.OrderNumber, entity.OrderNumber + Code, Common.ConstConfig.currOperator.ID))
                        {
                            //成功 调用全峰下单
                            if (!business.QFExpressOrderCreate(entity.OrderNumber + Code, this.txtQFOrderNum.Text, s.Consignee, s.Phone, s.Mpno, s.ProvinceName, s.CityName, s.DistrictName + s.Address, iItemList))
                            {
                                //失败 报错
                                MessageBox.Show(business.LastError, "错误");
                                rtn = false;
                            }
                        }
                        else
                        {
                            //失败 报错
                            MessageBox.Show(business.LastError, "错误");
                            rtn = false;
                        }

                    }
                    else
                    {
                        //已经有的话 
                        //把物流单号写入订单系统
                        if (business.OrderExpressUpdate(entity.ID, entity.OrderNumber, entity.OrderNumber + Code, Common.ConstConfig.currOperator.ID))
                        {

                            //先拿到已有的物流单号
                            Order_ExpressDC express = entity.ExpressList.FirstOrDefault(p => p.Type == 2);
                            //判断是否已经写入物流单号
                            if (!string.IsNullOrEmpty(express.RelationID))
                            {
                                //有写入过 先撤销全峰的订单
                                if (!business.QFExpressOrderCancel(express.RelationID))
                                {
                                    //失败 报错
                                    MessageBox.Show(business.LastError, "错误");
                                    rtn = false;
                                }

                            }

                            //全峰下单
                            if (!business.QFExpressOrderCreate(entity.OrderNumber + Code, this.txtQFOrderNum.Text, s.Consignee, s.Phone, s.Mpno, s.ProvinceName, s.CityName, s.DistrictName + s.Address, iItemList))
                            {
                                //失败 报错
                                MessageBox.Show(business.LastError, "错误");
                                rtn = false;
                            }
                        }
                        else
                        {
                            //失败 报错
                            MessageBox.Show(business.LastError, "错误");
                            rtn = false;
                        }

                    }
                }
                else
                {
                    MessageBox.Show(business.LastError, "错误");
                    rtn = false;
                }
            }
            else
            {
                MessageBox.Show(business.LastError, "错误");
                rtn = false;
            }
            return rtn;
            
        }
    }
}
