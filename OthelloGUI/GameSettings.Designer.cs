namespace OthelloGUI
{
    public partial class GameSettings
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonAgainstComputer = new System.Windows.Forms.Button();
            this.buttonAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(16, 20);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(274, 45);
            this.buttonBoardSize.TabIndex = 3;
            this.buttonBoardSize.Text = "Board Size: 6x6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonAgainstComputer
            // 
            this.buttonAgainstComputer.Location = new System.Drawing.Point(16, 85);
            this.buttonAgainstComputer.Name = "buttonAgainstComputer";
            this.buttonAgainstComputer.Size = new System.Drawing.Size(126, 48);
            this.buttonAgainstComputer.TabIndex = 0;
            this.buttonAgainstComputer.Text = "Play against the computer";
            this.buttonAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonAgainstComputer.Click += new System.EventHandler(this.buttonAgainstComputer_Click);
            // 
            // buttonAgainstFriend
            // 
            this.buttonAgainstFriend.Location = new System.Drawing.Point(164, 85);
            this.buttonAgainstFriend.Name = "buttonAgainstFriend";
            this.buttonAgainstFriend.Size = new System.Drawing.Size(126, 48);
            this.buttonAgainstFriend.TabIndex = 2;
            this.buttonAgainstFriend.Text = "Play against your friend";
            this.buttonAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonAgainstFriend.Click += new System.EventHandler(this.buttonAgainstFriend_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(310, 156);
            this.Controls.Add(this.buttonAgainstFriend);
            this.Controls.Add(this.buttonAgainstComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonAgainstComputer;
        private System.Windows.Forms.Button buttonAgainstFriend;
    }
}