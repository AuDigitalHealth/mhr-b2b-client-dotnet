REM http://www.medicareaustralia.gov.au/provider/vendors/healthcare-identifiers-developers/licensed-material/current-versions.jsp
@echo OFF
cls
@echo ============================================================================
@echo --- DELETE FILES NOT REQUIRED                                            ---
@echo ============================================================================

DEL .\net\*.cs \Q
MD .\net

@echo ============================================================================
@echo --- Building WSDL proxies                                                ---
@echo ============================================================================

dotnet-svcutil  wsdl\external\B2B_DocumentRepository*.wsdl      schema\common\*.xsd  schema\external\XDS.b_DocumentRepository.xsd schema\external\query.xsd schema\external\rs.xsd schema\external\lcm.xsd schema\external\rim.xsd    --outputDir .\net --outputFile PCEHR_DocumentRepository11.cs        --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.DocumentRepository   
dotnet-svcutil  wsdl\external\B2B_DocumentRegistry*.wsdl        schema\common\*.xsd  schema\external\query.xsd schema\external\rs.xsd schema\external\lcm.xsd schema\external\rim.xsd                                                 --outputDir .\net --outputFile PCEHR_DocumentRegistry11.cs          --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.DocumentRegistry     
dotnet-svcutil  wsdl\external\B2B_GetChangeHistoryView*.wsdl    schema\common\*.xsd  schema\external\PCEHR_GetChangeHistoryView*.xsd schema\external\rim.xsd schema\external\xsd_1.xsd                                                --outputDir .\net --outputFile PCEHR_GetChangeHistoryView11.cs     --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetChangeHistoryView 

REM Interfaces that dont use XDS schemas
dotnet-svcutil  wsdl\external\B2B_PCEHRProfile*.wsdl            schema\common\*.xsd  schema\external\PCEHR_DoesPCEHRExist*.xsd schema\external\PCEHR_GainPCEHRAccess*.xsd    --outputDir .\net --outputFile PCEHR_PCEHRProfile11.cs              --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.PCEHRProfile         

dotnet-svcutil  wsdl\external\B2B_GetAuditView*.wsdl            schema\common\*.xsd  schema\external\PCEHR_GetAuditView*.xsd               --outputDir .\net --outputFile PCEHR_GetAuditView11.cs                   --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetAuditView          

dotnet-svcutil  wsdl\external\B2B_GetTemplate*.wsdl             schema\common\*.xsd  schema\external\PCEHR_GetTemplate*.xsd                --outputDir .\net --outputFile PCEHR_GetTemplate11.cs                    --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetTemplate           
dotnet-svcutil  wsdl\external\B2B_RemoveDocument*.wsdl          schema\common\*.xsd  schema\external\PCEHR_RemoveDocument*.xsd             --outputDir .\net --outputFile PCEHR_RemoveDocument11.cs                 --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.RemoveDocument        
dotnet-svcutil  wsdl\external\B2B_SearchTemplate*.wsdl          schema\common\*.xsd  schema\external\PCEHR_SearchTemplate*.xsd             --outputDir .\net --outputFile PCEHR_SearchTemplate11.cs                 --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.SearchTemplate        

REM New interfaces
dotnet-svcutil  wsdl\external\B2B_GetIndividualDetailsView*.wsdl   schema\common\*.xsd  schema\external\PCEHR_GetIndividualDetailsView*.xsd     --outputDir .\net --outputFile PCEHR_GetIndividualDetailsView20.cs  --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetIndividualDetailsView 
dotnet-svcutil  wsdl\external\B2B_GetRepresentativeList*.wsdl      schema\common\*.xsd  schema\external\PCEHR_GetRepresentativeList*.xsd        --outputDir .\net --outputFile PCEHR_GetRepresentativeList11.cs     --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetRepresentativeList    
dotnet-svcutil  wsdl\external\B2B_RegisterPCEHR*.wsdl              schema\common\*.xsd  schema\external\PCEHR_RegisterPCEHR*.xsd                --outputDir .\net --outputFile PCEHR_RegisterPCEHR20.cs             --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.RegisterPCEHR            

dotnet-svcutil  wsdl\external\B2B_getView*.wsdl                    schema\common\*.xsd  schema\external\PCEHR_GetView.xsd   --outputDir .\net --outputFile PCEHR_GetView10.cs                                       --sync --serializer XmlSerializer --namespace *,Nehta.VendorLibrary.PCEHR.GetView                  


DEL *.config
PAUSE

