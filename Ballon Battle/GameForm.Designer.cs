using System.Drawing;

namespace Ballon_Battle
{
    partial class GameForm
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
            this.glControl = new OpenTK.GLControl();
            this.glTimer = new System.Windows.Forms.Timer(this.components);
            this.prizeTimer = new System.Windows.Forms.Timer(this.components);
            this.windTimer = new System.Windows.Forms.Timer(this.components);
            this.firstPlayerInfo = new System.Windows.Forms.Label();
            this.secondPlayerInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Location = new System.Drawing.Point(0, 0);
            this.glControl.Name = "glControl";
            this.glControl.Size = this.Size;
            this.glControl.TabIndex = 1;
            this.glControl.VSync = false;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
            this.glControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyUp);
            
            this.glTimer.Interval = 10;
            this.glTimer.Tick += new System.EventHandler(this.glTimer_Tick);
            
            this.prizeTimer.Interval = 5000;
            this.prizeTimer.Tick += new System.EventHandler(this.prizeTimer_Tick);
            
            this.windTimer.Interval = 2000;
            this.windTimer.Tick += new System.EventHandler(this.windTimer_Tick);
            
            this.firstPlayerInfo.BackColor = Color.FromArgb(65, 142, 196);
            this.firstPlayerInfo.ForeColor = System.Drawing.Color.White;
            this.firstPlayerInfo.Location = new System.Drawing.Point(0, 0);
            this.firstPlayerInfo.Name = "firstPlayerInfo";
            this.firstPlayerInfo.Size = new System.Drawing.Size(100, 23);
            this.firstPlayerInfo.TabIndex = 2;
            
            this.secondPlayerInfo.BackColor = Color.FromArgb(65, 142, 196);
            this.secondPlayerInfo.ForeColor = System.Drawing.Color.White;
            this.secondPlayerInfo.Location = new System.Drawing.Point(0, 0);
            this.secondPlayerInfo.Name = "secondPlayerInfo";
            this.secondPlayerInfo.Size = new System.Drawing.Size(100, 23);
            this.secondPlayerInfo.TabIndex = 3;
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.firstPlayerInfo);
            this.Controls.Add(this.secondPlayerInfo);
            this.Name = "GameForm";
            this.Text = "Balloon Battle";
            this.Resize += new System.EventHandler(this.GameForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Объект GLControl
        /// </summary>
        private OpenTK.GLControl glControl;

        /// <summary>
        /// Таймер, отвечающий за обновление игры
        /// </summary>
        private System.Windows.Forms.Timer glTimer;

        /// <summary>
        /// Таймер, отвечающий за появление призов
        /// </summary>
        private System.Windows.Forms.Timer prizeTimer;

        /// <summary>
        /// Таймер, отвечающий за появление ветра
        /// </summary>
        private System.Windows.Forms.Timer windTimer;
    }
}