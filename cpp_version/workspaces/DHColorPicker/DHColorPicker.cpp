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

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

BEGIN_MESSAGE_MAP(CDHColorPickerApp, CWinApp)
	ON_COMMAND(ID_HELP, &CWinApp::OnHelp)
END_MESSAGE_MAP()

CDHColorPickerApp::CDHColorPickerApp()
{
}

CDHColorPickerApp theApp;

BOOL CDHColorPickerApp::InitInstance()
{
	INITCOMMONCONTROLSEX InitCtrls;
	InitCtrls.dwSize = sizeof(InitCtrls);
	InitCtrls.dwICC = ICC_WIN95_CLASSES;
	InitCommonControlsEx(&InitCtrls);

	CWinApp::InitInstance();

	AfxEnableControlContainer();

	SetRegistryKey(_T("DHTools\\DHColorPicker"));

	CDHColorPickerDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
	}
	else if (nResponse == IDCANCEL)
	{
	}
	else if (nResponse == -1)
	{
		TRACE(traceAppMsg, 0, "Warning: dialog creation failed, so application is terminating unexpectedly.\n");
	}

#if !defined(_AFXDLL) && !defined(_AFX_NO_MFC_CONTROLS_IN_DIALOGS)
	ControlBarCleanUp();
#endif

	return FALSE;
}

