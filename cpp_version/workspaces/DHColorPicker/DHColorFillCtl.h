//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@gmail.com)
//	Module			: 
//	History			: 
//
//////////////////////////////////////////////////////////////////////////////////////////////////

#pragma once

class CDHColorFillCtl : public CStatic
{
	DECLARE_DYNAMIC(CDHColorFillCtl)

public:
	CDHColorFillCtl();
	virtual ~CDHColorFillCtl();

public:
	void SetColor(COLORREF col)
	{
		m_colBack = col;
		if (::IsWindow(GetSafeHwnd()))
			Invalidate();
	}
	COLORREF GetColor(void) { return m_colBack; }

	void SetBackColor(COLORREF col)
	{
		m_colBack = col;
		if (::IsWindow(GetSafeHwnd()))
			Invalidate();
	}
	COLORREF GetBackColor(void) { return m_colBack; }

	void SetForeColor(COLORREF col)
	{
		m_colText = col;
		if (::IsWindow(GetSafeHwnd()))
			Invalidate();
	}
	COLORREF GetForeColor(void) { return m_colText; }

	void SetTextAlign(UINT align)
	{
		m_nTextAlign = align;
		if (IsWindow(this->GetSafeHwnd()))
			Invalidate();
	}

	UINT GetTextAlign(void) { return m_nTextAlign; }

	void SetText(CString str)
	{
		m_strText = str;
		if (IsWindow(this->GetSafeHwnd()))
			Invalidate();
	}

	CString GetText(void) { return m_strText; }

	void SetBitmap(HBITMAP hBM)
	{
		if (m_hBitmap != NULL)
			DeleteObject(m_hBitmap);
		m_hBitmap = NULL;
		m_hBitmap = hBM;

		if (IsWindow(this->GetSafeHwnd()))
			Invalidate();
	}

	void ShowZoomRect(int nFactor)
	{
		m_nZoomRectFactor = nFactor;
		if (::IsWindow(this->GetSafeHwnd()))
			Invalidate();
	}

private:
	COLORREF		m_colBack;
	COLORREF		m_colText;
	CString			m_strText;
	UINT			m_nTextAlign;
	HBITMAP			m_hBitmap;
	int				m_nZoomRectFactor;

protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnPaint();

};


