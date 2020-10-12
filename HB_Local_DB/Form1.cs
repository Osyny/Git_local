﻿using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB_Local_DB
{
    public partial class Form1 : Form
    {
     

        OrderForm curentOrder;
    

        //Singelton
        HomeBugaltery homeBugaltery = HomeBugaltery.getInstance();

        Users curentUser;
        CategoryForm categoryForm;
        FormForPeriod formFromPeriod;

        UserForm fixUser;

        public Form1()
        {
            InitializeComponent();

            curentUser = new Users() { Id = 1, Email = "nina_3@email.com", Name = "НінаНікитюк", Password = "1234", Family_Id = 1 };

            curentOrder = new OrderForm(homeBugaltery);

            updateOrdersGrid();

        }

        private void updateOrdersGrid()
        {
            List<OrdersView> orders = homeBugaltery.ListOrders;

            dataGridViewOrders.Rows.Clear();

            foreach (OrdersView orderView in orders)
            {
                int rowIndex = dataGridViewOrders.Rows.Add(orderView.CategoryName);

                dataGridViewOrders.Rows[rowIndex].Cells[1].Value = orderView.UserName;
                dataGridViewOrders.Rows[rowIndex].Cells[2].Value = orderView.DateOrder;
                dataGridViewOrders.Rows[rowIndex].Cells[3].Value = orderView.Price;
                dataGridViewOrders.Rows[rowIndex].Cells[4].Value = orderView.Description;
                dataGridViewOrders.Rows[rowIndex].Cells[5].Value = "Fix";
                dataGridViewOrders.Rows[rowIndex].Cells[6].Value = "Delete";
                //Tag - id order
                dataGridViewOrders.Rows[rowIndex].Tag = orderView.Id;
            }
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           var row = dataGridViewOrders.Rows[dataGridViewOrders.SelectedCells[0].RowIndex];

            if (e.ColumnIndex == 5)
            {
                int orderId = (int)row.Tag;

                curentOrder.showForm(orderId);
            }
            else if(e.ColumnIndex == 6)
            {
                DialogResult result = MessageBox.Show("Видалити? \nВи впевнені?", "Confirmation", MessageBoxButtons.YesNo);
                if(result == DialogResult.Yes)
                {
                    homeBugaltery.deleteOrder((int)row.Tag);
                }
                if (result == DialogResult.No)
                {
                    return;
                }              
            }
            updateOrdersGrid();
        }

        private void newOrdersMenuItem_Click(object sender, EventArgs e)
        {
            curentOrder.showForm();

            updateOrdersGrid();
        }

        // New Category
        private void categoryMenuItem_Click(object sender, EventArgs e)
        {
            categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
        }

        private void usersMenuItem_Click(object sender, EventArgs e)
        {
            fixUser = new UserForm(curentUser);
            fixUser.ShowDialog();
        }

        private void toolMenuItemCosts_Click(object sender, EventArgs e)
        {
            formFromPeriod = new FormForPeriod(false);
            formFromPeriod.ShowDialog();
        }

        private void toolMenuItemProfit_Click(object sender, EventArgs e)
        {
            formFromPeriod = new FormForPeriod(true);
            formFromPeriod.ShowDialog();
        }
    }
}
