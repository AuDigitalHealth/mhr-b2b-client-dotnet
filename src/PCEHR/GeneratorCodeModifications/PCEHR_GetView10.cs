using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nehta.VendorLibrary.PCEHR.GetView
{
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.PrescriptionAndDispenseView.prescriptionAndDispenseView))]
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.MedicareOverview.medicareOverview))]
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.HealthCheckScheduleView.healthCheckScheduleView))]
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.ObservationView.observationView))]

    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.HealthRecordOverview.healthRecordOverView))]
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.PathologyReportView.pathologyReportView))]
    [System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.DiagnosticImagingReportView.diagnosticImagingReportView))]
	[System.Xml.Serialization.XmlInclude(typeof(Nehta.VendorLibrary.PCEHR.AdvanceCarePlanningView.advanceCarePlanningView))]
	
    public partial class getView
    {
    }
}
