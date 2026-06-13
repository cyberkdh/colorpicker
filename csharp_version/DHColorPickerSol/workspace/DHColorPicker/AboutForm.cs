//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@hotmail.com, cyberkdh@gmail.com), AI(Claude)
//	Module			: AboutForm
//	History			:
//	Copyrights		: Copyright ⓒCYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DHColorPicker {
	// MFC IDD_ABOUTBOX 포팅 — 237x96 DLU
	public class AboutForm : Form {
		public AboutForm() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			this.SuspendLayout();

			// 로고 비트맵 (MFC: IDB_BM_LOGO1, 7,7,80,82 DLU → ~10,11,120,133px)
			var picLogo = new PictureBox();
			picLogo.Location = new Point(10, 10);
			picLogo.Size     = new Size(80, 82);
			picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
			picLogo.TabStop  = false;
			try {
				string exeDir  = Path.GetDirectoryName(Application.ExecutablePath);
				string logoPath = Path.GetFullPath(Path.Combine(exeDir,
					@"..\..\..\..\..\projectroot\cpp_version\common\res\cyberkdh_picture.bmp"));
				if (File.Exists(logoPath))
					picLogo.Image = Image.FromFile(logoPath);
			}
			catch { }

			// "DHColorPicker, Version 2.0.1.11" (MFC: 93,7,135,8 DLU)
			var lblTitle = new Label();
			lblTitle.AutoSize = false;
			lblTitle.Location = new Point(100, 10);
			lblTitle.Size     = new Size(200, 16);
			lblTitle.Text     = "DHColorPicker, Version 2.0.1.11";

			// "Copyright (C)DHTOOLS 2008" (MFC: 93,18,135,8 DLU)
			var lblCopy = new Label();
			lblCopy.AutoSize = false;
			lblCopy.Location = new Point(100, 28);
			lblCopy.Size     = new Size(200, 16);
			lblCopy.Text     = "Copyright (C)DHTOOLS 2008";

			// Contact + License (MFC: 93,31,135,20 DLU)
			var lblContact = new Label();
			lblContact.AutoSize = false;
			lblContact.Location = new Point(100, 46);
			lblContact.Size     = new Size(200, 32);
			lblContact.Text     = "Contact Info:  cyberkdh@gmail.com\r\nLicense: More than a cup of coffee";

			// "Thanks to junga, chanmi." (MFC: 93,59,135,12 DLU)
			var lblThanks = new Label();
			lblThanks.AutoSize = false;
			lblThanks.Location = new Point(100, 82);
			lblThanks.Size     = new Size(200, 16);
			lblThanks.Text     = "Thanks to junga, chanmi.";

			// OK 버튼 (MFC: 180,75,50,14 DLU)
			var btnOK = new Button();
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location     = new Point(270, 108);
			btnOK.Size         = new Size(50, 22);
			btnOK.Text         = "OK";

			this.AcceptButton        = btnOK;
			this.AutoScaleDimensions = new SizeF(7F, 15F);
			this.AutoScaleMode       = AutoScaleMode.Font;
			this.ClientSize          = new Size(328, 138);
			this.FormBorderStyle     = FormBorderStyle.FixedDialog;
			this.MaximizeBox         = false;
			this.MinimizeBox         = false;
			this.ShowInTaskbar       = false;
			this.StartPosition       = FormStartPosition.CenterParent;
			this.Text                = "About DHColorPicker";

			this.Controls.Add(picLogo);
			this.Controls.Add(lblTitle);
			this.Controls.Add(lblCopy);
			this.Controls.Add(lblContact);
			this.Controls.Add(lblThanks);
			this.Controls.Add(btnOK);

			this.ResumeLayout(false);
		}
	}
}
