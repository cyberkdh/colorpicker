//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@gmail.com)
//	Module			: 
//	History			: 
//
//////////////////////////////////////////////////////////////////////////////////////////////////

#pragma once

#include "DHColorFillCtl.h"
#include "afxwin.h"

//#define DEF_USE_MOUSEHOOK

#ifdef DEF_USE_MOUSEHOOK
#define KWM_CHK1		WM_USER + 201
#endif

class CDHColorPickerDlg : public CDialog
{
public:
	CDHColorPickerDlg(CWnd* pParent = NULL);	// standard constructor

#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_DHCOLORPICKER_DIALOG };
#endif

private:
	UINT m_uChkTimer;
	CRect m_rtZoomPreview;
	CRect m_rtZoom;
	int m_nZoomFactor;
	ATOM		m_hotkeyAtomValue;
	int m_nCopyFormatType;

public:
	HBITMAP CheckMouseLoc(COLORREF* colValue);

#ifdef DEF_USE_MOUSEHOOK
protected:
	static LRESULT CALLBACK MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam);
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

protected:
	HICON m_hIcon;

	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();

	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnContextMenu(CWnd* /*pWnd*/, CPoint /*point*/);
	
	
	CDHColorFillCtl m_ctlPreviewColor;
	CComboBox m_cbType;
	CDHColorFillCtl m_ctlR;
	CDHColorFillCtl m_ctlRD;
	CDHColorFillCtl m_ctlG;
	CDHColorFillCtl m_ctlGD;
	CDHColorFillCtl m_ctlB;
	CDHColorFillCtl m_ctlBD;
	CDHColorFillCtl m_ctlZoom;
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnDestroy();
	afx_msg void OnBnClickedCheckAlwaysOntop();
	CButton m_btnAlwaysOnTop;

#ifdef DEF_USE_MOUSEHOOK
	afx_msg LRESULT OnChk1(WPARAM w, LPARAM l);
#endif
	afx_msg void OnHotKey(UINT nHotKeyId, UINT nKey1, UINT nKey2);
	CComboBox m_cbCopyFormat;
	afx_msg void OnCbnSelchangeComboCopyformat();
	afx_msg void OnBnClickedButAbout();
};
