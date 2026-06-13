//////////////////////////////////////////////////////////////////////////////////////////////////
//	Projects		: DHColorPicker
//	Author			: CYBERKDH(cyberkdh@gmail.com)
//	Module			: 
//	History			: 
//
//////////////////////////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "DHColorPicker.h"
#include "DHColorPickerDlg.h"
#include "afxdialogex.h"

#ifdef DEF_USE_MOUSEHOOK
HHOOK g_hMouseHook = NULL;
CDHColorPickerDlg* g_pMainDLG = NULL;
#endif

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_ABOUTBOX };
#endif

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(IDD_ABOUTBOX)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()

CDHColorPickerDlg::CDHColorPickerDlg(CWnd* pParent /*=NULL*/)
	: CDialog(IDD_DHCOLORPICKER_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_uChkTimer = 0;

#ifdef DEF_USE_MOUSEHOOK
	g_hMouseHook = NULL;
	g_pMainDLG = NULL;
#endif
}

void CDHColorPickerDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_STATIC_PREVIEWCOLOR, m_ctlPreviewColor);
	DDX_Control(pDX, IDC_COMBO_TYPE, m_cbType);
	DDX_Control(pDX, IDC_STATIC_R, m_ctlR);
	DDX_Control(pDX, IDC_STATIC_RD, m_ctlRD);
	DDX_Control(pDX, IDC_STATIC_G, m_ctlG);
	DDX_Control(pDX, IDC_STATIC_GD, m_ctlGD);
	DDX_Control(pDX, IDC_STATIC_B, m_ctlB);
	DDX_Control(pDX, IDC_STATIC_BD, m_ctlBD);
	DDX_Control(pDX, IDC_STATIC_ZOOM, m_ctlZoom);
	DDX_Control(pDX, IDC_CHECK_ALWAYS_ONTOP, m_btnAlwaysOnTop);
	DDX_Control(pDX, IDC_COMBO_COPYFORMAT, m_cbCopyFormat);
}

BEGIN_MESSAGE_MAP(CDHColorPickerDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_LBUTTONDOWN()
	ON_WM_DESTROY()
	ON_WM_CONTEXTMENU()
	ON_WM_TIMER()
	ON_BN_CLICKED(IDOK, &CDHColorPickerDlg::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CDHColorPickerDlg::OnBnClickedCancel)
#ifdef DEF_USE_MOUSEHOOK
	ON_MESSAGE(KWM_CHK1, OnChk1)
#endif
	ON_BN_CLICKED(IDC_CHECK_ALWAYS_ONTOP, &CDHColorPickerDlg::OnBnClickedCheckAlwaysOntop)
	ON_WM_HOTKEY()
	ON_CBN_SELCHANGE(IDC_COMBO_COPYFORMAT, &CDHColorPickerDlg::OnCbnSelchangeComboCopyformat)
	ON_BN_CLICKED(IDC_BUT_ABOUT, &CDHColorPickerDlg::OnBnClickedButAbout)
END_MESSAGE_MAP()

BOOL CDHColorPickerDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_ctlR.SetText(L"R");
	m_ctlG.SetText(L"G");
	m_ctlB.SetText(L"B");

	m_ctlR.SetBackColor(RGB(0, 0, 0));
	m_ctlG.SetBackColor(RGB(0, 0, 0));
	m_ctlB.SetBackColor(RGB(0, 0, 0));

	m_ctlR.SetForeColor(RGB(0, 254, 254));
	m_ctlG.SetForeColor(RGB(0, 254, 254));
	m_ctlB.SetForeColor(RGB(0, 254, 254));

	m_ctlR.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_CENTER);
	m_ctlG.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_CENTER);
	m_ctlB.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_CENTER);

	m_ctlRD.SetBackColor(RGB(0, 0, 0));
	m_ctlGD.SetBackColor(RGB(0, 0, 0));
	m_ctlBD.SetBackColor(RGB(0, 0, 0));

	m_ctlRD.SetForeColor(RGB(0, 254, 254));
	m_ctlGD.SetForeColor(RGB(0, 254, 254));
	m_ctlBD.SetForeColor(RGB(0, 254, 254));

	m_ctlRD.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_RIGHT);
	m_ctlGD.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_RIGHT);
	m_ctlBD.SetTextAlign(DT_SINGLELINE | DT_VCENTER | DT_RIGHT);

	CRect rt;
	m_ctlZoom.GetWindowRect(&rt);
	if (rt.Width() % 2 != 0)
		rt.right++;
	if (rt.Height() % 2 != 0)
		rt.bottom++;
	m_ctlZoom.SetWindowPos(NULL, 0, 0, rt.Width(), rt.Height(), SWP_NOMOVE | SWP_DRAWFRAME | SWP_NOZORDER);
	m_rtZoomPreview = rt;
	m_nZoomFactor = 8;
	m_rtZoom.left = 0;
	m_rtZoom.top = 0;
	m_rtZoom.right = m_rtZoomPreview.Width() / m_nZoomFactor;
	m_rtZoom.bottom = m_rtZoomPreview.Height() / m_nZoomFactor;

	m_ctlZoom.ShowZoomRect(m_nZoomFactor);
	m_cbType.SetCurSel(0);

#ifdef DEF_USE_MOUSEHOOK
	g_pMainDLG = this;
	g_hMouseHook = SetWindowsHookEx(WH_MOUSE, CDHColorPickerDlg::MouseHookProc, (HINSTANCE)NULL, GetCurrentThreadId());
#else
	m_uChkTimer = SetTimer(1000, 100, NULL);
#endif

	m_cbCopyFormat.InsertString(0, _T("#RRGGBB"));
	m_cbCopyFormat.InsertString(1, _T("RR, GG, BB"));
	m_cbCopyFormat.InsertString(2, _T("Integer"));
	m_cbCopyFormat.InsertString(3, _T("(RR, GG, BB)"));
	m_cbCopyFormat.InsertString(4, _T("(256, 256, 256)"));

	m_nCopyFormatType = theApp.GetProfileInt(_T("SETTINGS"), _T("COPYFORMATTYPE"), 0);
	if (m_nCopyFormatType < 0)
		m_nCopyFormatType = 0;
	if (m_nCopyFormatType > 4)
		m_nCopyFormatType = 4;
	m_cbCopyFormat.SetCurSel(m_nCopyFormatType);

	m_hotkeyAtomValue = 0;
	m_hotkeyAtomValue = GlobalAddAtom(_T("MWCOLORPICKER"));
	if (m_hotkeyAtomValue != 0)
	{
		// HOTKEY ��� 
		// WIN + CONTROL + C
		BOOL bRet = RegisterHotKey(GetSafeHwnd(), m_hotkeyAtomValue, MOD_WIN | MOD_CONTROL | MOD_ALT, 0x43);

		int a1 = 0;
		a1++;

		if (bRet == FALSE)
		{
			DWORD dwLastError = GetLastError();
			a1++;
		}
		
	}

	int nAOT = theApp.GetProfileInt(_T("SETTINGS"), _T("ALWAYSONTOP"), 1);
#if 0 // not working
	SetWindowPos(nAOT != 0 ? &wndTopMost : &wndNoTopMost,
		0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_DRAWFRAME);
#endif

	m_btnAlwaysOnTop.SetCheck(nAOT != 0 ? BST_CHECKED : BST_UNCHECKED);
	
	// why twice call hum
	if (nAOT != 0)
	{
		SetWindowPos(&wndTopMost,
			0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);

		SetWindowPos(&wndTopMost,
			0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
	}


	

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CDHColorPickerDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		if (nID == SC_CLOSE)
		{
			CDialog::OnOK();
		}
		CDialog::OnSysCommand(nID, lParam);
	}
}

void CDHColorPickerDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

HCURSOR CDHColorPickerDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}



void CDHColorPickerDlg::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: Add your message handler code here and/or call default

	CDialog::OnLButtonDown(nFlags, point);
}


void CDHColorPickerDlg::OnDestroy()
{
	CDialog::OnDestroy();

#ifdef DEF_USE_MOUSEHOOK
	g_pMainDLG = NULL;

	if (g_hMouseHook != NULL)
		UnhookWindowsHookEx(g_hMouseHook);
	g_hMouseHook = NULL;
#else

	if (m_uChkTimer != 0)
		KillTimer(m_uChkTimer);
	m_uChkTimer = 0;
#endif
}


void CDHColorPickerDlg::OnContextMenu(CWnd* /*pWnd*/, CPoint /*point*/)
{
}


void CDHColorPickerDlg::OnTimer(UINT_PTR nIDEvent)
{
	COLORREF col;
	HBITMAP hBM = CheckMouseLoc(&col);
	if (hBM != NULL)
	{
		m_ctlZoom.SetBitmap(hBM);
		if (col != m_ctlPreviewColor.GetColor())
		{
			m_ctlPreviewColor.SetBackColor(col);
			int nSel = m_cbType.GetCurSel();
			CString strR, strG, strB;
			CString strFmt = L"%d";
			switch (nSel)
			{
			case 0:
				strFmt = L"%d";
				break;
			case 1:
				strFmt = L"%02X";
				break;
			}
			strR.Format(strFmt, GetRValue(col));
			strG.Format(strFmt, GetGValue(col));
			strB.Format(strFmt, GetBValue(col));

			m_ctlRD.SetText(strR);
			m_ctlGD.SetText(strG);
			m_ctlBD.SetText(strB);
		}
	}

	CDialog::OnTimer(nIDEvent);
}


void CDHColorPickerDlg::OnBnClickedOk()
{
}


void CDHColorPickerDlg::OnBnClickedCancel()
{
}

#ifdef DEF_USE_MOUSEHOOK
LRESULT CDHColorPickerDlg::OnChk1(WPARAM w, LPARAM l)
{
	COLORREF col;
	HBITMAP hBM = CheckMouseLoc(&col);
	if (hBM != NULL)
	{
		m_ctlZoom.SetBitmap(hBM);
		if (col != m_ctlPreviewColor.GetColor())
		{
			m_ctlPreviewColor.SetBackColor(col);
			int nSel = m_cbType.GetCurSel();
			CString strR, strG, strB;
			CString strFmt = L"%d";
			switch (nSel)
			{
			case 0:
				strFmt = L"%d";
				break;
			case 1:
				strFmt = L"%02X";
				break;
			}
			strR.Format(strFmt, GetRValue(col));
			strG.Format(strFmt, GetGValue(col));
			strB.Format(strFmt, GetBValue(col));

			m_ctlRD.SetText(strR);
			m_ctlGD.SetText(strG);
			m_ctlBD.SetText(strB);
		}
	}

	return 0;
}

LRESULT CALLBACK CDHColorPickerDlg::MouseHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode == HC_ACTION)
	{
		if (wParam == WM_MOUSEMOVE)
		{
			if (g_pMainDLG != NULL && ::IsWindow(g_pMainDLG->GetSafeHwnd()))
			{
				MOUSEHOOKSTRUCT* pmh = (MOUSEHOOKSTRUCT*)lParam;
				TRACE("M : %d, %d\n", pmh->pt.x, pmh->pt.y);
				g_pMainDLG->PostMessage(KWM_CHK1, 0, 0);
			}
		}
	}

	return CallNextHookEx(g_hMouseHook, nCode, wParam, lParam);
}

#endif

HBITMAP CDHColorPickerDlg::CheckMouseLoc(COLORREF* colValue)
{
	CPoint pt;
	GetCursorPos(&pt);
	HDC hDC = ::GetDC(NULL);

	HDC hMemDC = CreateCompatibleDC(hDC);
	HBITMAP hBm = CreateCompatibleBitmap(hDC, m_rtZoom.Width(), m_rtZoom.Height());
	HBITMAP hOldBitmap = (HBITMAP)::SelectObject(hMemDC, hBm);

	::BitBlt(hMemDC, 0, 0, m_rtZoom.Width(), m_rtZoom.Height(), hDC, pt.x - m_rtZoom.Width() / 2, pt.y - m_rtZoom.Height() / 2, SRCCOPY);
	*colValue = ::GetPixel(hDC, pt.x, pt.y);

	::SelectObject(hMemDC, hOldBitmap);
	::DeleteDC(hMemDC);
	::ReleaseDC(NULL, hDC);

	return hBm;
}


void CDHColorPickerDlg::OnBnClickedCheckAlwaysOntop()
{
	TRACE("Checked: %d\n", m_btnAlwaysOnTop.GetCheck());
	SetWindowPos(m_btnAlwaysOnTop.GetCheck() == BST_CHECKED ? &wndTopMost : &wndNoTopMost,
		0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_DRAWFRAME);

	theApp.WriteProfileInt(_T("SETTINGS"), _T("ALWAYSONTOP"), m_btnAlwaysOnTop.GetCheck());
}


void CDHColorPickerDlg::OnHotKey(UINT nHotKeyId, UINT nKey1, UINT nKey2)
{
	if (nHotKeyId == m_hotkeyAtomValue)
	{
		// Clipboard ����
		BOOL bOpen = OpenClipboard();
		if (bOpen == TRUE)
		{
			EmptyClipboard();

			COLORREF col = m_ctlPreviewColor.GetColor();

			CString str;
			if (m_nCopyFormatType == 0)
				str.Format(_T("#%02X%02X%02X"), GetRValue(col), GetGValue(col), GetBValue(col));
			else if (m_nCopyFormatType == 1)
				str.Format(_T("%02X, %02X, %02X"), GetRValue(col), GetGValue(col), GetBValue(col));
			else if (m_nCopyFormatType == 2)
				str.Format(_T("%d"), col);
			else if (m_nCopyFormatType == 3)
				str.Format(_T("(%02X, %02X, %02X)"), GetRValue(col), GetGValue(col), GetBValue(col));
			else if (m_nCopyFormatType == 4)
				str.Format(_T("(%d, %d, %d"), GetRValue(col), GetGValue(col), GetBValue(col));

			size_t cbStr = (str.GetLength() + 1) * sizeof(TCHAR);
			HGLOBAL hData = GlobalAlloc(GMEM_MOVEABLE, cbStr);
			memcpy_s(GlobalLock(hData), cbStr, str.LockBuffer(), cbStr);
			GlobalUnlock(hData);
			str.UnlockBuffer();

			UINT uiFormat = (sizeof(TCHAR) == sizeof(WCHAR)) ? CF_UNICODETEXT : CF_TEXT;
			::SetClipboardData(uiFormat, hData);
			CloseClipboard();
		}
	}

	CDialog::OnHotKey(nHotKeyId, nKey1, nKey2);
}



void CDHColorPickerDlg::OnCbnSelchangeComboCopyformat()
{
	m_nCopyFormatType = m_cbCopyFormat.GetCurSel();
	if (m_nCopyFormatType < 0)
		m_nCopyFormatType = 0;
	if (m_nCopyFormatType > 4)
		m_nCopyFormatType = 4;
	theApp.WriteProfileInt(_T("SETTINGS"), _T("COPYFORMATTYPE"), m_nCopyFormatType);
}


void CDHColorPickerDlg::OnBnClickedButAbout()
{
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();
}
