namespace Tracking.Debug
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.cbDebugFrame = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbClickedColor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHoveringBall = new System.Windows.Forms.TextBox();
            this.lbCoordinates = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(701, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start engine";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(701, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(126, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop engine";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(701, 70);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(126, 23);
            this.btnCalibrate.TabIndex = 2;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(701, 99);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(126, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // imageBox
            // 
            this.imageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox.Location = new System.Drawing.Point(13, 12);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(682, 406);
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.imageBox_Click);
            this.imageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseMove);
            // 
            // cbDebugFrame
            // 
            this.cbDebugFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDebugFrame.FormattingEnabled = true;
            this.cbDebugFrame.Location = new System.Drawing.Point(701, 155);
            this.cbDebugFrame.Name = "cbDebugFrame";
            this.cbDebugFrame.Size = new System.Drawing.Size(126, 21);
            this.cbDebugFrame.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Debug frame:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(702, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clicked color:";
            // 
            // tbClickedColor
            // 
            this.tbClickedColor.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClickedColor.Location = new System.Drawing.Point(701, 215);
            this.tbClickedColor.Name = "tbClickedColor";
            this.tbClickedColor.ReadOnly = true;
            this.tbClickedColor.Size = new System.Drawing.Size(126, 20);
            this.tbClickedColor.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(782, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "(H/S/V)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 429);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Hovered ball information:";
            // 
            // tbHoveringBall
            // 
            this.tbHoveringBall.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHoveringBall.Location = new System.Drawing.Point(12, 445);
            this.tbHoveringBall.Multiline = true;
            this.tbHoveringBall.Name = "tbHoveringBall";
            this.tbHoveringBall.ReadOnly = true;
            this.tbHoveringBall.Size = new System.Drawing.Size(683, 115);
            this.tbHoveringBall.TabIndex = 11;
            // 
            // lbCoordinates
            // 
            this.lbCoordinates.Location = new System.Drawing.Point(702, 537);
            this.lbCoordinates.Name = "lbCoordinates";
            this.lbCoordinates.Size = new System.Drawing.Size(125, 23);
            this.lbCoordinates.TabIndex = 12;
            this.lbCoordinates.Text = "(0, 0)";
            this.lbCoordinates.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 572);
            this.Controls.Add(this.lbCoordinates);
            this.Controls.Add(this.tbHoveringBall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbClickedColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDebugFrame);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "PSE Billard Tracking System Visual Debugger";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Button btnReset;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.ComboBox cbDebugFrame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbClickedColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbHoveringBall;
        private System.Windows.Forms.Label lbCoordinates;
    }
}

