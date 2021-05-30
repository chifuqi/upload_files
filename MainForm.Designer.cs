
namespace upload_files_winform
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_update = new System.Windows.Forms.Button();
            this.txtBox_log = new System.Windows.Forms.TextBox();
            this.txtBox_Jsession = new System.Windows.Forms.TextBox();
            this.btn_Jsession = new System.Windows.Forms.Button();
            this.txtBox_selected = new System.Windows.Forms.TextBox();
            this.btn_selected = new System.Windows.Forms.Button();
            this.folderBrowserDialog_selected = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(515, 95);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(262, 43);
            this.btn_update.TabIndex = 12;
            this.btn_update.Text = "开始上传";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // txtBox_log
            // 
            this.txtBox_log.Location = new System.Drawing.Point(24, 167);
            this.txtBox_log.Multiline = true;
            this.txtBox_log.Name = "txtBox_log";
            this.txtBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBox_log.Size = new System.Drawing.Size(753, 403);
            this.txtBox_log.TabIndex = 11;
            // 
            // txtBox_Jsession
            // 
            this.txtBox_Jsession.Location = new System.Drawing.Point(170, 51);
            this.txtBox_Jsession.Name = "txtBox_Jsession";
            this.txtBox_Jsession.Size = new System.Drawing.Size(607, 21);
            this.txtBox_Jsession.TabIndex = 10;
            this.txtBox_Jsession.Text = "F14F40A4A5828A488B5E3722988920A1";
            // 
            // btn_Jsession
            // 
            this.btn_Jsession.Location = new System.Drawing.Point(12, 51);
            this.btn_Jsession.Name = "btn_Jsession";
            this.btn_Jsession.Size = new System.Drawing.Size(152, 23);
            this.btn_Jsession.TabIndex = 9;
            this.btn_Jsession.Text = "JSESSIONID";
            this.btn_Jsession.UseVisualStyleBackColor = true;
            // 
            // txtBox_selected
            // 
            this.txtBox_selected.Location = new System.Drawing.Point(170, 12);
            this.txtBox_selected.Name = "txtBox_selected";
            this.txtBox_selected.Size = new System.Drawing.Size(607, 21);
            this.txtBox_selected.TabIndex = 8;
            // 
            // btn_selected
            // 
            this.btn_selected.Location = new System.Drawing.Point(12, 12);
            this.btn_selected.Name = "btn_selected";
            this.btn_selected.Size = new System.Drawing.Size(152, 23);
            this.btn_selected.TabIndex = 7;
            this.btn_selected.Text = "请选择被上传文件夹";
            this.btn_selected.UseVisualStyleBackColor = true;
            this.btn_selected.Click += new System.EventHandler(this.btn_selected_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 591);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.txtBox_log);
            this.Controls.Add(this.txtBox_Jsession);
            this.Controls.Add(this.btn_Jsession);
            this.Controls.Add(this.txtBox_selected);
            this.Controls.Add(this.btn_selected);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "上传省厅审查表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TextBox txtBox_log;
        private System.Windows.Forms.TextBox txtBox_Jsession;
        private System.Windows.Forms.Button btn_Jsession;
        private System.Windows.Forms.TextBox txtBox_selected;
        private System.Windows.Forms.Button btn_selected;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_selected;
    }
}

