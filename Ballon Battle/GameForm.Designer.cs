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
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.createServerButton = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // glControl
            // 
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
            // 
            // glTimer
            // 
            this.glTimer.Interval = 5;
            this.glTimer.Tick += new System.EventHandler(this.glTimer_Tick);
            // 
            // prizeTimer
            // 
            this.prizeTimer.Interval = 5000;
            this.prizeTimer.Tick += new System.EventHandler(this.prizeTimer_Tick);
            // 
            // windTimer
            // 
            this.windTimer.Interval = 2000;
            this.windTimer.Tick += new System.EventHandler(this.windTimer_Tick);
            // 
            // firstPlayerInfo
            // 
            this.firstPlayerInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(142)))), ((int)(((byte)(196)))));
            this.firstPlayerInfo.ForeColor = System.Drawing.Color.White;
            this.firstPlayerInfo.Location = new System.Drawing.Point(0, 0);
            this.firstPlayerInfo.Name = "firstPlayerInfo";
            this.firstPlayerInfo.Size = new System.Drawing.Size(100, 23);
            this.firstPlayerInfo.TabIndex = 2;
            // 
            // secondPlayerInfo
            // 
            this.secondPlayerInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(142)))), ((int)(((byte)(196)))));
            this.secondPlayerInfo.ForeColor = System.Drawing.Color.White;
            this.secondPlayerInfo.Location = new System.Drawing.Point(0, 0);
            this.secondPlayerInfo.Name = "secondPlayerInfo";
            this.secondPlayerInfo.Size = new System.Drawing.Size(100, 23);
            this.secondPlayerInfo.TabIndex = 3;
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(325, 401);
            this.ipTextBox.Multiline = true;
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(157, 37);
            this.ipTextBox.TabIndex = 4;
            this.ipTextBox.Text = "26.41.29.58";
            this.ipTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectButton.Location = new System.Drawing.Point(104, 401);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(178, 37);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Подключиться";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // createServerButton
            // 
            this.createServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createServerButton.Location = new System.Drawing.Point(530, 401);
            this.createServerButton.Name = "createServerButton";
            this.createServerButton.Size = new System.Drawing.Size(178, 37);
            this.createServerButton.TabIndex = 6;
            this.createServerButton.Text = "Создать";
            this.createServerButton.UseVisualStyleBackColor = true;
            this.createServerButton.Click += new System.EventHandler(this.createServerButton_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(142)))), ((int)(((byte)(196)))));
            this.infoLabel.Location = new System.Drawing.Point(25, 23);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(0, 13);
            this.infoLabel.TabIndex = 7;
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelButton.Location = new System.Drawing.Point(313, 346);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(178, 37);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.firstPlayerInfo);
            this.Controls.Add(this.secondPlayerInfo);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.createServerButton);
            this.Name = "GameForm";
            this.Text = "Balloon Battle";
            this.Resize += new System.EventHandler(this.GameForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button createServerButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button cancelButton;
    }
}