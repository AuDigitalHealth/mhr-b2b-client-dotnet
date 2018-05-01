cls
DEL *.CS
SET WSDLTOOL="c:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\SvcUtil.exe"

REM Interfaces that use XDS schemas
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_DocumentRepository*.wsdl      schema\common\*.xsd  schema\external\XDS.b_DocumentRepository.xsd schema\external\query.xsd schema\external\rs.xsd schema\external\lcm.xsd schema\external\rim.xsd   /out:PCEHR_DocumentRepository11.cs    /config:B2B_DocumentRepository.config      /namespace:*,Nehta.VendorLibrary.PCEHR.DocumentRepository     /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_DocumentRegistry*.wsdl        schema\common\*.xsd  schema\external\query.xsd schema\external\rs.xsd schema\external\lcm.xsd schema\external\rim.xsd                                                /out:PCEHR_DocumentRegistry11.cs      /config:B2B_DocumentRegistry.config        /namespace:*,Nehta.VendorLibrary.PCEHR.DocumentRegistry       /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GetChangeHistoryView*.wsdl    schema\common\*.xsd  schema\external\PCEHR_GetChangeHistoryView*.xsd schema\external\rim.xsd schema\external\xsd_1.xsd                                               /out:PCEHR_GetChangeHistoryView11.cs  /config:PCEHR_GetChangeHistoryView.config  /namespace:*,Nehta.VendorLibrary.PCEHR.GetChangeHistoryView   /tcv:Version35

REM Interfaces that dont use XDS schemas
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_PCEHRProfile*.wsdl            schema\common\*.xsd  schema\external\PCEHR_DoesPCEHRExist*.xsd schema\external\PCEHR_GainPCEHRAccess*.xsd                                                            /out:PCEHR_PCEHRProfile11.cs          /config:PCEHR_PCEHRProfile.config          /namespace:*,Nehta.VendorLibrary.PCEHR.PCEHRProfile           /tcv:Version35

%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GetAuditView*.wsdl            schema\common\*.xsd  schema\external\PCEHR_GetAuditView*.xsd              /out:PCEHR_GetAuditView11.cs             /config:PCEHR_GetAuditView.config            /namespace:*,Nehta.VendorLibrary.PCEHR.GetAuditView            /tcv:Version35

%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GetTemplate*.wsdl             schema\common\*.xsd  schema\external\PCEHR_GetTemplate*.xsd               /out:PCEHR_GetTemplate11.cs              /config:PCEHR_GetTemplate.config             /namespace:*,Nehta.VendorLibrary.PCEHR.GetTemplate             /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_RemoveDocument*.wsdl          schema\common\*.xsd  schema\external\PCEHR_RemoveDocument*.xsd            /out:PCEHR_RemoveDocument11.cs           /config:PCEHR_RemoveDocument.config          /namespace:*,Nehta.VendorLibrary.PCEHR.RemoveDocument          /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_SearchTemplate*.wsdl          schema\common\*.xsd  schema\external\PCEHR_SearchTemplate*.xsd            /out:PCEHR_SearchTemplate11.cs           /config:PCEHR_SearchTemplate.config          /namespace:*,Nehta.VendorLibrary.PCEHR.SearchTemplate          /tcv:Version35

REM New interfaces
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GetIndividualDetailsView*.wsdl   schema\common\*.xsd  schema\external\PCEHR_GetIndividualDetailsView*.xsd    /out:PCEHR_GetIndividualDetailsView20.cs  /config:GetIndividualDetailsView.config  /namespace:*,Nehta.VendorLibrary.PCEHR.GetIndividualDetailsView   /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GetRepresentativeList*.wsdl      schema\common\*.xsd  schema\external\PCEHR_GetRepresentativeList*.xsd       /out:PCEHR_GetRepresentativeList11.cs     /config:GetRepresentativeList.config     /namespace:*,Nehta.VendorLibrary.PCEHR.GetRepresentativeList      /tcv:Version35
%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_RegisterPCEHR*.wsdl              schema\common\*.xsd  schema\external\PCEHR_RegisterPCEHR*.xsd               /out:PCEHR_RegisterPCEHR20.cs             /config:RegisterPCEHR.config             /namespace:*,Nehta.VendorLibrary.PCEHR.RegisterPCEHR              /tcv:Version35

%WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_getView*.wsdl                    schema\common\*.xsd  schema\external\PCEHR_GetView.xsd  /out:PCEHR_GetView10.cs                                       /config:GetView.config                   /namespace:*,Nehta.VendorLibrary.PCEHR.GetView                    /tcv:Version35

rem %WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GSERegistration*.wsdl            schema\common\*.xsd  schema\external\PCEHR_GSERegistration*.xsd             /out:PCEHR_GSERegistration10.cs           /config:GSERegistration.config           /namespace:*,nehta.pcehr.GSERegistration10            /tcv:Version35
rem %WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_GSERegistration*.wsdl            schema\common\*.xsd                                                         /out:PCEHR_GSERegistration10.cs           /config:GSERegistration.config           /namespace:*,nehta.pcehr.GSERegistration10            /tcv:Version35
rem %WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_RequestIVC*.wsdl                 schema\common\*.xsd  schema\external\PCEHR_RequestIVC*.xsd                  /out:PCEHR_RequestIVC20.cs                /config:RequestIVC.config                /namespace:*,nehta.pcehr.RequestIVC20              /tcv:Version35
rem %WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_StorePOROInformation*.wsdl       schema\common\*.xsd  schema\external\PCEHR_StorePOROInformation*.xsd        /out:PCEHR_StorePOROInformation10.cs      /config:StorePOROInformation.config      /namespace:*,nehta.pcehr.StorePOROInformation10       /tcv:Version35
rem %WSDLTOOL%  /useSerializerForFaults /serializer:XmlSerializer  wsdl\external\B2B_UpdateIHIDetails*.wsdl           schema\common\*.xsd  schema\external\PCEHR_UpdateIHIDetails*.xsd            /out:PCEHR_UpdateIHIDetails10.cs          /config:UpdateIHIDetails.config          /namespace:*,nehta.pcehr.UpdateIHIDetails10           /tcv:Version35

DEL *.Config
PAUSE
