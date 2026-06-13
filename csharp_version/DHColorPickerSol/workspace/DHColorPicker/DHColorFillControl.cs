//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@hotmail.com, cyberkdh@gmail.com), AI(Claude)
//	Module			: DHColorFillControl
//	History			:
//	Copyrights		: Copyright ⓒCYBERKDH. All Rights Reserved.
//////////////////////////////////////////////////////////////////////////////////////////////////

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DHColorPicker {
	// CDHColorFillCtl port — custom panel for color fill or bitmap (zoom) display
	// CDHColorFillCtl 포팅 — 색상 채우기 또는 비트맵(줌) 표시 커스텀 패널
	public class DHColorFillControl : Panel {
		private Color _fillColor = Color.White;
		private Color _textColor = Color.Black;
		private string _text = "";
		private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;
		private Bitmap _bitmap = null;
		private int _zoomRectFactor = 0;

		// Enables double-buffered owner-draw rendering
		// 더블버퍼 오너드로우 렌더링 활성화
		public DHColorFillControl() {
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		// Gets or sets the background fill color and triggers repaint
		// 배경 채우기 색상을 가져오거나 설정하고 다시 그리기 트리거
		public new Color BackColor {
			get => _fillColor;
			set { _fillColor = value; Invalidate(); }
		}

		// Gets or sets the text foreground color and triggers repaint
		// 텍스트 전경색을 가져오거나 설정하고 다시 그리기 트리거
		public new Color ForeColor {
			get => _textColor;
			set { _textColor = value; Invalidate(); }
		}

		// Gets or sets the display text and triggers repaint
		// 표시 텍스트를 가져오거나 설정하고 다시 그리기 트리거
		public new string Text {
			get => _text;
			set { _text = value; Invalidate(); }
		}

		// Gets or sets the text alignment and triggers repaint
		// 텍스트 정렬 방식을 가져오거나 설정하고 다시 그리기 트리거
		public ContentAlignment TextAlign {
			get => _textAlign;
			set { _textAlign = value; Invalidate(); }
		}

		// Returns the current fill color
		// 현재 채우기 색상을 반환
		public Color GetColor() => _fillColor;

		// Sets the zoom bitmap to display and triggers repaint
		// 표시할 줌 비트맵을 설정하고 다시 그리기 트리거
		public void SetBitmap(Bitmap bm) {
			Bitmap old = _bitmap;
			_bitmap = bm;
			old?.Dispose();
			Invalidate();
		}

		// Sets the zoom factor for the center crosshair rectangle overlay
		// 중앙 십자선 사각형 오버레이의 줌 배율을 설정
		public void ShowZoomRect(int factor) {
			_zoomRectFactor = factor;
			Invalidate();
		}

		// Paints the control: draws bitmap with zoom rect overlay, or solid color with text
		// 컨트롤 그리기: 줌 사각형 오버레이가 있는 비트맵, 또는 단색 배경에 텍스트 렌더링
		protected override void OnPaint(PaintEventArgs e) {
			Graphics g = e.Graphics;
			Rectangle rt = ClientRectangle;

			if (_bitmap != null) {
				g.InterpolationMode = InterpolationMode.NearestNeighbor;
				g.PixelOffsetMode = PixelOffsetMode.Half;
				g.DrawImage(_bitmap, rt);

				if (_zoomRectFactor != 0) {
					int half = _zoomRectFactor / 2;
					Rectangle zRect = new Rectangle(
						rt.Width / 2 - half,
						rt.Height / 2 - half,
						_zoomRectFactor,
						_zoomRectFactor);
					using (Pen pen = new Pen(Color.Black, 1))
						g.DrawRectangle(pen, zRect);
				}
			}
			else {
				using (SolidBrush brush = new SolidBrush(_fillColor))
					g.FillRectangle(brush, rt);

				if (!string.IsNullOrEmpty(_text)) {
					TextRenderer.DrawText(g, _text, Font, rt, _textColor, ToTextFlags(_textAlign));
				}
			}
		}

		// Converts ContentAlignment to TextFormatFlags for TextRenderer
		// ContentAlignment을 TextRenderer용 TextFormatFlags로 변환
		private static TextFormatFlags ToTextFlags(ContentAlignment align) {
			switch (align) {
				case ContentAlignment.MiddleRight:
					return TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
				case ContentAlignment.MiddleLeft:
					return TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
				default:
					return TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
			}
		}
	}
}
