//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@hotmail.com, cyberkdh@gmail.com), AI(Claude)
//	Module			: Form1
//	History			:
//	Copyrights		: Copyright ⓒCYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DHColorPicker {
	public partial class Form1 : Form {

		#region Win32 API
		[DllImport("user32.dll")] static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
		[DllImport("user32.dll")] static extern bool UnregisterHotKey(IntPtr hWnd, int id);
		[DllImport("kernel32.dll")] static extern short GlobalAddAtom(string lpString);
		[DllImport("kernel32.dll")] static extern short GlobalDeleteAtom(short nAtom);

		private const uint MOD_ALT     = 0x0001;
		private const uint MOD_CONTROL = 0x0002;
		private const uint MOD_WIN     = 0x0008;
		private const int  WM_HOTKEY   = 0x0312;
		#endregion

		private const string REG_KEY = @"Software\CYBERKDH\DHColorPicker";

		private System.Windows.Forms.Timer _timer;
		private int   _zoomFactor      = 8;
		private Size  _zoomCaptureSize;
		private int   _hotKeyAtom      = 0;
		private int   _copyFormatType  = 0;

		public Form1() {
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

			// R/G/B 레이블 초기화 (검정 배경, 청록색 텍스트)
			ctlR.BackColor  = Color.Black;
			ctlR.ForeColor  = Color.FromArgb(0, 254, 254);
			ctlR.Text       = "R";
			ctlR.TextAlign  = ContentAlignment.MiddleCenter;

			ctlG.BackColor  = Color.Black;
			ctlG.ForeColor  = Color.FromArgb(0, 254, 254);
			ctlG.Text       = "G";
			ctlG.TextAlign  = ContentAlignment.MiddleCenter;

			ctlB.BackColor  = Color.Black;
			ctlB.ForeColor  = Color.FromArgb(0, 254, 254);
			ctlB.Text       = "B";
			ctlB.TextAlign  = ContentAlignment.MiddleCenter;

			ctlRD.BackColor = Color.Black;
			ctlRD.ForeColor = Color.FromArgb(0, 254, 254);
			ctlRD.TextAlign = ContentAlignment.MiddleRight;

			ctlGD.BackColor = Color.Black;
			ctlGD.ForeColor = Color.FromArgb(0, 254, 254);
			ctlGD.TextAlign = ContentAlignment.MiddleRight;

			ctlBD.BackColor = Color.Black;
			ctlBD.ForeColor = Color.FromArgb(0, 254, 254);
			ctlBD.TextAlign = ContentAlignment.MiddleRight;

			// 줌 컨트롤 크기 짝수로 맞춤
			int zw = ctlZoom.Width  % 2 != 0 ? ctlZoom.Width  + 1 : ctlZoom.Width;
			int zh = ctlZoom.Height % 2 != 0 ? ctlZoom.Height + 1 : ctlZoom.Height;
			ctlZoom.Size       = new Size(zw, zh);
			_zoomCaptureSize   = new Size(zw / _zoomFactor, zh / _zoomFactor);
			ctlZoom.ShowZoomRect(_zoomFactor);

			// 콤보박스 초기화
			cbType.Items.AddRange(new object[] { "RGB(Integer)", "RGB(Hex)" });
			cbType.SelectedIndex = 0;

			cbCopyFormat.Items.AddRange(new object[] {
				"#RRGGBB", "RR, GG, BB", "Integer", "(RR, GG, BB)", "(256, 256, 256)"
			});

			// 레지스트리에서 설정 로드
			_copyFormatType = LoadSetting("COPYFORMATTYPE", 0);
			if (_copyFormatType < 0) _copyFormatType = 0;
			if (_copyFormatType > 4) _copyFormatType = 4;
			cbCopyFormat.SelectedIndex = _copyFormatType;

			int aot = LoadSetting("ALWAYSONTOP", 1);
			chkAlwaysOnTop.Checked = aot != 0;
			TopMost = aot != 0;

			// 전역 단축키 등록: Win+Ctrl+Alt+C
			_hotKeyAtom = GlobalAddAtom("MWCOLORPICKER");
			if (_hotKeyAtom != 0)
				RegisterHotKey(Handle, _hotKeyAtom, MOD_WIN | MOD_CONTROL | MOD_ALT, 0x43);

			// 100ms 타이머 시작
			_timer          = new System.Windows.Forms.Timer();
			_timer.Interval = 100;
			_timer.Tick    += Timer_Tick;
			_timer.Start();
		}

		// 마우스 위치 감지 및 색상/줌 업데이트
		private void Timer_Tick(object sender, EventArgs e) {
			Color col;
			Bitmap bm = CheckMouseLoc(out col);
			if (bm == null) return;

			ctlZoom.SetBitmap(bm);

			if (col != ctlPreviewColor.GetColor()) {
				ctlPreviewColor.BackColor = col;
				UpdateRgbDisplay(col);
			}
		}

		// 마우스 포인터 주변 영역 캡처 + 픽셀 색상 반환
		private Bitmap CheckMouseLoc(out Color colValue) {
			Point pt  = Cursor.Position;
			int   cw  = _zoomCaptureSize.Width;
			int   ch  = _zoomCaptureSize.Height;

			Bitmap bm = new Bitmap(cw, ch);
			using (Graphics g = Graphics.FromImage(bm)) {
				g.CopyFromScreen(
					pt.X - cw / 2, pt.Y - ch / 2,
					0, 0,
					new Size(cw, ch),
					CopyPixelOperation.SourceCopy);
			}

			colValue = bm.GetPixel(cw / 2, ch / 2);
			return bm;
		}

		// RGB 수치 레이블 업데이트
		private void UpdateRgbDisplay(Color col) {
			if (cbType.SelectedIndex == 1) {
				ctlRD.Text = col.R.ToString("X2");
				ctlGD.Text = col.G.ToString("X2");
				ctlBD.Text = col.B.ToString("X2");
			}
			else {
				ctlRD.Text = col.R.ToString();
				ctlGD.Text = col.G.ToString();
				ctlBD.Text = col.B.ToString();
			}
		}

		// 클립보드 복사 (5가지 형식)
		private void CopyColorToClipboard() {
			Color col = ctlPreviewColor.GetColor();
			int colorRef = col.R | (col.G << 8) | (col.B << 16);
			string str;
			switch (_copyFormatType) {
				case 0:  str = $"#{col.R:X2}{col.G:X2}{col.B:X2}";              break;
				case 1:  str = $"{col.R:X2}, {col.G:X2}, {col.B:X2}";          break;
				case 2:  str = colorRef.ToString();                              break;
				case 3:  str = $"({col.R:X2}, {col.G:X2}, {col.B:X2})";        break;
				case 4:  str = $"({col.R}, {col.G}, {col.B})";                  break;
				default: str = $"#{col.R:X2}{col.G:X2}{col.B:X2}";              break;
			}
			Clipboard.SetText(str);
		}

		// 전역 단축키 메시지 처리 (WM_HOTKEY)
		protected override void WndProc(ref Message m) {
			if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == _hotKeyAtom)
				CopyColorToClipboard();
			base.WndProc(ref m);
		}

		// Always On Top 토글
		private void chkAlwaysOnTop_CheckedChanged(object sender, EventArgs e) {
			TopMost = chkAlwaysOnTop.Checked;
			SaveSetting("ALWAYSONTOP", chkAlwaysOnTop.Checked ? 1 : 0);
		}

		// 복사 형식 변경
		private void cbCopyFormat_SelectedIndexChanged(object sender, EventArgs e) {
			_copyFormatType = cbCopyFormat.SelectedIndex;
			if (_copyFormatType < 0) _copyFormatType = 0;
			if (_copyFormatType > 4) _copyFormatType = 4;
			SaveSetting("COPYFORMATTYPE", _copyFormatType);
		}

		// Dec/Hex 형식 변경 시 즉시 업데이트
		private void cbType_SelectedIndexChanged(object sender, EventArgs e) {
			UpdateRgbDisplay(ctlPreviewColor.GetColor());
		}

		private void btnAbout_Click(object sender, EventArgs e) {
			using (var dlg = new AboutForm())
				dlg.ShowDialog(this);
		}

		// 레지스트리 설정 로드/저장
		private int LoadSetting(string key, int defaultValue) {
			try {
				using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(REG_KEY)) {
					if (rk == null) return defaultValue;
					object val = rk.GetValue(key);
					return val != null ? (int)val : defaultValue;
				}
			}
			catch { return defaultValue; }
		}

		private void SaveSetting(string key, int value) {
			try {
				using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(REG_KEY))
					rk.SetValue(key, value, RegistryValueKind.DWord);
			}
			catch { }
		}

		protected override void OnFormClosing(FormClosingEventArgs e) {
			_timer?.Stop();
			_timer?.Dispose();
			if (_hotKeyAtom != 0) {
				UnregisterHotKey(Handle, _hotKeyAtom);
				GlobalDeleteAtom((short)_hotKeyAtom);
				_hotKeyAtom = 0;
			}
			base.OnFormClosing(e);
		}
	}
}
