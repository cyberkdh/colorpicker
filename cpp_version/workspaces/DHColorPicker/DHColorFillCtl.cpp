//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@gmail.com)
//	Module			: 
//	History			: 
//
//////////////////////////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "DHColorPicker.h"
#include "DHColorFillCtl.h"

IMPLEMENT_DYNAMIC(CDHColorFillCtl, CStatic)

CDHColorFillCtl::CDHColorFillCtl()
{
	m_colBack = RGB(255, 255, 255);
	m_colText = RGB(0, 0, 0);
	m_nTextAlign = DT_LEFT | DT_TOP | DT_SINGLELINE;
	m_strText = "";
	m_hBitmap = NULL;
	m_nZoomRectFactor = 0;
}

CDHColorFillCtl::~CDHColorFillCtl()
{
	if (m_hBitmap != NULL)
		::DeleteObject(m_hBitmap);
	m_hBitmap = NULL;
}

BEGIN_MESSAGE_MAP(CDHColorFillCtl, CStatic)
	ON_WM_PAINT()
END_MESSAGE_MAP()

void CDHColorFillCtl::OnPaint()
{
	CPaintDC dc(this);

	CRect rt;
	GetClientRect(&rt);

	if (m_hBitmap != NULL)
	{
		HDC hMemDC = CreateCompatibleDC(dc.GetSafeHdc());
		HBITMAP hOldBitmap = (HBITMAP)::SelectObject(hMemDC, m_hBitmap);
		BITMAP bmInfo;
		GetObject(m_hBitmap, sizeof(BITMAP), &bmInfo);

		int nStretchBltMode = SetStretchBltMode(dc.GetSafeHdc(), COLORONCOLOR);

		StretchBlt(dc.GetSafeHdc(), 0, 0, rt.Width(), rt.Height(), hMemDC, 0, 0, bmInfo.bmWidth, bmInfo.bmHeight, SRCCOPY);

		if (m_nZoomRectFactor != 0)
		{
			CRect rtZoom;
			rtZoom.left = rt.Width() / 2 - m_nZoomRectFactor / 2;
			rtZoom.top = rt.Height() / 2 - m_nZoomRectFactor / 2;
			rtZoom.right = rtZoom.left + m_nZoomRectFactor;
			rtZoom.bottom = rtZoom.top + m_nZoomRectFactor;
			CPen pen;
			pen.CreatePen(PS_SOLID, 1, RGB(0, 0, 0));
			CPen* pOldPen = dc.SelectObject(&pen);
			dc.MoveTo(rtZoom.TopLeft());
			dc.LineTo(rtZoom.right, rtZoom.top);
			dc.LineTo(rtZoom.BottomRight());
			dc.LineTo(rtZoom.left, rtZoom.bottom);
			dc.LineTo(rtZoom.TopLeft());
			dc.SelectObject(pOldPen);
			pen.DeleteObject();
		}

		SetStretchBltMode(dc.GetSafeHdc(), nStretchBltMode);

		::SelectObject(hMemDC, hOldBitmap);
		::DeleteDC(hMemDC);
	}
	else
	{
		dc.FillSolidRect(&rt, m_colBack);

		if (m_strText.GetLength() > 0)
		{
			HFONT hFnt = (HFONT)GetStockObject(DEFAULT_GUI_FONT);

			int nBkMode = dc.SetBkMode(TRANSPARENT);
			COLORREF colOld = dc.SetTextColor(m_colText);
			HFONT hOldFont = (HFONT)::SelectObject(dc.GetSafeHdc(), hFnt);

			dc.DrawText(m_strText, &rt, m_nTextAlign);

			::SelectObject(dc.GetSafeHdc(), hOldFont);
			dc.SetTextColor(colOld);
			dc.SetBkMode(nBkMode);
		}
	}
}
