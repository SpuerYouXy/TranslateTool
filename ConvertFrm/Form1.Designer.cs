namespace ConvertFrm
{
    partial class Form1
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
            this.btn_choiceExcel = new System.Windows.Forms.Button();
            this.lb_excelPath = new System.Windows.Forms.Label();
            this.btn_replace = new System.Windows.Forms.Button();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.btn_output = new System.Windows.Forms.Button();
            this.lb_outputDir = new System.Windows.Forms.Label();
            this.btn_choiceFile = new System.Windows.Forms.Button();
            this.lb_filepath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_choiceExcel
            // 
            this.btn_choiceExcel.Location = new System.Drawing.Point(22, 21);
            this.btn_choiceExcel.Name = "btn_choiceExcel";
            this.btn_choiceExcel.Size = new System.Drawing.Size(75, 23);
            this.btn_choiceExcel.TabIndex = 0;
            this.btn_choiceExcel.Text = "选择Excel";
            this.btn_choiceExcel.UseVisualStyleBackColor = true;
            this.btn_choiceExcel.Click += new System.EventHandler(this.btn_choiceExcel_Click);
            // 
            // lb_excelPath
            // 
            this.lb_excelPath.AutoSize = true;
            this.lb_excelPath.Location = new System.Drawing.Point(103, 26);
            this.lb_excelPath.Name = "lb_excelPath";
            this.lb_excelPath.Size = new System.Drawing.Size(0, 12);
            this.lb_excelPath.TabIndex = 1;
            // 
            // btn_replace
            // 
            this.btn_replace.Location = new System.Drawing.Point(22, 124);
            this.btn_replace.Name = "btn_replace";
            this.btn_replace.Size = new System.Drawing.Size(152, 23);
            this.btn_replace.TabIndex = 2;
            this.btn_replace.Text = "替换文件中的中文";
            this.btn_replace.UseVisualStyleBackColor = true;
            this.btn_replace.Click += new System.EventHandler(this.btn_replace_Click);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(22, 162);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_log.Size = new System.Drawing.Size(485, 129);
            this.txt_log.TabIndex = 5;
            // 
            // btn_output
            // 
            this.btn_output.Location = new System.Drawing.Point(22, 60);
            this.btn_output.Name = "btn_output";
            this.btn_output.Size = new System.Drawing.Size(103, 23);
            this.btn_output.TabIndex = 3;
            this.btn_output.Text = "选择输出目录";
            this.btn_output.UseVisualStyleBackColor = true;
            this.btn_output.Click += new System.EventHandler(this.btn_output_Click);
            // 
            // lb_outputDir
            // 
            this.lb_outputDir.AutoSize = true;
            this.lb_outputDir.Location = new System.Drawing.Point(169, 65);
            this.lb_outputDir.Name = "lb_outputDir";
            this.lb_outputDir.Size = new System.Drawing.Size(0, 12);
            this.lb_outputDir.TabIndex = 4;
            // 
            // btn_choiceFile
            // 
            this.btn_choiceFile.Location = new System.Drawing.Point(22, 95);
            this.btn_choiceFile.Name = "btn_choiceFile";
            this.btn_choiceFile.Size = new System.Drawing.Size(103, 23);
            this.btn_choiceFile.TabIndex = 6;
            this.btn_choiceFile.Text = "选择文件";
            this.btn_choiceFile.UseVisualStyleBackColor = true;
            this.btn_choiceFile.Click += new System.EventHandler(this.btn_choiceFile_Click);
            // 
            // lb_filepath
            // 
            this.lb_filepath.AutoSize = true;
            this.lb_filepath.Location = new System.Drawing.Point(133, 100);
            this.lb_filepath.Name = "lb_filepath";
            this.lb_filepath.Size = new System.Drawing.Size(0, 12);
            this.lb_filepath.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 295);
            this.Controls.Add(this.lb_filepath);
            this.Controls.Add(this.btn_choiceFile);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.lb_outputDir);
            this.Controls.Add(this.btn_output);
            this.Controls.Add(this.btn_replace);
            this.Controls.Add(this.lb_excelPath);
            this.Controls.Add(this.btn_choiceExcel);
            this.Name = "Form1";
            this.Text = "转英文窗口";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_choiceExcel;
        private System.Windows.Forms.Label lb_excelPath;
        private System.Windows.Forms.Button btn_replace;
        private System.Windows.Forms.TextBox txt_log;
        private System.Windows.Forms.Button btn_output;
        private System.Windows.Forms.Label lb_outputDir;
        private System.Windows.Forms.Button btn_choiceFile;
        private System.Windows.Forms.Label lb_filepath;
    }
}

