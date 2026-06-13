//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@hotmail.com, cyberkdh@gmail.com), AI(Claude)
//	Module			: Form1.Designer
//	History			:
//	Copyrights		: Copyright ⓒCYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////

namespace DHColorPicker {
	partial class Form1 {
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing) {
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent() {
			this.ctlZoom         = new DHColorPicker.DHColorFillControl();
			this.ctlPreviewColor = new DHColorPicker.DHColorFillControl();
			this.ctlR            = new DHColorPicker.DHColorFillControl();
			this.ctlRD           = new DHColorPicker.DHColorFillControl();
			this.ctlG            = new DHColorPicker.DHColorFillControl();
			this.ctlGD           = new DHColorPicker.DHColorFillControl();
			this.ctlB            = new DHColorPicker.DHColorFillControl();
			this.ctlBD           = new DHColorPicker.DHColorFillControl();
			this.cbType          = new System.Windows.Forms.ComboBox();
			this.lblCopyFormat   = new System.Windows.Forms.Label();
			this.cbCopyFormat    = new System.Windows.Forms.ComboBox();
			this.chkAlwaysOnTop  = new System.Windows.Forms.CheckBox();
			this.lblCopyKey      = new System.Windows.Forms.Label();
			this.btnAbout        = new System.Windows.Forms.Button();
			this.SuspendLayout();

			// ctlZoom — 줌 확대 표시 영역 (MFC: 93x100 DLU)
			this.ctlZoom.Location = new System.Drawing.Point(8, 8);
			this.ctlZoom.Name     = "ctlZoom";
			this.ctlZoom.Size     = new System.Drawing.Size(150, 168);
			this.ctlZoom.TabStop  = false;

			// ctlPreviewColor — 현재 색상 미리보기 (MFC: 48x38 DLU inner)
			this.ctlPreviewColor.BackColor = System.Drawing.Color.Silver;
			this.ctlPreviewColor.Location  = new System.Drawing.Point(164, 38);
			this.ctlPreviewColor.Name      = "ctlPreviewColor";
			this.ctlPreviewColor.Size      = new System.Drawing.Size(88, 68);
			this.ctlPreviewColor.TabStop   = false;

			// ctlR — "R" 레이블
			this.ctlR.Location = new System.Drawing.Point(260, 38);
			this.ctlR.Name     = "ctlR";
			this.ctlR.Size     = new System.Drawing.Size(28, 24);
			this.ctlR.TabStop  = false;

			// ctlRD — R 값 표시
			this.ctlRD.Location = new System.Drawing.Point(291, 38);
			this.ctlRD.Name     = "ctlRD";
			this.ctlRD.Size     = new System.Drawing.Size(97, 24);
			this.ctlRD.TabStop  = false;

			// ctlG — "G" 레이블
			this.ctlG.Location = new System.Drawing.Point(260, 66);
			this.ctlG.Name     = "ctlG";
			this.ctlG.Size     = new System.Drawing.Size(28, 24);
			this.ctlG.TabStop  = false;

			// ctlGD — G 값 표시
			this.ctlGD.Location = new System.Drawing.Point(291, 66);
			this.ctlGD.Name     = "ctlGD";
			this.ctlGD.Size     = new System.Drawing.Size(97, 24);
			this.ctlGD.TabStop  = false;

			// ctlB — "B" 레이블
			this.ctlB.Location = new System.Drawing.Point(260, 94);
			this.ctlB.Name     = "ctlB";
			this.ctlB.Size     = new System.Drawing.Size(28, 24);
			this.ctlB.TabStop  = false;

			// ctlBD — B 값 표시
			this.ctlBD.Location = new System.Drawing.Point(291, 94);
			this.ctlBD.Name     = "ctlBD";
			this.ctlBD.Size     = new System.Drawing.Size(97, 24);
			this.ctlBD.TabStop  = false;

			// cbType — RGB(Integer) / RGB(Hex) 선택 (MFC: top-right, full width)
			this.cbType.DropDownStyle         = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.FormattingEnabled     = false;
			this.cbType.Location              = new System.Drawing.Point(164, 8);
			this.cbType.Name                  = "cbType";
			this.cbType.Size                  = new System.Drawing.Size(224, 23);
			this.cbType.TabIndex              = 1;
			this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);

			// lblCopyFormat — "Copy Format" 레이블
			this.lblCopyFormat.AutoSize  = false;
			this.lblCopyFormat.Location  = new System.Drawing.Point(164, 144);
			this.lblCopyFormat.Name      = "lblCopyFormat";
			this.lblCopyFormat.Size      = new System.Drawing.Size(68, 23);
			this.lblCopyFormat.Text      = "Copy Format";
			this.lblCopyFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

			// cbCopyFormat — 클립보드 복사 형식 선택
			this.cbCopyFormat.DropDownStyle         = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCopyFormat.FormattingEnabled     = false;
			this.cbCopyFormat.Location              = new System.Drawing.Point(235, 141);
			this.cbCopyFormat.Name                  = "cbCopyFormat";
			this.cbCopyFormat.Size                  = new System.Drawing.Size(153, 23);
			this.cbCopyFormat.TabIndex              = 2;
			this.cbCopyFormat.SelectedIndexChanged += new System.EventHandler(this.cbCopyFormat_SelectedIndexChanged);

			// chkAlwaysOnTop — 항상 위에 표시
			this.chkAlwaysOnTop.AutoSize         = true;
			this.chkAlwaysOnTop.Location         = new System.Drawing.Point(8, 183);
			this.chkAlwaysOnTop.Name             = "chkAlwaysOnTop";
			this.chkAlwaysOnTop.Size             = new System.Drawing.Size(95, 19);
			this.chkAlwaysOnTop.TabIndex         = 3;
			this.chkAlwaysOnTop.Text             = "Always on Top";
			this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
			this.chkAlwaysOnTop.CheckedChanged  += new System.EventHandler(this.chkAlwaysOnTop_CheckedChanged);

			// lblCopyKey — "Copy KEY: [WIN]+[CTRL]+[ALT]+[C]"
			this.lblCopyKey.AutoSize  = false;
			this.lblCopyKey.Location  = new System.Drawing.Point(108, 183);
			this.lblCopyKey.Name      = "lblCopyKey";
			this.lblCopyKey.Size      = new System.Drawing.Size(230, 19);
			this.lblCopyKey.Text      = "Copy KEY: [WIN]+[CTRL]+[ALT]+[C]";
			this.lblCopyKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

			// btnAbout — About 버튼
			this.btnAbout.Location = new System.Drawing.Point(344, 180);
			this.btnAbout.Name     = "btnAbout";
			this.btnAbout.Size     = new System.Drawing.Size(52, 22);
			this.btnAbout.TabIndex = 4;
			this.btnAbout.Text     = "About...";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click  += new System.EventHandler(this.btnAbout_Click);

			// Form1
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize          = new System.Drawing.Size(400, 210);
			this.Controls.Add(this.ctlZoom);
			this.Controls.Add(this.ctlPreviewColor);
			this.Controls.Add(this.ctlR);
			this.Controls.Add(this.ctlRD);
			this.Controls.Add(this.ctlG);
			this.Controls.Add(this.ctlGD);
			this.Controls.Add(this.ctlB);
			this.Controls.Add(this.ctlBD);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.lblCopyFormat);
			this.Controls.Add(this.cbCopyFormat);
			this.Controls.Add(this.chkAlwaysOnTop);
			this.Controls.Add(this.lblCopyKey);
			this.Controls.Add(this.btnAbout);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox     = false;
			this.MinimizeBox     = false;
			this.Name            = "Form1";
			this.Text            = "DHColorPicker";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private DHColorFillControl ctlZoom;
		private DHColorFillControl ctlPreviewColor;
		private DHColorFillControl ctlR;
		private DHColorFillControl ctlRD;
		private DHColorFillControl ctlG;
		private DHColorFillControl ctlGD;
		private DHColorFillControl ctlB;
		private DHColorFillControl ctlBD;
		private System.Windows.Forms.ComboBox  cbType;
		private System.Windows.Forms.Label     lblCopyFormat;
		private System.Windows.Forms.ComboBox  cbCopyFormat;
		private System.Windows.Forms.CheckBox  chkAlwaysOnTop;
		private System.Windows.Forms.Label     lblCopyKey;
		private System.Windows.Forms.Button    btnAbout;
	}
}
