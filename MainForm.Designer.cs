using System.Drawing;
using System.Windows.Forms;
using YouthMeadowGeneralStore.Utilities;

namespace YouthMeadowGeneralStore
{
    partial class MainForm
    {
        private WrappedDisplayList displayList;
        private Button btnPurchase;
        private Button btnWarehouse;
        private Button btnShelf;
        private Button btnStartBusiness;
        private Label lblMoney;
        private TableLayoutPanel rootLayout;
        private FlowLayoutPanel actionPanel;
        private Panel headerPanel;

        private void InitializeComponent()
        {
            rootLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            lblMoney = new Label();
            actionPanel = new FlowLayoutPanel();
            btnPurchase = new Button();
            btnWarehouse = new Button();
            btnShelf = new Button();
            btnStartBusiness = new Button();
            displayList = new WrappedDisplayList();
            rootLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            actionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(headerPanel, 0, 0);
            rootLayout.Controls.Add(actionPanel, 0, 1);
            rootLayout.Controls.Add(displayList, 0, 2);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.RowCount = 3;
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rootLayout.Padding = new Padding(12);
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(lblMoney);
            headerPanel.Dock = DockStyle.Fill;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMoney.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 134);
            lblMoney.Location = new Point(1090, 16);
            lblMoney.Text = "当前金钱：1000";
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnPurchase);
            actionPanel.Controls.Add(btnWarehouse);
            actionPanel.Controls.Add(btnShelf);
            actionPanel.Controls.Add(btnStartBusiness);
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.FlowDirection = FlowDirection.LeftToRight;
            actionPanel.Padding = new Padding(0, 6, 0, 0);
            // 
            // btnPurchase
            // 
            btnPurchase.Size = new Size(120, 40);
            btnPurchase.Text = "进货列表";
            btnPurchase.UseVisualStyleBackColor = true;
            // 
            // btnWarehouse
            // 
            btnWarehouse.Size = new Size(120, 40);
            btnWarehouse.Text = "仓库列表";
            btnWarehouse.UseVisualStyleBackColor = true;
            // 
            // btnShelf
            // 
            btnShelf.Size = new Size(120, 40);
            btnShelf.Text = "货架列表";
            btnShelf.UseVisualStyleBackColor = true;
            // 
            // btnStartBusiness
            // 
            btnStartBusiness.Size = new Size(120, 40);
            btnStartBusiness.Text = "开始营业";
            btnStartBusiness.UseVisualStyleBackColor = true;
            // 
            // displayList
            // 
            displayList.Dock = DockStyle.Fill;
            displayList.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 1200);
            Controls.Add(rootLayout);
            MinimumSize = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "青春田野小卖部";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            actionPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
