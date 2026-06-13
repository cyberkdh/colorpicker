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

		// Initializes form components
		// 폼 컴포넌트 초기화
		public Form1() {
			InitializeComponent();
		}

		// Initializes controls, loads settings, registers hotkey, starts timer
		// 컨트롤 초기화, 설정 로드, 단축키 등록, 타이머 시작
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);

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

			int zw = ctlZoom.Width  % 2 != 0 ? ctlZoom.Width  + 1 : ctlZoom.Width;
			int zh = ctlZoom.Height % 2 != 0 ? ctlZoom.Height + 1 : ctlZoom.Height;
			ctlZoom.Size       = new Size(zw, zh);
			_zoomCaptureSize   = new Size(zw / _zoomFactor, zh / _zoomFactor);
			ctlZoom.ShowZoomRect(_zoomFactor);

			cbType.Items.AddRange(new object[] { "RGB(Integer)", "RGB(Hex)" });
			cbType.SelectedIndex = 0;

			cbCopyFormat.Items.AddRange(new object[] {
				"#RRGGBB", "RR, GG, BB", "Integer", "(RR, GG, BB)", "(256, 256, 256)"
			});

			_copyFormatType = LoadSetting("COPYFORMATTYPE", 0);
			if (_copyFormatType < 0) _copyFormatType = 0;
			if (_copyFormatType > 4) _copyFormatType = 4;
			cbCopyFormat.SelectedIndex = _copyFormatType;

			int aot = LoadSetting("ALWAYSONTOP", 1);
			chkAlwaysOnTop.Checked = aot != 0;
			TopMost = aot != 0;

			_hotKeyAtom = GlobalAddAtom("MWCOLORPICKER");
			if (_hotKeyAtom != 0)
				RegisterHotKey(Handle, _hotKeyAtom, MOD_WIN | MOD_CONTROL | MOD_ALT, 0x43);

			_timer          = new System.Windows.Forms.Timer();
			_timer.Interval = 100;
			_timer.Tick    += Timer_Tick;
			_timer.Start();
		}

		// Detects mouse position every 100ms and updates color/zoom display
		// 100ms마다 마우스 위치를 감지하여 색상/줌 표시 업데이트
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

		// Captures screen area around cursor and returns the pixel color at cursor
		// 마우스 포인터 주변 영역을 캡처하고 커서 위치의 픽셀 색상을 반환
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

		// Updates R/G/B value labels in decimal or hex format
		// R/G/B 수치 레이블을 10진수 또는 16진수 형식으로 업데이트
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

		// Copies the current color to clipboard in the selected format (5 formats)
		// 현재 색상을 선택된 형식(5가지)으로 클립보드에 복사
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

		// Handles WM_HOTKEY message to trigger clipboard copy via global hotkey
		// WM_HOTKEY 메시지를 처리하여 전역 단축키로 클립보드 복사 실행
		protected override void WndProc(ref Message m) {
			if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == _hotKeyAtom)
				CopyColorToClipboard();
			base.WndProc(ref m);
		}

		// Toggles Always On Top and saves the state to registry
		// Always On Top을 토글하고 레지스트리에 상태 저장
		private void chkAlwaysOnTop_CheckedChanged(object sender, EventArgs e) {
			TopMost = chkAlwaysOnTop.Checked;
			SaveSetting("ALWAYSONTOP", chkAlwaysOnTop.Checked ? 1 : 0);
		}

		// Updates copy format type and saves the selection to registry
		// 복사 형식 타입을 갱신하고 선택값을 레지스트리에 저장
		private void cbCopyFormat_SelectedIndexChanged(object sender, EventArgs e) {
			_copyFormatType = cbCopyFormat.SelectedIndex;
			if (_copyFormatType < 0) _copyFormatType = 0;
			if (_copyFormatType > 4) _copyFormatType = 4;
			SaveSetting("COPYFORMATTYPE", _copyFormatType);
		}

		// Refreshes RGB display immediately when display type (Dec/Hex) changes
		// 표시 형식(10진/16진) 변경 시 RGB 수치를 즉시 갱신
		private void cbType_SelectedIndexChanged(object sender, EventArgs e) {
			UpdateRgbDisplay(ctlPreviewColor.GetColor());
		}

		// Opens the About dialog
		// About 다이얼로그를 표시
		private void btnAbout_Click(object sender, EventArgs e) {
			using (var dlg = new AboutForm())
				dlg.ShowDialog(this);
		}

		// Loads an integer value from the registry; returns defaultValue if not found
		// 레지스트리에서 정수값 로드, 없으면 defaultValue 반환
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

		// Saves an integer value to the registry as DWORD
		// 레지스트리에 정수값을 DWORD 형식으로 저장
		private void SaveSetting(string key, int value) {
			try {
				using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(REG_KEY))
					rk.SetValue(key, value, RegistryValueKind.DWord);
			}
			catch { }
		}

		// Stops timer and unregisters hotkey on form close
		// 폼 종료 시 타이머 정지 및 전역 단축키 해제
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
