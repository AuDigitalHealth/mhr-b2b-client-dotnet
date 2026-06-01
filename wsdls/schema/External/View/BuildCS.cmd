@echo OFF
SET XSDTOOL="c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\xsd.exe"

cls
@echo ============================================================================
@echo --- DELETE FILES NOT REQUIRED                                            ---
@echo ============================================================================

DEL *.cs

@echo ============================================================================
@echo --- Building WSDL proxies                                                ---
@echo ============================================================================

REM Not for Release 3
REM %XSDTOOL% PCEHR_AchievementDiaryView.xsd          /c /l:CS /n:Nehta.VendorLibrary.PCEHR.AchievementDiaryView

REM For Release 3
%XSDTOOL% PCEHR_HealthCheckScheduleView.xsd       /c /l:CS /n:Nehta.VendorLibrary.PCEHR.HealthCheckScheduleView
%XSDTOOL% PCEHR_MedicareOverview.xsd              /c /l:CS /n:Nehta.VendorLibrary.PCEHR.MedicareOverview
%XSDTOOL% PCEHR_ObservationView.xsd               /c /l:CS /n:Nehta.VendorLibrary.PCEHR.ObservationView
%XSDTOOL% PCEHR_PrescriptionAndDispenseView.xsd   /c /l:CS /n:Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView

REM For Release 5
%XSDTOOL% PCEHR_PathologyReportView.xsd            /c /l:CS /n:Nehta.VendorLibrary.PCEHR.PathologyReportView
%XSDTOOL% PCEHR_DiagnosticImagingReportView.xsd    /c /l:CS /n:Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView
%XSDTOOL% PCEHR_HealthRecordOverview.xsd           /c /l:CS /n:Nehta.VendorLibrary.PCEHR.HealthRecordOverview
%XSDTOOL% PCEHR_AdvanceCarePlanningView.xsd        /c /l:CS /n:Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView

REM For Release 7
%XSDTOOL% PCEHR_View_CommonType.xsd ../../Common/xmldsig-core-schema.xsd ../../Common/PCEHR_CommonTypes_Supplementary.xsd PCEHR_PathologyReportView_Response.xsd /c /l:CS /n:Nehta.VendorLibrary.PCEHR.PathologyReportView
ren PCEHR_CommonTypes_Supplementary_PCEHR_PathologyReportView_Response.cs PCEHR_PathologyReportView_Response.cs

%XSDTOOL% PCEHR_View_CommonType.xsd ../../Common/xmldsig-core-schema.xsd ../../Common/PCEHR_CommonTypes_Supplementary.xsd PCEHR_DiagnosticImagingReportView_Response.xsd /c /l:CS /n:Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView
ren PCEHR_CommonTypes_Supplementary_PCEHR_DiagnosticImagingReportView_Response.cs PCEHR_DiagnosticImagingReportView_Response.cs

%XSDTOOL% PCEHR_View_CommonType.xsd ../../Common/xmldsig-core-schema.xsd ../../Common/PCEHR_CommonTypes_Supplementary.xsd PCEHR_HealthRecordOverview_Response.xsd /c /l:CS /n:Nehta.VendorLibrary.PCEHR.HealthRecordOverview
ren PCEHR_CommonTypes_Supplementary_PCEHR_HealthRecordOverview_Response.cs PCEHR_HealthRecordOverview_Response.cs

%XSDTOOL% PCEHR_View_CommonType.xsd ../../Common/xmldsig-core-schema.xsd ../../Common/PCEHR_CommonTypes_Supplementary.xsd PCEHR_AdvanceCarePlanningView_Response.xsd /c /l:CS /n:Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView
ren PCEHR_CommonTypes_Supplementary_PCEHR_AdvanceCarePlanningView_Response.cs PCEHR_AdvanceCarePlanningView_Response.cs




PAUSE
