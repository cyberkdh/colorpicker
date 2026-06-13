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
	// CDHColorFillCtl 포팅 — 색상 채우기 또는 비트맵(줌) 표시 커스텀 패널
	public class DHColorFillControl : Panel {
		private Color _fillColor = Color.White;
		private Color _textColor = Color.Black;
		private string _text = "";
		private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;
		private Bitmap _bitmap = null;
		private int _zoomRectFactor = 0;

		public DHColorFillControl() {
			SetStyle(
				ControlStyles.UserPaint |
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.OptimizedDoubleBuffer,
				true);
		}

		public new Color BackColor {
			get => _fillColor;
			set { _fillColor = value; Invalidate(); }
		}

		public new Color ForeColor {
			get => _textColor;
			set { _textColor = value; Invalidate(); }
		}

		public new string Text {
			get => _text;
			set { _text = value; Invalidate(); }
		}

		public ContentAlignment TextAlign {
			get => _textAlign;
			set { _textAlign = value; Invalidate(); }
		}

		public Color GetColor() => _fillColor;

		public void SetBitmap(Bitmap bm) {
			Bitmap old = _bitmap;
			_bitmap = bm;
			old?.Dispose();
			Invalidate();
		}

		public void ShowZoomRect(int factor) {
			_zoomRectFactor = factor;
			Invalidate();
		}

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
